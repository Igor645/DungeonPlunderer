using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private Animator myAnimator;

    private bool facingRight = true;

    //variables to play with
    public float speed = 2.0f;
    public float horizMovement;//- 1[OR]-1[OR]0

    private bool isSpawnExit;
    private GameObject[] players;

    private int howManyLevelsLoaded = 0;

    private enum MovementState{running}

    [SerializeField] private AudioSource runSoundEffect;

    [Header("Sound Settings")]
    [SerializeField] private float cooldownForRunningSound;

    private void Start()
    {
        //define the gameobjects found on the player
        rb2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();

        DontDestroyOnLoad(gameObject);
        Physics2D.IgnoreLayerCollision(10, 12, true);

        if (GameObject.FindGameObjectWithTag("bossmusic"))
        {
            AudioSource bossTheme = GameObject.FindGameObjectWithTag("bossmusic").GetComponent<AudioSource>();
            bossTheme.Stop();
        }
    }

    //handles the input for physics
    private void Update()
    {
        cooldownForRunningSound -= Time.deltaTime;
        //check direction given by player
        if (!(Input.GetKey("r")))
        {
                horizMovement = Input.GetAxis("Horizontal");

                if(cooldownForRunningSound <= 0 && Input.GetAxis("Horizontal") != 0 && gameObject.GetComponent<PlayerJump>().grounded)
                {
                    runSoundEffect.Play();
                    cooldownForRunningSound = 0.4f;
                }
        }
        else
        {
            horizMovement = 0;
        }
    }

    //handles running the physics
    private void FixedUpdate()
    {
        //move the character left and right
        rb2D.velocity = new Vector2(horizMovement * speed, rb2D.velocity.y);
        Flip(horizMovement);
        myAnimator.SetFloat("speed", Mathf.Abs(horizMovement));
    }

   private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "DialogueStarter")
        {
            GameObject.FindGameObjectWithTag("DialogueBox").GetComponentInChildren<Dialogue>().textComponent.text = string.Empty;
            GameObject.FindGameObjectWithTag("DialogueBox").GetComponentInChildren<Dialogue>().StartDialogue();
        }

        if (collision.GetComponent<LevelLoader>() != null)
        {
            isSpawnExit = collision.GetComponent<LevelLoader>().isExit;
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        howManyLevelsLoaded++;

        if (isSpawnExit || howManyLevelsLoaded == 1)
        { 
            FindEntrancePos();
        }
        else if(!isSpawnExit)
        {
            FindExitPos();
        }

        players = GameObject.FindGameObjectsWithTag("Player");

        if(players.Length > 1)
        {
            Destroy(players[1]);   
        }

        if (GameObject.FindGameObjectWithTag("bossmusic"))
        {
            GameObject.FindGameObjectWithTag("bossmusic").GetComponent<AudioSource>().Stop();
        }
    }

    //flipping function
    private void Flip(float horizMovement)
    {
        //if facing left && moving left
        if (horizMovement < 0 && facingRight || horizMovement > 0 && !facingRight)
        {
            facingRight = !facingRight;

            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    void FindEntrancePos()
    {
        transform.position = GameObject.FindWithTag("EntrancePos").transform.position;
    }

    void FindExitPos()
    {
        transform.position = GameObject.FindWithTag("ExitPos").transform.position;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{

    public Transform player;
    private Animator anim;
    private Rigidbody2D rb;
    [SerializeField] CanvasGroup cg;

    [Header("Ground Details")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float radOCircle;
    [SerializeField] AudioSource bossTheme;
    [SerializeField] AudioSource victorySound;

    public bool grounded;
    public float playerTimeInRange = 0;
    public float attack1Range = 8;
    private int amountsPlayed = 0;


    public bool isFlipped = false;

    private void Awake()
    {
        cg.interactable = false;
        cg.alpha = 0;

        if(GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        anim = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, radOCircle, whatIsGround);

        if (Vector2.Distance(player.position, rb.position) <= attack1Range)
        {
            playerTimeInRange += Time.fixedDeltaTime;
        }

        if (anim.GetBool("Run") || anim.GetBool("Idle"))
        {
            if (playerTimeInRange >= 50)
            {
                anim.SetBool("Roll", true);
                anim.SetBool("Run", false);
                playerTimeInRange = 0;
            }
        }

        if (gameObject.GetComponent<Health>().dead)
        {
            if (amountsPlayed == 0)
            {
                amountsPlayed++;
                bossTheme.Stop();
                victorySound.Play();
                cg.interactable = true;
                cg.alpha = 1;
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = false;
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerJump>().enabled = false;
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>().enabled = false;
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShield>().enabled = false;
            }
            if(Input.GetKeyDown(KeyCode.Space))
            {
                Application.Quit();
            }

        }
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if(transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if(transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheck.position, radOCircle);
    }
}

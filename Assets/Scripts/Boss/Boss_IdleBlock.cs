using UnityEngine;

public class Boss_IdleBlock : MonoBehaviour
{
    private Animator anim;
    private Collider2D playerAttack;
    private GameObject player;

    public float blockingTime;
    private bool declareTimer;

    private void Start()
    {
        anim = GetComponent<Animator>();
        if (GameObject.FindGameObjectWithTag("PlayerAttackArea") != null)
        {
            playerAttack = GameObject.FindGameObjectWithTag("PlayerAttackArea").GetComponent<Collider2D>();
        }
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (anim.GetBool("IdleBlock"))
        {
            if (declareTimer)
            {
                blockingTime = 2.5f;
            }
            blockingTime -= Time.deltaTime;
            declareTimer = false;
        }

        if (blockingTime <= 0)
        {
            anim.SetBool("IdleBlock", false);
            declareTimer = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerAttackArea" && blockingTime > 0)
        {
            Debug.Log(collision.tag);
            player.GetComponent<Health>().TakeDamage(1);
            gameObject.GetComponent<Health>().AddHealth(1);
            anim.SetTrigger("Block");
            blockingTime = 0;
        }
    }
}

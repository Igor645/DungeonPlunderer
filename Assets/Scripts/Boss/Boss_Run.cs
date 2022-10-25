using UnityEngine;

public class Boss_Run : StateMachineBehaviour
{
    public float speed = 2;
    public float rollSpeed = 100;
    public float attack1Range = 8;

    Transform player;
    Rigidbody2D rb;
    Boss boss;

    private int attackPicker;
    public float attackCooldown = 1;
    private float currentCooldown = 0;
    private float minDistance = 4;

    private enum MovementState { Running }
    private AudioSource bossRun;
    private float cooldownForRunningSound = 0.4f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<Boss>();
        bossRun = animator.GetComponents<AudioSource>()[1];
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        cooldownForRunningSound -= Time.deltaTime;
        currentCooldown -= Time.deltaTime;

        attackPicker = Random.Range(1, 3);

        boss.LookAtPlayer();
        Vector2 target = new Vector2(player.position.x, rb.position.y);
        float range = Vector2.Distance(target, rb.position);

        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);

        if (boss.grounded)
        {
            if (range > minDistance)
            {
                animator.SetBool("Idle", false);
                animator.SetBool("Run", true);
                rb.MovePosition(newPos);
                if(cooldownForRunningSound <= 0)
                {
                    bossRun.Play();
                    cooldownForRunningSound = 0.4f;
                }
            }
            else
            {
                animator.SetBool("Idle", true);
                animator.SetBool("Run", false);
            }

            if (currentCooldown <= 0)
            {

                if (Vector2.Distance(player.position, rb.position) <= attack1Range)
                {
                    animator.SetTrigger("Attack1");
                }
                else if (Vector2.Distance(player.position, rb.position) > attack1Range && attackPicker == 2)
                {
                    animator.SetTrigger("Jump");
                }
                currentCooldown = attackCooldown;
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack1");
        animator.ResetTrigger("Jump");
        animator.ResetTrigger("Attack2");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_RollToSide : StateMachineBehaviour
{
    private Transform edgePointLeft;
    private Transform edgePointRight;
    private Rigidbody2D rb;
    private Boss boss;
    public float speed = 7;
    private Vector2 target;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Run", false);
        edgePointLeft = GameObject.FindGameObjectWithTag("BossEdgeLeft").transform;
        edgePointRight = GameObject.FindGameObjectWithTag("BossEdgeRight").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<Boss>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector2.Distance(edgePointLeft.position, rb.position) <= Vector2.Distance(edgePointRight.position, rb.position))
        {
            target = new Vector2(edgePointLeft.position.x, rb.position.y);
        }
        else
        {
            target = new Vector2(edgePointRight.position.x, rb.position.y);
        }
        boss.LookAtPlayer();
        
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        if( rb.position.x - 1 < edgePointLeft.position.x || edgePointLeft.position.x < rb.position.x + 1 || edgePointRight.position.x == rb.position.x)
        {
            //animator.SetBool("Roll", false);
            animator.SetTrigger("PointReached");
        }
    }
}

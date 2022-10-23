using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_BlockOrAttack : StateMachineBehaviour
{
    private int shieldOrAttack;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        shieldOrAttack = Random.Range(1, 4);
        if (shieldOrAttack == 1)
        {
            animator.SetBool("IdleBlock", true);
        }
        else if(shieldOrAttack == 2)
        {
            animator.SetTrigger("Attack3");
        }
        else
        {
            animator.SetTrigger("Roll");
        }
    }
}

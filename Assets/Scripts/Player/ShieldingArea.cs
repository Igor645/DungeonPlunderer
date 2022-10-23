using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldingArea : MonoBehaviour
{
    private Health playerHealth;
    private Animator anim;
    // Start is called before the first frame update

    private void Awake()
    {
        anim = GetComponentInParent<Animator>();
        playerHealth = GetComponentInParent<Health>();    
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
            if (collider.GetComponent<AttackArea>() != null)
            {
                anim.SetTrigger("blocking");
            }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private Animator anim;
    private int damage = 1;

    public float damTimer;

    private void Update()
    {
        if (damTimer > 0)
        {
            damTimer -= Time.deltaTime;
        }
    }

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collider)
    {
        // && collider.GetComponent<PlayerShield>().shielding != true
        if (collider.GetComponent<Health>() != null && collider.tag == "Enemy")
        {
            if (damTimer <= 0)
            {
                Health health = collider.GetComponent<Health>();
                health.TakeDamage(damage);
                damTimer = 0.18f;
            }
        }        
    }
}

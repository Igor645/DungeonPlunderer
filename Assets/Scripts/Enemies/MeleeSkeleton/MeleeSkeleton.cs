using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSkeleton : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] private float attackCooldown;

    [Header("Range Specifications")]
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;

    [Header("Objects")]
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;

    public GameObject attackArea;

    private EnemyPatrol enemyPatrol;

    private float cooldownTimer = Mathf.Infinity;

    private Animator anim;
    private Health playerHealth;

    private GameObject player;

    private float timer = 0f;
    private float timeToAttack = 0.20f;
    private bool attacking = false;

    private enum MovementState { attack }

    [SerializeField] private AudioSource attackSoundEffect;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
        player = GameObject.FindGameObjectWithTag("Player");
        attackArea = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                anim.SetTrigger("meleeAttack");
            }
        }
        else
        {
            attackArea.SetActive(false);
        }

        if(enemyPatrol != null)
        {
            enemyPatrol.enabled = !PlayerInSight();
        }

        if (attacking)
        {
            timer += Time.deltaTime;

            if (timer >= timeToAttack)
            {
                timer = 0;
                attackArea.SetActive(false);
            }
        }
    }

    public bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
                                            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
                                            0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
        {
            playerHealth = hit.transform.GetComponent<Health>();
        }
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void DamagePlayer()
    {
        attacking = true;
        attackArea.SetActive(true);
        attackSoundEffect.Play();
    }
}

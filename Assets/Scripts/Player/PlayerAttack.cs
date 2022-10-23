using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    private Animator anim;
    private float cooldownTimer = Mathf.Infinity;

    [SerializeField]private GameObject attackArea;

    private bool attacking = false;

    private float timer = 0f;
    private float timeToAttack = 0.20f;

    private enum MovementState { attacking }

    [SerializeField] private AudioSource attackSoundEffect;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        attackArea = transform.GetChild(1).gameObject;
        attackArea.SetActive(attacking);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && cooldownTimer > attackCooldown)
        {
            anim.SetTrigger("attack");
            attackSoundEffect.Play();
        }

        if (attacking)
        {
            timer += Time.deltaTime;

            if(timer >= timeToAttack)
            {
                timer = 0;
                attacking = false;
                attackArea.SetActive(attacking);
            }
        }

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        cooldownTimer = 0;


        attacking = true;
        attackArea.SetActive(attacking);
    }
}

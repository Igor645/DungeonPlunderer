using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerJump : MonoBehaviour
{
    [Header("Jump Details")]
    public float jumpForce;
    public float jumpTime;
    private float jumpTimeCounter;
    private bool stoppedJumping;

    [Header("Ground Details")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float radOCircle;
    public bool grounded;

    [Header("Components")]
    private Rigidbody2D rb;
    private Animator myAnimator;
    private Health playerHealth;

    private enum MovementState { running }

    [SerializeField] private AudioSource jumpSoundEffect;
    [SerializeField] private AudioSource landingSoundEffect;

    private bool airborne;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        jumpTimeCounter = jumpTime;
        playerHealth = GetComponent<Health>();
    }

    private void Update()
    {
        //what it means to be grounded
        grounded = Physics2D.OverlapCircle(groundCheck.position, radOCircle, whatIsGround);

        if (!playerHealth.dead)
        {

            if (grounded)
            {
                jumpTimeCounter = jumpTime;
                myAnimator.ResetTrigger("jump");
                myAnimator.SetBool("falling", false);
            }

            //if we press the jump button
            if (Input.GetButtonDown("Jump") && grounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                stoppedJumping = false;
                //tell the animator to play jump anim
                myAnimator.SetTrigger("jump");
                jumpSoundEffect.Play();
            }

            //if we hold the jump button
            if (Input.GetButton("Jump") && !stoppedJumping && (jumpTimeCounter > 0))
            {
                //jump!!
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
                myAnimator.SetTrigger("jump");
            }

            //if we release the jump button
            if (Input.GetButtonUp("Jump"))
            {
                jumpTimeCounter = 0;
                stoppedJumping = true;
                myAnimator.SetBool("falling", true);
            }

            if (rb.velocity.y < 0)
            {
                myAnimator.SetBool("falling", true);
                myAnimator.ResetTrigger("jump");
            }

            grounded = Physics2D.OverlapCircle(groundCheck.position, radOCircle, whatIsGround);

            if (!grounded)
            {
                airborne = true;
            }

            if (grounded && airborne)
            {
                myAnimator.SetTrigger("landing");
                airborne = false;
                landingSoundEffect.Play();
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheck.position, radOCircle);
    }

    private void FixedUpdate()
    {
        HandleLayers();
    }

    private void HandleLayers()
    {
        if (!grounded)
        {
            myAnimator.SetLayerWeight(1, 1);
        }
        else
        {
            myAnimator.SetLayerWeight(1, 0);
        }
    }
}

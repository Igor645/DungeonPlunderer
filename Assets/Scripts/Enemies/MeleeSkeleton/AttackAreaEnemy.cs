using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAreaEnemy : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private int damage;

    public enum MovementState { shielding, damaged }

    private AudioSource[] audios;

    protected AudioSource blockSoundEffect;

    [SerializeField] private string audioNameAndInt;

    private void Start()
    {
        audios = GameObject.FindGameObjectWithTag("Audios").GetComponents<AudioSource>();

        blockSoundEffect = audios[0];
    }

    public float damTimer;

    public void Update()
    {
        if (damTimer > 0)
        {
            damTimer -= Time.deltaTime;
        }
    }
    // Start is called before the first frame update
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<Health>() != null)
        {
            if (damTimer <= 0 && collider.GetComponent<PlayerShield>().shielding != true)
            {
                Health health = collider.GetComponent<Health>();
                health.TakeDamage(damage);
                damTimer = 0.19f;
            }
            else if (collider.GetComponent<PlayerShield>().shielding == true)
            {
                anim = collider.GetComponent<Animator>();
                anim.SetTrigger("blocking");
                blockSoundEffect.Play();
            }
        }
    }
}

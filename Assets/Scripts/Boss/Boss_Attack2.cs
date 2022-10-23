using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Attack2 : MonoBehaviour
{
	public int attackDamage = 1;

	public Vector3 attackOffset;
	public float attackRange = 3f;
	public LayerMask attackMask;

    private enum MovementState { attacking }
    [SerializeField] private AudioSource bossAttack;
    
	public void Attack2()
	{
		bossAttack.Play();
		Vector3 pos = transform.position;
		pos += transform.right * attackOffset.x;
		pos += transform.up * attackOffset.y;

		Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
		if (colInfo != null && !Physics2D.GetIgnoreLayerCollision(10, 11) && !colInfo.GetComponent<PlayerShield>().shielding)
		{
			colInfo.GetComponent<Health>().TakeDamage(attackDamage);
		}
		else if(colInfo != null)
		{
			colInfo.GetComponent<Animator>().SetTrigger("blocking");
		}
	}

	void OnDrawGizmosSelected()
	{
		Vector3 pos = transform.position;
		pos += transform.right * attackOffset.x;
		pos += transform.up * attackOffset.y;

		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(pos, attackRange);
	}
}

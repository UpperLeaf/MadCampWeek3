using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashAttack : AbstractAttack
{
    private LayerMask enemies;
    private float attackRange;
   

    protected void Awake()
    {
        attackRange = 0.6f;
        enemies = LayerMask.NameToLayer("Enemy");
        isAttackable = true;
        coolTime = 0.8f;
    }

    public override void Attack(int damage, Transform attackPosition, PlayerState playerState)
    {
        playerState.attackDirection = (int)transform.localScale.x;
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, 1 << enemies);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponent<AbstractDamagable>().TakeDamage(damage, gameObject);
        }
        StartCoroutine("CoolTime");
    }

    IEnumerator CoolTime()
    {
        yield return new WaitForSeconds(coolTime);
        isAttackable = true;
    }
    
}

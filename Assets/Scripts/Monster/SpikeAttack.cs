using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeAttack : AbstractMonsterAttack
{
    private void Start()
    {
        enemies = LayerMask.NameToLayer("Player");
        isAttackable = true;
    }

    public override void Attack()
    {
        if (isAttackable)
        {
            isAttackable = false;
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(transform.position, 1f, 1 << enemies);
            for (int i = 0; i < enemiesToDamage.Length; i++)
                enemiesToDamage[i].GetComponent<AbstractDamagable>().TakeDamage(monsterStats.damage, gameObject);
            StartCoroutine("CoolTime");
        }
    }

    IEnumerator CoolTime()
    {
        yield return new WaitForSeconds(monsterStats.coolTime);
        isAttackable = true;
    }
}
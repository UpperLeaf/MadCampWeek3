using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultMonsterAttack : AbstractMonsterAttack
{
    private void Start()
    {
        cooltime = 0.2f;
        damage = 10;
        enemies = LayerMask.NameToLayer("Player");
        StartCoroutine("AttackCoroutine");
    }

    IEnumerator AttackCoroutine()
    {
        while (true)
        {
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(transform.position, 1f, 1 << enemies);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<AbstractDamagable>().TakeDamage(damage, gameObject);
            }
            yield return new WaitForSeconds(cooltime);
        }
    }
}

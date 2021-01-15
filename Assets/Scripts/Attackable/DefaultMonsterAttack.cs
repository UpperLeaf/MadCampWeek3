using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultMonsterAttack : AbstractAttack
{
    private void Start()
    {
        StartCoroutine("AttackCoroutine", 10);
    }

    public override void Attack(int damage)
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(transform.position , 1f, 1 << enemies);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            Debug.Log(enemiesToDamage[i]);
            enemiesToDamage[i].GetComponent<AbstractDamagable>().TakeDamage(10, gameObject);
        }
    }

    IEnumerator AttackCoroutine(int damage)
    {
        while (true)
        {
            Attack(damage);
            yield return new WaitForSeconds(0.2f);
        }
    }
}

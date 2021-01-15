using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashAttack : AbstractAttack
{
    private void Awake()
    {
        Transform[] transforms = GetComponentsInChildren<Transform>();
        attackPos = transforms[1];
        attackRange = 1f;
        enemies = LayerMask.NameToLayer("Enemy");
    }



    public override void Attack(int damage)
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, 1 << enemies);
        for(int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponent<AbstractDamagable>().TakeDamage(damage, gameObject);
        }
    }
}

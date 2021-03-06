﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashAttack : AbstractAttack
{
    [SerializeField]
    private GameObject slashEffect;

    private LayerMask enemies;
    
    [SerializeField]
    private float attackRange;
   
    protected void Awake()
    {
        attackRange = 0.7f;
        enemies = LayerMask.NameToLayer("Enemy");
        isAttackable = true;
        coolTime = 0.6f;
    }

    public override void Attack(int damage, Transform attackPosition, PlayerState playerState)
    {
        playerState.attackDirection = (int)transform.localScale.x;
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, 1 << enemies);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponent<AbstractDamagable>().TakeDamage(damage, gameObject);
            Transform enemies = enemiesToDamage[i].GetComponent<Transform>();
            Vector3 slashEffectPos = new Vector3(enemies.position.x, enemies.position.y, enemies.position.z + 1);
            slashEffect.transform.position = slashEffectPos;
            GameObject effect = Instantiate(slashEffect);
            Destroy(effect, effect.GetComponent<ParticleSystem>().main.duration + 1f);
        }
        StartCoroutine("CoolTime");
    }


    IEnumerator CoolTime()
    {
        yield return new WaitForSeconds(coolTime);
        isAttackable = true;
    }
    
}

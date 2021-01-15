using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultMonsterAttack : AbstractMonsterAttack
{
    private Animator _anim;

    private void Start()
    {
        idleTime = 1f;
        cooltime = 3f;
        damage = 10;
        enemies = LayerMask.NameToLayer("Player");
        _anim = GetComponent<Animator>();
        isAttackable = true;
    }

    public override void Attack()
    {
        if (isAttackable)
        {
            isAttackable = false;
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(transform.position, 1f, 1 << enemies);
            _anim.SetTrigger("Attack");
            _anim.SetBool("isStop", true);
            for (int i = 0; i < enemiesToDamage.Length; i++)
                enemiesToDamage[i].GetComponent<AbstractDamagable>().TakeDamage(damage, gameObject);
            StartCoroutine("CoolTime");
            StartCoroutine("IdleTime");
        }
    }

    IEnumerator IdleTime()
    {
        yield return new WaitForSeconds(idleTime);
        _anim.SetBool("isStop", false);
    }

    IEnumerator CoolTime()
    {
        yield return new WaitForSeconds(cooltime);
        isAttackable = true;
    }
}

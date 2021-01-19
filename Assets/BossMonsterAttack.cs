using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonsterAttack : MonoBehaviour
{
    [SerializeField]
    MonsterStats bossStats;

    private Animator _anim;
    private bool isAttackable;
    private LayerMask enemies;

    private void Start()
    {
        enemies = LayerMask.NameToLayer("Player");
        _anim = GetComponent<Animator>();
        isAttackable = true;


    }

    public void Attack()
    {
        if (isAttackable)
        {
            isAttackable = false;
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(transform.position, bossStats.attackField, 1 << enemies);
            _anim.SetTrigger("Attack");
            _anim.SetBool("isStop", true);
            for (int i = 0; i < enemiesToDamage.Length; i++)
                enemiesToDamage[i].GetComponent<AbstractDamagable>().TakeDamage(bossStats.damage, gameObject);
            StartCoroutine("CoolTime");
            StartCoroutine("IdleTime");
        }
    }

    IEnumerator IdleTime()
    {
        yield return new WaitForSeconds(bossStats.idleTime);
        _anim.SetBool("isStop", false);
    }

    IEnumerator CoolTime()
    {
        yield return new WaitForSeconds(bossStats.coolTime);
        isAttackable = true;
    }

}

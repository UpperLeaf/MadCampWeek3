using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : AbstractAttack
{
    [SerializeField]
    private GameObject swordEffect;

    private LayerMask enemies;

    [SerializeField]
    private float attackRange;

    private float attackOffset;
    protected void Awake()
    {
        attackRange = 2.5f;
        enemies = LayerMask.NameToLayer("Enemy");
        isAttackable = true;
        coolTime = 1f;
        attackOffset = 2f;
    }

    public override void Attack(int damage, Transform attackPosition, PlayerState playerState)
    {
        playerState.attackDirection = (int)transform.localScale.x;
        swordEffect.transform.localScale = PlayerManager.Instance.getPlayer().transform.localScale;

        Vector3 newAttackPostion = new Vector3(attackPosition.position.x + attackOffset * swordEffect.transform.localScale.x, attackPosition.position.y, attackPosition.position.z);

        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(newAttackPostion, attackRange, 1 << enemies);
        for (int i = 0; i < enemiesToDamage.Length; i++)
            enemiesToDamage[i].GetComponent<AbstractDamagable>().TakeDamage(damage * 2, gameObject);

        swordEffect.transform.position = newAttackPostion;

        GameObject effect = Instantiate(swordEffect);
        Destroy(effect, 0.5f);
        StartCoroutine("CoolTime");
    }

    IEnumerator CoolTime()
    {
        yield return new WaitForSeconds(coolTime);
        isAttackable = true;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballAttack : AbstractAttack
{
    [SerializeField]
    private GameObject fireball;

    private GameObject _instanceFireball;

    private void Start()
    {
        isAttackable = true;
        coolTime = 1;
    }

    public void SetFireball(GameObject fireball)
    {
        this.fireball = fireball;
    }

    public override void Attack(int damage, Transform attackPosition, PlayerState playerState)
    {
        if (isAttackable && !playerState.isAttacking && !playerState.isCast)
        {
            isAttackable = false;
            playerState.isCast= true;
            playerState.attackDirection = (int)transform.localScale.x;
            fireball.transform.position = attackPosition.position;
            fireball.transform.localScale = PlayerManager.Instance.getPlayer().transform.localScale;
            _instanceFireball = Instantiate(fireball);
            StartCoroutine("CoolTime");
        }
    }

    IEnumerator CoolTime()
    {
        yield return new WaitForSeconds(coolTime);
        isAttackable = true;
    }


}

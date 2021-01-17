using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarknessAttack : AbstractAttack
{
    [SerializeField]
    private GameObject darkness;

  
    private void Start()
    {
        isAttackable = true;
        coolTime = 10f;
    }

    public override void Attack(int damage, Transform attackPosition, PlayerState playerState)
    {
        playerState.attackDirection = (int)transform.localScale.x;
        darkness.transform.position = attackPosition.position;
        GameObject darknessInstance = Instantiate(darkness);
        darknessInstance.GetComponent<Darkness>().SetDamage(damage / 3);
        StartCoroutine("CoolTime");
    }

    IEnumerator CoolTime()
    {
        yield return new WaitForSeconds(coolTime);
        isAttackable = true;
    }
}

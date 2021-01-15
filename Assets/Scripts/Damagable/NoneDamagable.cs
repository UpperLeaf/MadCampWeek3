using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoneDamagable : AbstractDamagable
{
    public override void TakeDamage(int damage, GameObject attacker)
    {
        Debug.Log(damage + " 의 피해를 입었습니다.");
    }
}



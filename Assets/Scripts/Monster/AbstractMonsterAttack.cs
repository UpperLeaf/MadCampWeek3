using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractMonsterAttack : MonoBehaviour
{
    protected float cooltime;

    protected float idleTime;

    protected int damage;

    protected LayerMask enemies;

    protected bool isAttackable;

    public abstract void Attack();
}

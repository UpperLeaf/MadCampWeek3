using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractMonsterAttack : MonoBehaviour
{
    [SerializeField]
    protected float cooltime;

    [SerializeField]
    protected float idleTime;

    [SerializeField]
    protected int damage;

    protected LayerMask enemies;

    protected bool isAttackable;

    public abstract void Attack();
}

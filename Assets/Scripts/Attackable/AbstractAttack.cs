using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractAttack : MonoBehaviour
{
    [SerializeField]
    protected float coolTime;
    
    [SerializeField]
    protected bool isAttackable;

    [SerializeField]
    public AttackType attackType;

    [SerializeField]
    public DistanceType distanceType;

    public abstract void Attack(int damage, Transform attackPosition, PlayerState playerState);

    public bool IsAttackable()
    {
        return isAttackable;
    }

    public void SetAttackable(bool attackable)
    {
        isAttackable = attackable;
    }

    public enum AttackType
    {
        ATTACK,
        CAST,
        DASH
    }

    public enum DistanceType
    {
        NEAR,
        FAR,
    }
}

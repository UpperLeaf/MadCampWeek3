using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractAttack : MonoBehaviour
{
    [SerializeField]
    protected float coolTime;
    
    [SerializeField]
    protected bool isAttackable;

    public abstract void Attack(int damage, Transform attackPosition, PlayerState playerState);
}

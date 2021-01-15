using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractAttack : MonoBehaviour, IAttackable
{
    protected Transform attackPos;
    protected float attackRange;
    
    public LayerMask enemies;


    public abstract void Attack(int damage);
}

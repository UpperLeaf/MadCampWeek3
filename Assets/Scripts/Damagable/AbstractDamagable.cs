using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class AbstractDamagable : MonoBehaviour
{
    [SerializeField]
    protected float _hp;

    [SerializeField]
    protected int _hitTime;

    [SerializeField]
    protected bool _isDied;

    protected Animator _anim;
    public abstract void TakeDamage(int damage, GameObject attacker);

}

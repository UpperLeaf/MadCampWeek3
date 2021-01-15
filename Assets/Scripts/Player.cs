using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : AbstractDamagable
{
    private PlayerState playerState;

    [SerializeField]
    private int _hp;

    private void Awake()
    {
        playerState = GetComponent<PlayerState>();
    }

    public int HP
    {
        get => _hp;
    }

    private void setHp(int value)
    {
        if (_hp - value > 0)
        {
            _hp -= value;
            playerState.isDamaged = true;
        }
        else
        {
            _hp = 0;
            playerState.isDied = true;
        }
    }

    public override void TakeDamage(int damage, GameObject attacker)
    {
        //피격중인 상태가 아닐때만 데미지를 받을 수 있다.
        if (!playerState.isDamaged)
        {
            setHp(damage);
        }
    }
}

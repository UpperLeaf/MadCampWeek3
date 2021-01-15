using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    private AbstractAttack xAttack;

    [SerializeField]
    private AbstractAttack aAttack; 

    [SerializeField]
    private Transform xAttackPos;

    [SerializeField]
    private Transform aAttackPos;


    private PlayerState _playerState;

    void Awake()
    {
        xAttack = gameObject.AddComponent<SlashAttack>();
        _playerState = GetComponent<PlayerState>();
    }

    public void AttackByInputX(int damage)
    {
        xAttack.Attack(damage, xAttackPos, _playerState);
    }


    public void AttackByInputA(int damage)
    {
        //aAttack.Attack(damage);
    }
}

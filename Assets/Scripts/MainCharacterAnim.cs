using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterAnim : MonoBehaviour
{
    private PlayerState _playerState;

    private Animator _animator;
    private bool _attacking;
    private bool _damaged;
    private bool _died;
    
    private void Awake()
    {
        _playerState = GetComponent<PlayerState>();
        _animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        _animator.SetBool("isWalking", _playerState.isWalking);
        _animator.SetBool("isIdling", _playerState.isIdling);        
        _animator.SetBool("isJumping", _playerState.isJumping);
        
        if (_playerState.isAttacking && !_attacking)
            Attack();

        if (_playerState.isDamaged && !_damaged)
            Damaged();

        if (_playerState.isDied && !_died)
        {
            Died();
        }
    }

    public void Died()
    {
        _died = true;
        _animator.SetTrigger("Died");
    }

    public void DiedFinish()
    {
        //TODO 죽었을때 처리
    }

    public void Damaged()
    {
        _damaged = true;
        _animator.SetTrigger("Damaged");
    }

    public void DamagedFinish()
    {
        _damaged = false;
        _playerState.isDamaged = false;
    }

    public void Attack()
    {
        _attacking = true;
        _animator.SetTrigger("Attack");
    }

    public void AttackFinish()
    {
        _attacking = false;
        _playerState.isAttacking = false;
    }
}

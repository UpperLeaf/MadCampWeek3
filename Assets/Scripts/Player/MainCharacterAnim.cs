using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterAnim : MonoBehaviour
{
    private PlayerState _playerState;

    private Animator _animator;


    [SerializeField]
    private GameObject dashEffect;
    private Transform _dashPos;

    private GameObject _instanceDashEffect;

    [SerializeField]
    private bool _attacking;
    [SerializeField]
    private bool _damaged;
    [SerializeField]
    private bool _died;
    [SerializeField]
    private bool _dashed;
    [SerializeField]
    private bool _cast;

    private void Start()
    {
        _playerState = GetComponent<PlayerState>();
        _animator = GetComponent<Animator>();
        Transform[] transforms = GetComponentsInChildren<Transform>();
        foreach (Transform pos in transforms)
        {
            if (pos.name.Equals("dashEffectPos"))
            {
                _dashPos = pos;
                break;
            }
        }

    }

    void Update()
    {
        _animator.SetBool("isWalking", _playerState.isWalking);
        _animator.SetBool("isIdling", _playerState.isIdling);        
        _animator.SetBool("isJumping", _playerState.isJumping);
        
        if (_playerState.isAttacking && !_attacking)
            Attack();

        if (_playerState.isCast && !_cast)
            Cast();

        if (_playerState.isDamaged && !_damaged)
            Damaged();

        if (_playerState.isDied && !_died)
        {
            Died();
        }

        if (_playerState.isDashing && !_dashed)
        {
            Dash();
        }
    }

    public void Died()
    {
        _died = true;
        _playerState.isDied = true;
        _animator.SetTrigger("Died");
    }

    public void DiedFinish()
    {
        //TODO DiedFinish
        Destroy(gameObject);
    }

    public void Damaged()
    {
        _damaged = true;
        _attacking = false;
        _playerState.isAttacking = false;
        _playerState.isIdling = true;
        _playerState.isJumping = false;
        _animator.SetTrigger("Damaged");
    }

    public void DamagedStart()
    {
        float direction = transform.localScale.x;
        StartCoroutine("HitCoroutine", direction * -1);
    }

    IEnumerator HitCoroutine(float direction)
    {
        for (int i = 0; i < 30; i++)
        {
            transform.Translate(new Vector2(direction / 30, 0));
            yield return null;
        }
    }

    private void Cast()
    {
        _cast = true;
        _animator.SetTrigger("Cast");
    }

    public void CastFinish()
    {
        _cast = false;
        _playerState.isCast = false;
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

    public void Dash()
    {
        _dashed = true;
        _animator.SetTrigger("Dash");
        _instanceDashEffect = Instantiate(dashEffect, _dashPos);
    }

    public void DashFinish()
    {
        _dashed = false;
        _playerState.isDashing = false;
        Destroy(_instanceDashEffect);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterAnim : MonoBehaviour
{

    private AttackManager _attackManager;
    private Animator _animator;
    private PlayerState _playerState;

    [SerializeField]
    private GameObject dashEffect;
    private Transform _dashPos;

    private GameObject _instanceDashEffect;

    [SerializeField]
    private bool _damaged;
    [SerializeField]
    private bool _died;
    [SerializeField]
    private bool _dashed;
    [SerializeField]
    private bool _cast;

    private KeyCode nowKey;

    private void Start()
    {
        _playerState = GetComponent<PlayerState>();
        _animator = GetComponent<Animator>();
        _attackManager = GetComponent<AttackManager>();

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

    public void DamagedFinish()
    {
        _damaged = false;
        _playerState.isDamaged = false;
    }

    public void Attack(KeyCode key)
    {
        switch (key) {
            case KeyCode.X:
                if (_attackManager.isAttackAbleX())
                {
                    _animator.SetTrigger("Attack");
                    _playerState.isAttacking = true;
                    _playerState.attackDirection = (int)transform.localScale.x;
                    nowKey = key;
                }
                break;
            case KeyCode.A:
                if (_attackManager.isAttackAbleA())
                {
                    _animator.SetTrigger("Cast");
                    _playerState.isCast = true;
                    _playerState.attackDirection = (int)transform.localScale.x;
                    nowKey = key;
                }
                break;
            case KeyCode.S:
                if (_attackManager.isAttackAbleS())
                {
                    _animator.SetTrigger("Cast");
                    _playerState.isCast = true;
                    _playerState.attackDirection = (int)transform.localScale.x;
                    nowKey = key;
                }
                break;
        }
    }

    public void AttackEvent()
    {
        switch (nowKey)
        {
            case KeyCode.X:
                _attackManager.AttackByInputX();
                break;
            case KeyCode.A:
                _attackManager.AttackByInputA();
                break;
            case KeyCode.S:
                _attackManager.AttackByInputS();
                break;
        }
    }

    public void AttackFinish()
    {
        _playerState.isAttacking = false;
    }

    public void CastFinish()
    {
        _playerState.isCast = false;
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

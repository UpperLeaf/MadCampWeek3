using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterAnim : MonoBehaviour
{

    private AttackManager _attackManager;

    private Animator _animator;
    private PlayerState _playerState;

    [SerializeField]
    private GameObject _dash;
    
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
        _dash = Instantiate(_dash, gameObject.transform);    
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
        Invoke("DamagedFinish", 0.5f);
    }



    public void DamagedFinish()
    {
        _damaged = false;
        _playerState.isDamaged = false;
    }

    public void Attack(KeyCode key)
    {
        if (_attackManager.isAttackAble(key))
        {
            AbstractAttack.AttackType attackType = _attackManager.getAttackType(key);
            switch (attackType)
            {
                case AbstractAttack.AttackType.ATTACK:
                    _animator.SetTrigger("Attack");
                    Invoke("AttackFinish", 0.4f);
                    _playerState.isAttacking = true;
                    break;
                case AbstractAttack.AttackType.CAST:
                    _animator.SetTrigger("Cast");
                    Invoke("CastFinish", 0.7f);
                    _playerState.isCast = true;
                    break;
            }
            _playerState.attackDirection = (int)transform.localScale.x;
            nowKey = key;
        }
    }

    public void AttackEvent()
    {
        _attackManager.AttackByInput(nowKey);
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
        Dash script = _dash.GetComponent<Dash>();
        bool isDashAble = script.IsDashAble();

        if (isDashAble)
        {
            _playerState.isDashing = true;
            
            if(script.IsNoneDamaged())
                gameObject.tag = "NoneDamage";
            script.ExecuteDash();
            Invoke("DashFinish", 0.5f);
            _animator.SetTrigger("Dash");
            _instanceDashEffect = Instantiate(dashEffect, _dashPos);
        }
    }

    public void DashFinish()
    {
        Dash script = _dash.GetComponent<Dash>();
        if (script.IsNoneDamaged())
            gameObject.tag = "Player";
        _playerState.isDashing = false;
        Destroy(_instanceDashEffect);
    }
}

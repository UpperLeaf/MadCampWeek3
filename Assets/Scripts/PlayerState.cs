using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [SerializeField]
    private bool _isWalking = false;
    [SerializeField]
    private bool _isIdling = true;
    [SerializeField]
    private bool _isJumping = false;
    [SerializeField]
    private bool _isAttacking = false;
    [SerializeField]
    private bool _isDamaged = false;
    [SerializeField]
    private bool _isDied = false;

    private int _attackDirection;

    public static int RIGHT_DIRECTION = 0;
    public static int LEFT_DIRECTION = 1;

    public bool isDied
    {
        get => _isDied;
        set
        {
            _isDied = value;
        }
    }

    public int attackDirection
    {
        get => _attackDirection;
        set => _attackDirection = value;
    }

    public bool isDamaged
    {
        get => _isDamaged;
        set
        {
            _isDamaged = value;
        }
    }

    public bool isWalking
    {
        get => _isWalking;
        set
        {
            _isWalking = value;
            _isIdling = !value;
        }
    }
    public bool isIdling
    {
        get => _isIdling;
        set
        {
            _isWalking = !value;
            _isIdling = value;
        }
    }
    public bool isJumping
    {
        get => _isJumping;
        set
        {
            _isJumping = value;
        }
    }

    public bool isAttacking
    {
        get => _isAttacking;
        set => _isAttacking = value;
    }
}

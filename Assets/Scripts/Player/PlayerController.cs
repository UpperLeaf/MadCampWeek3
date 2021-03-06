﻿using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField, Tooltip("Acceleration while grounded.")]
    float walkAcceleration = 75;

    [SerializeField, Tooltip("Acceleration while in the air.")]
    float airAcceleration = 20;

    [SerializeField, Tooltip("Deceleration while in the air.")]
    float airDeceleration = 30;

    [SerializeField, Tooltip("Deceleration applied when character is grounded and not attempting to move.")]
    float groundDeceleration = 70;

    private Player player;

    private PlayerState _playerState;

    private BoxCollider2D boxCollider;

    private MainCharacterAnim mainCharacterAnim;

    private LayerMask floor;
    private LayerMask wall;

    private Vector2 velocity;

    private PlayerSkillManager skillManager;

    private bool grounded;

    private float acceleration;
    private float deceleration;

    // 다른 오브젝트에서 플레이어 관련 정보에 접근할 수 있게 하기
    public static GameObject GetGameObject()
    {
        return GameObject.FindGameObjectWithTag("Player");
    }
    public static Transform GetTranform()
    {
        return GameObject.FindGameObjectWithTag("Player").transform;
    }
    public static PlayerController GetController()
    {
        return GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    public static Animator GetAnimator()
    {
        return GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }


    private void Start()
    {
        player = GetComponent<Player>();
        boxCollider = GetComponent<BoxCollider2D>();
        _playerState = GetComponent<PlayerState>();
        mainCharacterAnim = GetComponent<MainCharacterAnim>();
        skillManager = GetComponent<PlayerSkillManager>();
        acceleration = grounded ? walkAcceleration : airAcceleration;
        deceleration = grounded ? groundDeceleration : airDeceleration;

        floor = LayerMask.NameToLayer("Floor");
        wall = LayerMask.NameToLayer("RealWall");
    }
    private void Update()
    {
        if(!_playerState.isDied)
            MoveHorizontal();
        CollisionCheck();
        CollisionRealWall();
        if (!_playerState.isDamaged && !_playerState.isDied)
        {
            Jump();
            Dash();
            Attack();
        }
        Gravity();
    }

    private void MoveHorizontal()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        float _speed = _playerState.isDashing ? player.dashSpeed : player.speed;

        if (_playerState.isDamaged || _playerState.isCast)
            moveInput = 0;
        

        if ((moveInput != 0 && !_playerState.isAttacking))
        {
            transform.localScale = new Vector3(1 * moveInput, 1, 1);
            velocity.x = Mathf.MoveTowards(velocity.x, _speed * moveInput, acceleration * Time.deltaTime);
            _playerState.isWalking = true;
        }
        else if (moveInput != 0 && (_playerState.isJumping && _playerState.isAttacking) == true)
        {
            velocity.x = Mathf.MoveTowards(velocity.x, _speed * moveInput, acceleration * Time.deltaTime);
            _playerState.isWalking = true;
        }
        else
        {
            velocity.x = Mathf.MoveTowards(velocity.x, 0, deceleration * Time.deltaTime);
            _playerState.isWalking = false;
        }

        transform.Translate(velocity * Time.deltaTime);
    }

    private void CollisionRealWall()
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, boxCollider.size, 0, (1 << wall));
        foreach (Collider2D hit in hits)
        {
            ColliderDistance2D colliderDistance = hit.Distance(boxCollider);

            if (colliderDistance.isOverlapped)
            {
                transform.Translate(colliderDistance.pointA - colliderDistance.pointB);
                if (Vector2.Angle(colliderDistance.normal, Vector2.up) < 60 && velocity.y < 0)
                {
                    grounded = true;
                    _playerState.isJumping = false;
                }
            }
        }
    }

    private void CollisionCheck()
    {
        grounded = false;

        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, boxCollider.size, 0, (1 << floor) | (1 << wall));

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Platform") && hit.transform.position.y > transform.position.y - 1.03f)
            {
                continue;
            }

            ColliderDistance2D colliderDistance = hit.Distance(boxCollider);

            if (colliderDistance.isOverlapped)
            {
                transform.Translate(colliderDistance.pointA - colliderDistance.pointB);
                if (Vector2.Angle(colliderDistance.normal, Vector2.up) < 60 && velocity.y < 0)
                {
                    grounded = true;
                    _playerState.isJumping = false;
                }
            }
        }
    }

   private void Jump()
    {
        if (grounded && !_playerState.isAttacking && !_playerState.isDamaged)
        {
            velocity.y = 0;
            if (Input.GetButton("Jump"))
            {
                velocity.y = Mathf.Sqrt(2 * player.jumpHeight * Mathf.Abs(Physics2D.gravity.y));
                _playerState.isJumping = true;
            }
        }
    }

    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.Z) && checkDashable() && skillManager.canUseDash)
        {
            mainCharacterAnim.Dash();
        }
    }

    public void Gravity()
    {
        velocity.y += Physics2D.gravity.y * Time.deltaTime;
    }

    public void Attack()
    {
        bool isAttackable = checkAttackable();
        if (isAttackable)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                mainCharacterAnim.Attack(KeyCode.X);
            }
            else if (!_playerState.isJumping && Input.GetKeyDown(KeyCode.A))
            {
                mainCharacterAnim.Attack(KeyCode.A);
            }
            else if (!_playerState.isJumping && Input.GetKeyDown(KeyCode.S))
            {
                mainCharacterAnim.Attack(KeyCode.S);
            }
            else if(!_playerState.isJumping && Input.GetKeyDown(KeyCode.D))
            {
                mainCharacterAnim.Attack(KeyCode.D);
            }
        }
    }
    
    private bool checkDashable()
    {
        return _playerState.isWalking && !(_playerState.isAttacking || _playerState.isCast || _playerState.isJumping || _playerState.isDashing);
    }

    private bool checkAttackable()
    {
        return !(_playerState.isDamaged || _playerState.isAttacking || _playerState.isCast);
    }

    public void Shake()
    {

    }

}

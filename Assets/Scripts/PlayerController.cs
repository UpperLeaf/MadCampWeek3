using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField, Tooltip("Max speed, in units per second, that the character moves.")]
    float speed = 4;

    [SerializeField, Tooltip("Max speed, in units per second, that the character moves.")]
    float dashSpeed = 6;

    [SerializeField, Tooltip("Acceleration while grounded.")]
    float walkAcceleration = 75;

    [SerializeField, Tooltip("Acceleration while in the air.")]
    float airAcceleration = 20;

    [SerializeField, Tooltip("Deceleration while in the air.")]
    float airDeceleration = 30;

    [SerializeField, Tooltip("Deceleration applied when character is grounded and not attempting to move.")]
    float groundDeceleration = 70;

    [SerializeField, Tooltip("Max height the character will jump regardless of gravity")]
    float jumpHeight = 3;


    private PlayerState _playerState;

    private BoxCollider2D boxCollider;

    
    private AttackManager attackManager;
    
    private Vector2 velocity;

    private bool grounded;

    private float acceleration;
    private float deceleration;

  
    
    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        _playerState = GetComponent<PlayerState>();
        attackManager = GetComponent<AttackManager>();

        acceleration = grounded ? walkAcceleration : airAcceleration;
        deceleration = grounded ? groundDeceleration : airDeceleration;

        attackManager.gameObject.SetActive(true);
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        float moveInput = MoveHorizontal();
        CollisionCheck();
        if (!_playerState.isDamaged)
        {
            Jump();
            Dash();
            Attack((int)moveInput);
        }       
        Gravity();
    }

    private float MoveHorizontal()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        float _speed = _playerState.isDashing ? dashSpeed : speed;

        if (_playerState.isDamaged)
            moveInput = 0;
        

        if ((moveInput != 0 && !_playerState.isAttacking))
        {
            transform.localScale = new Vector3(1 * moveInput, 1, 1);
            velocity.x = Mathf.MoveTowards(velocity.x, _speed * moveInput, acceleration * Time.deltaTime);
            _playerState.isWalking = true;
        }
        else if (moveInput != 0 && (_playerState.isJumping && _playerState.isAttacking) == true)
        {
            if (_playerState.attackDirection != 0)
                transform.localScale = new Vector3(_playerState.attackDirection, 1, 1);

            velocity.x = Mathf.MoveTowards(velocity.x, _speed * moveInput, acceleration * Time.deltaTime);
            _playerState.isWalking = true;
        }
        else
        {
            velocity.x = Mathf.MoveTowards(velocity.x, 0, deceleration * Time.deltaTime);
            _playerState.isWalking = false;
        }

        transform.Translate(velocity * Time.deltaTime);
        return moveInput;
    }
    private void CollisionCheck()
    {
        grounded = false;

        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, boxCollider.size, 0);

        foreach (Collider2D hit in hits)
        {
            if (hit.Equals(boxCollider))
                continue;

            ColliderDistance2D colliderDistance = hit.Distance(boxCollider);

            if (colliderDistance.isOverlapped)
            {
                transform.Translate(colliderDistance.pointA - colliderDistance.pointB);
                if (Vector2.Angle(colliderDistance.normal, Vector2.up) < 90 && velocity.y < 0)
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
                velocity.y = Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(Physics2D.gravity.y));
                _playerState.isJumping = true;
            }
        }
    }

    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.Z) && _playerState.isWalking && !_playerState.isAttacking && !_playerState.isJumping)
        {
            _playerState.isDashing = true;
        }
    }

    public void Gravity()
    {
        velocity.y += Physics2D.gravity.y * Time.deltaTime;
    }

    public void Attack(int direction)
    {
        if (!_playerState.isDamaged && Input.GetKeyDown(KeyCode.X))
        {
            attackManager.AttackByInputX(10);
        }
    }
}

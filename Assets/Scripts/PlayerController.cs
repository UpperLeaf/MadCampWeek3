using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PlayerController : MonoBehaviour, IAttackable
{
    [SerializeField, Tooltip("Max speed, in units per second, that the character moves.")]
    float speed = 4;

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

    private BoxCollider2D boxCollider;

    private Vector2 velocity;

    private bool grounded;

    private float acceleration;
    private float deceleration;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        
        acceleration = grounded ? walkAcceleration : airAcceleration;
        deceleration = grounded ? groundDeceleration : airDeceleration;
    }

    private void Update()
    {
        MoveHorizontal();
        CollisionCheck();
        Jump();
        Attack();
        Gravity();
    }

    public void MoveHorizontal()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");

        if (moveInput != 0)
            velocity.x = Mathf.MoveTowards(velocity.x, speed * moveInput, acceleration * Time.deltaTime);
        else
            velocity.x = Mathf.MoveTowards(velocity.x, 0, deceleration * Time.deltaTime);

        transform.Translate(velocity * Time.deltaTime);
    }
    public void CollisionCheck()
    {
        grounded = false;
        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, boxCollider.size, 0);

        foreach (Collider2D hit in hits)
        {
            if (hit == boxCollider)
                continue;

            ColliderDistance2D colliderDistance = hit.Distance(boxCollider);

            if (colliderDistance.isOverlapped)
            {
                transform.Translate(colliderDistance.pointA - colliderDistance.pointB);
            }

            if (Vector2.Angle(colliderDistance.normal, Vector2.up) < 90 && velocity.y < 0)
            {
                grounded = true;
            }
        }
    }
    public void Jump()
    {
        if (grounded)
        {
            velocity.y = 0;
            if (Input.GetButton("Jump"))
            {
                velocity.y = Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(Physics2D.gravity.y));
            }
        }
    }

    public void Gravity()
    {
        velocity.y += Physics2D.gravity.y * Time.deltaTime;
    }

    public void Attack()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {

        }
    }
}

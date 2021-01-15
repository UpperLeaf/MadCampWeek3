using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField, Tooltip("Max speed, in units per second, that the character moves.")]
    float speed = 9;

    [SerializeField, Tooltip("Acceleration while grounded.")]
    float walkAcceleration = 75;

    [SerializeField, Tooltip("Acceleration while in the air.")]
    float airAcceleration = 30;

    [SerializeField, Tooltip("Deceleration applied when character is grounded and not attempting to move.")]
    float groundDeceleration = 70;

    [SerializeField, Tooltip("Max height the character will jump regardless of gravity")]
    float jumpHeight = 4;

    private BoxCollider2D boxCollider;

    private Vector2 velocity;

    private bool grounded;

    // 체력 바 프리펩
    public HitPoints hitPoints;
    public HealthBar healthBarPrefab;
    public float maxHitPoints;
    public float startingHitPoints;

    HealthBar healthBar;

    private void Start()
    {
        hitPoints.value = startingHitPoints;

        // 프리팹 초기화 및 healthBar에 healthBarPrefab 주소 값(참조) 저장
        // TODO Update에서 healthBar 이용해서 HP 와 UI 연동되도록 하기 
        healthBar = Instantiate(healthBarPrefab);
        healthBar.player = this;

    }

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        velocity.x = Mathf.MoveTowards(velocity.x, speed * moveInput, walkAcceleration * Time.deltaTime);
        transform.Translate(velocity * Time.deltaTime);


        float acceleration = grounded ? walkAcceleration : airAcceleration;
        float deceleration = grounded ? groundDeceleration : 0;


        if (moveInput != 0)
            velocity.x = Mathf.MoveTowards(velocity.x, speed * moveInput, walkAcceleration * Time.deltaTime);
        else
            velocity.x = Mathf.MoveTowards(velocity.x, 0, groundDeceleration * Time.deltaTime);

        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, boxCollider.size, 0);

        grounded = false;

        foreach (Collider2D hit in hits)
        {
            if (hit == boxCollider)
                continue;

            // TODO 몬스터와의 충돌 시 데미지 입음
            if (hit.gameObject.tag == "Monster")
            {
                continue;
            }




            ColliderDistance2D colliderDistance = hit.Distance(boxCollider);

            if (colliderDistance.isOverlapped)
            {
                transform.Translate(colliderDistance.pointA - colliderDistance.pointB);
            }

            if(Vector2.Angle(colliderDistance.normal, Vector2.up) < 90 && velocity.y < 0)
            {
                grounded = true;
            }
        }

        if(grounded)
        {
            velocity.y = 0;
            if (Input.GetButton("Jump"))
            {
                velocity.y = Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(Physics2D.gravity.y));
            }
        }
        velocity.y += Physics2D.gravity.y * Time.deltaTime;


        if (Input.GetKeyDown(KeyCode.I))
        {
            AdjustHitPoints(5);
            Debug.Log("체력 증가");
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            AdjustHitPoints(-5);
            Debug.Log("체력 감소");
        }


    }


    public void AdjustHitPoints(int amount)
    {

        float newValue = hitPoints.value + amount;

        if (newValue <= maxHitPoints && newValue >= 1.0f) hitPoints.value = newValue;

        if (newValue < 1.0f) Debug.Log("플레이어가 죽어야 함");

        Debug.Log("Adjusted hitpoints by: " + amount + ". New Value: " + hitPoints.value);

    }
}

using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class MonsterController : MonoBehaviour
{

    [SerializeField, Tooltip("땅에서의 가속도 (왜 필요한거지?)")]
    float walkAcceleration = 10f;

    private BoxCollider2D boxCollider;

    private Vector2 velocity;

    [SerializeField]
    private bool grounded;

    Animator anim;

    // 이동 방향
    bool movingRight;

    // 몬스터의 스탯
    [SerializeField, Tooltip("최대 체력")]
    int maxHp = 100;

    [SerializeField, Tooltip("최대 공격력")]
    int maxStr = 10;

    [SerializeField, Tooltip("최대 속도")]
    float maxSpeed = 2f;

    // 몬스터의 현재 스탯
    [SerializeField, Tooltip("현재 체력")]
    int hp;

    [SerializeField, Tooltip("현재 공격력")]
    int str;

    [SerializeField, Tooltip("현재 속도")]
    float speed;

    [SerializeField, Tooltip("시야")]
    float sight;

    [SerializeField, Tooltip("공격 범위")]
    float attackField;

    [SerializeField, Tooltip("플레이어를 따라가고 있는지 여부")]
    bool isFollowingPlayer;

    [SerializeField, Tooltip("플레이어가 오른쪽에 있는지 여부")]
    bool isPlayerRight;

    [SerializeField]
    protected RectTransform hpBar;

    public GameObject prefHpBar;
    public GameObject canvas;


    [SerializeField] LayerMask playerLayerMask;

    float height = 1.0f;

    Vector2 playerPosition;

    private void Start()
    {
        Debug.Log("몬스터 등장!");
        anim = GetComponent<Animator>();
        hp = maxHp;
        str = maxStr;
        speed = maxSpeed;
        movingRight = anim.GetBool("isRightMoving");
        playerLayerMask = LayerMask.NameToLayer("Player");
        sight = 3.0f;
        attackField = 0.5f;
        isFollowingPlayer = false;
        isPlayerRight = true;
        grounded = false;

    }
    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void GoRight()
    {
        velocity.x = Mathf.MoveTowards(velocity.x, speed, walkAcceleration * Time.deltaTime);
        anim.SetBool("isRightMoving", true);
        transform.Translate(velocity * Time.deltaTime);
    }

    private void GoLeft()
    {
        velocity.x = Mathf.MoveTowards(velocity.x, (-1) * speed, walkAcceleration * Time.deltaTime);
        anim.SetBool("isRightMoving", false);
        transform.Translate(velocity * Time.deltaTime);
    }

    private void Stop()
    {
        velocity.x = 0;
        transform.Translate(velocity * Time.deltaTime);
    }

    private void StopToFall()
    {
        velocity.y = 0;
        transform.Translate(velocity * Time.deltaTime);
    }
    private void Fall()
    {
        velocity.y += Physics2D.gravity.y * Time.deltaTime;
        transform.Translate(velocity * Time.deltaTime);
    }

    private void Update()
    {
        Vector2 position = transform.position;
        Vector2 frontVec = anim.GetBool("isRightMoving") ? Vector2.right : Vector2.left;

        if (anim.GetBool("isAttacking"))
        {
            Stop();
        }
        else if (isFollowingPlayer)
        {
            Debug.Log("플레이어를 따라가는중");

            if (playerPosition.x < position.x)
                GoLeft();
            else GoRight();

            //transform.Translate((playerPosition - position) * Time.deltaTime);
            //if (isPlayerRight) GoRight(); else GoLeft();
        }
        else if (anim.GetBool("isRightMoving") && anim.GetBool("isWalking"))
        {
            GoRight();       
        }
        else if (anim.GetBool("isWalking"))
        {
            GoLeft();
        }
        else
        {
            Stop();
        }

        // 땅과 닿아있지 않으면 떨어지게 하기
        //if (grounded) StopToFall(); else Fall();



        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, boxCollider.size, 0);

        foreach (Collider2D hit in hits)
        {
            // 자기 스스로와의 충돌을 제외
            if (hit == boxCollider)
                continue;

            // 몬스터와의 충돌을 제외
            if (hit.gameObject.tag == "Monster")
                continue;
            
            // 플레이어와의 충돌을 제외
            if (hit.gameObject.tag == "Player")
                continue;
            

            // TODO 플레이어의 무기/스킬/마법 등과 충돌 시 데미지 입음
            if (hit.gameObject.tag == "Weapon")
            {
                // health -= hit.gameObject.str = 

                // TODO 몬스터 바운스(밀려남)/프리즈(경직) 효과

                if (hp < 1)
                {
                    // TODO 몬스터 죽음
                }
            }

            ColliderDistance2D colliderDistance = hit.Distance(boxCollider);

            if (colliderDistance.isOverlapped)
                transform.Translate(colliderDistance.pointA - colliderDistance.pointB);


            // 땅과 닿아있지 있는지 체크
            if (Vector2.Angle(colliderDistance.normal, Vector2.up) < 90 && velocity.y < 0)
            {
                grounded = true;
            }
            else grounded = false;

        }

        // 지형 체크 
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 0.5f, 0));
        RaycastHit2D rayHitGround = Physics2D.Raycast(position + frontVec, Vector3.down);

        if (rayHitGround.collider == null)
        {
            movingRight = !movingRight;
            anim.SetBool("isRightMoving", movingRight);
        }


        Collider2D[] attackHits = Physics2D.OverlapCircleAll(transform.position, attackField, 1 << playerLayerMask);
        if (attackHits.Length > 0 && attackHits[0] != null )
        {
            Debug.Log("플레이어가 공격권에 있다!");
            anim.SetBool("isAttacking", true);
            anim.SetBool("isWalking", false);
            //playerPosition = attackHits[0].transform.position;   
        }
        else
        {
            anim.SetBool("isAttacking", false);
            anim.SetBool("isWalking", true);


        }

        Collider2D[] sightHits = Physics2D.OverlapCircleAll(transform.position, sight, 1 << playerLayerMask);

        Debug.Log(playerLayerMask.value);
        if (sightHits.Length > 0 && sightHits[0] != null)
        {
            Debug.Log("플레이어가 시야에 있다!");
            isFollowingPlayer = true;
            playerPosition = sightHits[0].transform.position;
        }
        else isFollowingPlayer = false;

    }



    // 상태 전환 함수 및 상태 클래스 짜기


}


public class MonsterState
{
    private bool _isRightMoving = true;
    private bool _isWalking = true;
    private bool _isHit = false;
    private bool _isAttacking = false;
    private bool _isDead = false;

    public bool isRightMoving
    {
        get => _isRightMoving;
        set
        {
            _isRightMoving = value;
        }
    }
    public bool isWalking
    {
        get => _isWalking;
        set
        {
            _isWalking = value;
        }
    }
    public bool isHit
    {
        get => _isHit;
        set
        {
            _isWalking = !value;
            _isHit = value;
        }
    }

    public bool isAttacking
    {
        get => _isAttacking;
        set 
        {
            _isWalking = !value;
            _isAttacking = value;
        }

    }

    public bool isDead
    {
        get => _isDead;
        set
        {
            _isDead = value;
        }
    }
}
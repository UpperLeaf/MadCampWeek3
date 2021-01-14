using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class MonsterController : MonoBehaviour
{

    [SerializeField, Tooltip("땅에서의 가속도 (왜 필요한거지?)")]
    float walkAcceleration = 50;

    private BoxCollider2D boxCollider;

    private Vector2 velocity;

    private bool grounded;

    Animator anim;

    // 이동 방향
    bool movingRight = true;

    // 몬스터의 스탯
    [SerializeField, Tooltip("최대 체력")]
    int maxHp = 100;

    [SerializeField, Tooltip("최대 공격력")]
    int maxStr = 10;

    [SerializeField, Tooltip("최대 속도")]
    float maxSpeed = 7;

    // 몬스터의 현재 스탯
    [SerializeField, Tooltip("현재 체력")]
    int hp;

    [SerializeField, Tooltip("현재 공격력")]
    int str;

    [SerializeField, Tooltip("현재 속도")]
    float speed;


    [SerializeField] Transform wallDetection;
    [SerializeField] LayerMask wallLayerMask;


    // TODO 플레이어 따라다니도록 하기
    private void Start()
    {
        Debug.Log("몬스터 등장!");
        anim = GetComponent<Animator>();
        hp = maxHp;
        str = maxStr;
        speed = maxSpeed;
    }
    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void MeetOtherMonster()
    {
        Debug.Log("안녕! 다른 몬스터야");
    }

    private void Update()
    {
        if (anim.GetBool("isRightMoving"))
        {
            Debug.Log("isRightMoving");
            velocity.x = Mathf.MoveTowards(velocity.x, speed, walkAcceleration * Time.deltaTime);
        }
        else
        {
            velocity.x = Mathf.MoveTowards(velocity.x, (-1) * speed, walkAcceleration * Time.deltaTime);
        }

        // 몬스터를 실제로 움직이는 부분
        transform.Translate(velocity * Time.deltaTime);


        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, boxCollider.size, 0);

        grounded = false;

        foreach (Collider2D hit in hits)
        {
            // 자기 스스로와의 충돌을 제외
            if (hit == boxCollider)
                continue;

            // 몬스터와의 충돌을 제외
            if (hit.gameObject.tag == "Monster")
            {
                continue;
            }

            // 플레이어와의 충돌을 제외
            if (hit.gameObject.tag == "Player")
            {
                continue;
            }

            // TODO 플레이어의 무기/스킬/마법 등과 충돌 시 데미지 입음
            if (hit.gameObject.tag == "Weapon")
            {
                // health -= hit.gameObject.str = 

                // TODO 몬스터 바운스(밀려남)/프리즈(경직) 효과

                if (hp <= 0)
                {
                    // TODO 몬스터 죽음
                }
            }

            ColliderDistance2D colliderDistance = hit.Distance(boxCollider);

            if (colliderDistance.isOverlapped)
            {
                transform.Translate(colliderDistance.pointA - colliderDistance.pointB);
            }


            //if (hit.gameObject.tag == "Wall")
            //{
            //    Debug.Log("Point A: " + colliderDistance.pointA);
            //    Debug.Log("Point B: " + colliderDistance.pointB);


            //    Vector2 vec = movingRight ? new Vector2(-0.5f, 0) : new Vector2(0.5f, 0);
            //    //Vector3 vec = movingRight ? new Vector3(-0.5f, 0, 0) : new Vector3(0.5f, 0, 0);
            //    movingRight = !movingRight;

            //    //transform.position += vec;
            //    transform.Translate(vec);
            //    continue;
            //}

            Debug.Log("충돌함: " + hit.gameObject.name);


            if (Vector2.Angle(colliderDistance.normal, Vector2.up) < 90 && velocity.y < 0)
            {
                grounded = true;
            }

            // 지형 체크 
            Vector2 position = transform.position;
            Vector2 frontVec = anim.GetBool("isRightMoving") ? position + Vector2.right : position + Vector2.left;

            if (velocity.y < 0)
            {
                Debug.DrawRay(frontVec, Vector3.down, new Color(0, 0.5f, 0));
                RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down);

                if (rayHit.collider == null)
                {
                    Debug.Log("낭떠러지");
                    movingRight = !movingRight;
                    anim.SetBool("isRightMoving", movingRight);
                }
            }

            // TODO 만약 플레이어가 공격권 안에 있다면 공격하기
            anim.SetBool("isWalking", true);

        }

        if (grounded)
        {
            velocity.y = 0;
        }

        // 땅과 닿아있지 않으면 떨어지게 하기
        velocity.y += Physics2D.gravity.y * Time.deltaTime;

    }
}

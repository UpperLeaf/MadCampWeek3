using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField, Tooltip("땅에서의 가속도 (왜 필요한거지?)")]
    float walkAcceleration = 10f;

    private BoxCollider2D groundCheckCollider;

    private Vector2 velocity;

    [SerializeField]
    private bool grounded;

    Animator anim;

    // 이동 방향
    bool movingRight;

    [SerializeField, Tooltip("최대 속도")]
    float maxSpeed = 2f;

    [SerializeField, Tooltip("현재 속도")]
    float speed;

    [SerializeField, Tooltip("시야")]
    float sight;

    [SerializeField, Tooltip("공격 범위")]
    float attackField;

    [SerializeField, Tooltip("플레이어를 따라가고 있는지 여부")]
    bool isFollowingPlayer;


    [SerializeField, Tooltip("높이 (땅 위에 있는지 체크할 때 사용)")]
    float height;


    private AbstractMonsterAttack _attackStrategy;

    [System.NonSerialized] public float dir = 1.0f;
    [System.NonSerialized] public float basScaleX = 1.0f;

    [SerializeField] LayerMask playerLayerMask;
    protected float speedVx = 0.0f;

    private LayerMask floor;

    Vector2 playerPosition;

    Transform bossSprite;
    Transform bossGroundCollider;
    Transform bossHitCollider;

    private void Start()
    {
        bossSprite = gameObject.transform.Find("BossSprite");
        bossGroundCollider = gameObject.transform.Find("BossGroundCollider");
        bossHitCollider = gameObject.transform.Find("BossHitCollider");



        floor = LayerMask.NameToLayer("Floor");
        
        anim = bossSprite.GetComponent<Animator>();
        groundCheckCollider = bossGroundCollider.GetComponent<BoxCollider2D>();



        _attackStrategy = GetComponent<DefaultMonsterAttack>();
        speed = maxSpeed;
        playerLayerMask = LayerMask.NameToLayer("Player");
        sight = 3.0f;
        attackField = 0.5f;
        isFollowingPlayer = false;
        grounded = false;
    }

    private void Awake()
    {

    }
    private void CollisionCheck()
    {
        grounded = false;
        //Collider2D[] hits = Physics2D.OverlapBoxAll(groundCheckCollider.transform.position, groundCheckCollider.size, 0);

        Collider2D[] hits = Physics2D.OverlapBoxAll(groundCheckCollider.transform.position, groundCheckCollider.size, 0, 1 << floor);
        //Debug.Log("hits length: " + hits.Length);

        //grounded = (hits.Length > 0);

        //Debug.DrawRay(groundCheckCollider.transform.position, Vector2.down * height, new Color(255, 255, 0));
        //RaycastHit2D rayHitGround = Physics2D.Raycast(groundCheckCollider.transform.position, Vector2.up);

        ////grounded = !(rayHitGround.collider == null);

        //if (rayHitGround.collider == null || rayHitGround.collider == groundCheckCollider || rayHitGround.collider.tag == "Monster")
        //{
        //    grounded = false;
        //    //dir *= (-1);
        //}
        //else
        //{
        //    Debug.Log("rayHit " + rayHitGround.collider.name);

        //    grounded = true;
        //}


        foreach (Collider2D hit in hits)
        {
            Debug.Log(hit.name);
            grounded = true;

        }

            //if (velocity.y < float.Epsilon)
            //{
            //    ColliderDistance2D colliderDistance = hit.Distance(groundCheckCollider);

            //    if (colliderDistance.isOverlapped)
            //    {
            //        transform.Translate(colliderDistance.pointA - colliderDistance.pointB);
            //        if (Vector2.Angle(colliderDistance.normal, Vector2.up) < 60 && velocity.y < 0)
            //        {
            //            grounded = true;
            //        }
            //    }
            //}



            //}
        }

    public void Gravity()
    {
        if (grounded) velocity.y = 0;
        else velocity.y += Physics2D.gravity.y * Time.deltaTime;
    }


    private void Move()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        float _speed = 6f;
        float acceleration = 75;
        float deceleration = 75;

        //if (_playerState.isDamaged || _playerState.isCast)
        //    moveInput = 0;
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);


        if (moveInput != 0 && !stateInfo.IsTag("Attack"))
        {
            transform.localScale = new Vector3(1 * moveInput, 1, 1);
            velocity.x = Mathf.MoveTowards(velocity.x, _speed * moveInput, acceleration * Time.deltaTime);
            anim.SetTrigger("Run");
        }
        else if (moveInput != 0 && (stateInfo.IsTag("Attack")))
        {
            velocity.x = Mathf.MoveTowards(velocity.x, _speed * moveInput, acceleration * Time.deltaTime);
            anim.SetTrigger("Run");
        }
        else
        {
            velocity.x = Mathf.MoveTowards(velocity.x, 0, deceleration * Time.deltaTime);
            anim.SetTrigger("Idle");
        }

        transform.Translate(velocity * Time.deltaTime);
    }


  

    private void Update()
    {

        // 애니메이션 전환 디버깅

        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetTrigger("Attack");
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            anim.SetTrigger("Hit");
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            anim.SetTrigger("Idle");
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetTrigger("Dead");
        }

        // 방향 전환
        //transform.localScale = new Vector3(basScaleX * dir, transform.localScale.y, transform.localScale.z);

        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        Vector2 position = transform.position;
        Vector2 frontVec = (dir > 0) ? Vector2.right * 3 : Vector2.left * 3;

        if (!stateInfo.IsTag("Dead"))
            Move();
        Gravity();
        CollisionCheck();


        //float height = 3.0f;

        //Debug.DrawRay(position, Vector2.down * height, new Color(255, 255, 0));
        //RaycastHit2D rayHitGround = Physics2D.Raycast(position, Vector2.down, height);

        ////grounded = !(rayHitGround.collider == null);

        //if (rayHitGround.collider == null || rayHitGround.collider == groundCheckCollider)
        //{
        //    grounded = false;
        //    //dir *= (-1);
        //}
        //else
        //{
        //    Debug.Log(rayHitGround.collider.name);

        //    grounded = true;
        //}


        //Debug.DrawRay(position + frontVec, new Vector2(0, -4), new Color(255, 255, 0));
        //RaycastHit2D rayHitGround = Physics2D.Raycast(position + frontVec, new Vector2(0, -4), 1.0f);

        //if (rayHitGround.collider == null)
        //{
        //    //dir *= (-1);
        //}
        //else Debug.Log(rayHitGround.collider.name);


        //if (!stateInfo.IsTag("Hit"))
        //{
        //    Collider2D[] attackHits = Physics2D.OverlapCircleAll(transform.position, attackField, 1 << playerLayerMask);
        //    if (attackHits.Length > 0 && attackHits[0] != null)
        //        _attackStrategy.Attack();
        //}


        //Collider2D[] sightHits = Physics2D.OverlapCircleAll(transform.position, sight, 1 << playerLayerMask);
        //if (sightHits.Length > 0 && sightHits[0] != null)
        //{
        //    isFollowingPlayer = true;s
        //    playerPosition = sightHits[0].transform.position;
        //}
        //else isFollowingPlayer = false;

    }


    public bool ActionLookup(GameObject go, float near)
    {
        if (Vector3.Distance(transform.position, go.transform.position) > near)
        {
            dir = (transform.position.x < go.transform.position.x) ? +1 : -1;
            return true;
        }
        return false;
    }

    public bool ActionMoveToNear(GameObject go, float near)
    {
        if (Vector3.Distance(transform.position, go.transform.position) > near)
        {
            ActionMove((transform.position.x < go.transform.position.x) ? +1.0f : -1.0f);
            return true;
        }
        return false;
    }

    public bool ActionMoveToFar(GameObject go, float far)
    {
        if (Vector3.Distance(transform.position, go.transform.position) < far)
        {
            ActionMove((transform.position.x > go.transform.position.x) ? +1.0f : -1.0f);
            return true;
        }
        return false;
    }

    public virtual void ActionMove(float n)
    {
        if (n != 0.0f)
        {
            dir = Mathf.Sign(n);
            speedVx = speed * n;
            anim.SetTrigger("Run");
        }
        else
        {
            speedVx = 0;
            anim.SetTrigger("Idle");
        }
    }
}

using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class MonsterController : MonoBehaviour
{
    [SerializeField]
    protected MonsterStats monsterStats;


    float walkAcceleration = 10f;

    private BoxCollider2D boxCollider;

    private Vector2 velocity;

    [SerializeField]
    private bool grounded;

    protected Animator anim;

    // 이동 방향
    bool movingRight;

    [SerializeField, Tooltip("플레이어를 따라가고 있는지 여부")]
    bool isFollowingPlayer;


    [SerializeField, Tooltip("높이 (땅 위에 있는지 체크할 때 사용)")]
    protected float height;


    protected AbstractMonsterAttack _attackStrategy;

 
    protected LayerMask playerLayerMask;


    Vector2 playerPosition;

    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        GetAttackStrategy();

        if (monsterStats == null) Debug.Log("monsterStats가 null");

        monsterStats.speed = monsterStats.maxSpeed;
        monsterStats.hp = monsterStats.maxHp;
        movingRight = anim.GetBool("isRightMoving");
        playerLayerMask = LayerMask.NameToLayer("Player");
        isFollowingPlayer = false;
        grounded = false;
        anim.SetBool("isStop", false);
    }

    protected virtual void GetAttackStrategy()
    {
        _attackStrategy = GetComponent<DefaultMonsterAttack>();

    }

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void GoRight()
    {
        velocity.x = Mathf.MoveTowards(velocity.x, monsterStats.speed, walkAcceleration * Time.deltaTime);
        anim.SetBool("isRightMoving", true);
        transform.Translate(velocity * Time.deltaTime);
    }

    private void GoLeft()
    {
        velocity.x = Mathf.MoveTowards(velocity.x, (-1) * monsterStats.speed, walkAcceleration * Time.deltaTime);
        anim.SetBool("isRightMoving", false);
        transform.Translate(velocity * Time.deltaTime);
    }

    private void Stop()
    {
        velocity.x = 0;
        transform.Translate(velocity * Time.deltaTime);
    }

    virtual public void DeathEvent()
    {
        Debug.Log("Death!");
        Destroy(gameObject);
    }

    virtual protected void Update()
    {
        Vector2 position = transform.position;
        Vector2 frontVec = anim.GetBool("isRightMoving") ? Vector2.right * 3 : Vector2.left* 3;
        bool isStop = anim.GetBool("isStop");
        bool isDied = anim.GetBool("isDied");
        bool isHit = anim.GetBool("isHit");
       
        if (isStop || isDied)
        {
            Stop();
        }
        else if (isFollowingPlayer)
        {
            if (playerPosition.x < position.x)
                GoLeft();
            else GoRight();

        }
        else if (anim.GetBool("isRightMoving"))
        {
            GoRight();       
        }
        else if (!anim.GetBool("isRightMoving"))
        {
            GoLeft();
        }

        Debug.DrawRay(position + frontVec, new Vector2(0, -4), new Color(255, 255, 0));
        RaycastHit2D rayHitGround = Physics2D.Raycast(position + frontVec, Vector2.down);
        RaycastHit2D rayHitGroundFront = Physics2D.Raycast(position + frontVec, Vector2.right);
        
        if (rayHitGround.collider == null)
        {
            movingRight = !movingRight;
            anim.SetBool("isRightMoving", movingRight);
        }

        SeekAndAttack(isHit);
        SeekPlayer();
    }

    protected void SeekPlayer()
    {
        Collider2D[] sightHits = Physics2D.OverlapCircleAll(transform.position, monsterStats.sight, 1 << playerLayerMask);
        if (sightHits.Length > 0 && sightHits[0] != null)
        {
            isFollowingPlayer = true;
            playerPosition = sightHits[0].transform.position;
        }
        else isFollowingPlayer = false;
    }

    virtual protected void SeekAndAttack(bool isHit)
    {
        if (!isHit)
        {
            Collider2D[] attackHits = Physics2D.OverlapCircleAll(transform.position, monsterStats.attackField, 1 << playerLayerMask);
            if (attackHits.Length > 0 && attackHits[0] != null)
                _attackStrategy.Attack();
        }

    }

}
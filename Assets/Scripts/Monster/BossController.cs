using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class BossController : MonsterController
{
    [SerializeField]
    BossStats bossStats;

    BossMonsterAttack _bossAttackStrategy;

    protected override void Start()
    {
        anim = GetComponent<Animator>();
        _bossAttackStrategy = GetComponent<BossMonsterAttack>();
        playerLayerMask = LayerMask.NameToLayer("Player");
        maxSpeed = bossStats.maxSpeed;
        speed = bossStats.speed;
        sight = bossStats.sight;
        attackField = bossStats.attackField;
        anim.SetBool("isStop", false);

    }

    protected override void SeekAndAttack(bool isHit)
    {
        if (!isHit)
        {
            Collider2D[] attackHits = Physics2D.OverlapCircleAll(transform.position, attackField, 1 << playerLayerMask);
            if (attackHits.Length > 0 && attackHits[0] != null)
                _bossAttackStrategy.Attack();
        }

    }
    public override void DeathEvent()
    {
        Debug.Log("Death!");
        Destroy(gameObject);
    }



}
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
public class BossController : MonsterController
{
    [SerializeField]
    MonsterStats bossStats;

    BossMonsterAttack _bossAttackStrategy;
    public Slider healthBar;

    protected override void GetAttackStrategy()
    {
        _bossAttackStrategy = GetComponent<BossMonsterAttack>();
    }

    protected override void Update()
    {
        base.Update();
        healthBar.value = bossStats.hp;
    }

    protected override void SeekAndAttack(bool isHit)
    {
        if (!isHit)
        {
            Collider2D[] attackHits = Physics2D.OverlapCircleAll(transform.position, monsterStats.attackField, 1 << playerLayerMask);
            if (attackHits.Length > 0 && attackHits[0] != null)
                _bossAttackStrategy.Attack();
        }

    }
    public override void DeathEvent()
    {
        Debug.Log("게임 클리어!");
        Destroy(gameObject);
    }


}
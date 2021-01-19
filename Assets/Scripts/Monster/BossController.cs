using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
public class BossController : MonsterController
{

    BossMonsterAttack _bossAttackStrategy;

    [SerializeField]
    private Slider healthBar;

    protected override void GetAttackStrategy()
    {
        _bossAttackStrategy = GetComponent<BossMonsterAttack>();
    }

    protected override void Update()
    {
        base.Update();
        Debug.Log("healthBar: " + healthBar.value);
        healthBar.value = monsterStats.hp / monsterStats.maxHp;
    }

    protected override bool SeekAndAttack(bool isHit)
    {
        if (!isHit)
        {
            Collider2D[] attackHits = Physics2D.OverlapCircleAll(transform.position, monsterStats.attackField, 1 << playerLayerMask);
            if (attackHits.Length > 0 && attackHits[0] != null)
            {
                _bossAttackStrategy.Attack();
                return true;
            }
        }
        return false;
    }
    public override void DeathEvent()
    {
        Debug.Log("게임 클리어!");
        Destroy(gameObject);
    }


}
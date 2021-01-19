using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(BoxCollider2D))]
public class BossController : MonsterController
{

    BossMonsterAttack _bossAttackStrategy;

    [SerializeField]
    private Slider healthBar;

    public GameObject spike;
    public float spikeThrowInterval = 30.0f;


    IEnumerator ThrowSpike()
    {
        while (true)
        {
            Vector3 frontVec =  (anim.GetBool("isRightMoving") ? Vector2.right : Vector2.left);

            GameObject obj = Instantiate(spike, transform.position + frontVec, transform.rotation);
            //if (obj.activeInHierarchy)
            //{
                SpikeController spikeController = obj.GetComponent<SpikeController>();
                spikeController.SetVelocity(frontVec);
            //}
            yield return new WaitForSeconds(spikeThrowInterval);

        }
    }


    protected override void GetAttackStrategy()
    {
        _bossAttackStrategy = GetComponent<BossMonsterAttack>();
        StartCoroutine("ThrowSpike");
    }

    protected override void Update()
    {

        base.Update();
        Debug.Log("healthBar: " + healthBar.value);
        healthBar.value = monsterStats.hp / monsterStats.maxHp;
        if (healthBar.value < 0.3) spikeThrowInterval = 20.0f;
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
        SceneManager.LoadScene("InfoScene");
    }


}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : AbstractDamagable
{
    private PlayerState playerState;

    //[SerializeField]
    //private int _hp;

    // 체력바
    public HitPoints hitPoints;
    public HealthBar healthBarPrefab;
    public float maxHitPoints;
    
    [SerializeField]
    private float startingHitPoints;
    
    HealthBar healthBar;


    private void Awake()
    {
        playerState = GetComponent<PlayerState>();
    }

    private void Start()
    {
        hitPoints.HP = startingHitPoints;
        healthBar = Instantiate(healthBarPrefab);
        healthBar.player = this;
    }

    private void setHp(int damaged)
    {
        Debug.Log(hitPoints.HP);
        if (hitPoints.HP - damaged >  0)
        {
            hitPoints.HP -= damaged;
            playerState.isDamaged = true;
        }
        else
        {
            hitPoints.HP = 0f;
            playerState.isDied = true;
        }
    }

    public override void TakeDamage(int damage, GameObject attacker)
    {
        //피격중인 상태가 아닐때만 데미지를 받을 수 있다.
        if (!playerState.isDamaged)
        {
            setHp(damage);
            StartCoroutine("HitCoroutine", attacker);
        }
    }

    IEnumerator HitCoroutine(GameObject attacker)
    {
        float direction = transform.position.x - attacker.transform.position.x > 0 ? 1 : -1;
        Vector2 dir = new Vector2(direction / 15, 0);
        for (int i = 0; i < 20; i++)
        {
            transform.Translate(dir);
            yield return null;
        }
    }
}

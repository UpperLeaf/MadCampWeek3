using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : AbstractDamagable
{
    private PlayerState playerState;
    
    public float startingHitPoints;

    public int attackDamage;

    public int magicDamage;

    public bool isDashNoneDamaged;

    public int isDashNoneDamagedTime;

    public int speed;

    public int dashSpeed;

    public int jumpHeight;

    [SerializeField]
    public HealthBar healthBarPrefab;

    private HealthBar healthBar;

    public HitPoints hitPoints;
    
    public float maxHitPoints;
    


    private void Awake()
    {
        playerState = GetComponent<PlayerState>();
    }

    private void Start()
    {
        healthBar = Instantiate(healthBarPrefab);
        hitPoints.HP = startingHitPoints;
        _hp = startingHitPoints;
        healthBar.player = this;
    }

    private void setHp(int damaged)
    {
        if (_hp - damaged >  0)
        {
            _hp -= damaged;
            playerState.isDamaged = true;
        }
        else
        {
            _hp = 0;
            playerState.isDied = true;
        }
        hitPoints.HP = _hp;
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

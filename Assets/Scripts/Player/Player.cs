using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : AbstractDamagable
{
    private PlayerState playerState;
    
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
        healthBar = Instantiate(healthBarPrefab, gameObject.transform);
        hitPoints.HP = maxHitPoints;
        _hp = maxHitPoints;
        healthBar.player = this;
    }

    public void plusAttackDamage()
    {
        Debug.Log("plusAttackDamage Skill을 샀다!");
        attackDamage += 5;
    }

    public void plusMagicDamage()
    {
        magicDamage += 5;
    }

    public void plusMaxHp()
    {
        maxHitPoints += 10;
        healthBar.maxHitPoints = maxHitPoints;
    }

    public void plusMoveSpeed()
    {
        speed += 1;
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
        if (!playerState.isDamaged && gameObject.tag == "Player")
        {
            setHp(damage);
            StartCoroutine("HitCoroutine", attacker);
        }
    }

    IEnumerator HitCoroutine(GameObject attacker)
    {
        float direction = transform.position.x - attacker.transform.position.x > 0 ? 1 : -1;
        Vector2 dir = new Vector2(direction / 10, 0);
        for (int i = 0; i < 10; i++)
        {
            transform.Translate(dir);
            yield return null;
        }
    }
}

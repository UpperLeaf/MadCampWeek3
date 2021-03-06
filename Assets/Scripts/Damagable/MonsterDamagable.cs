﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDamagable : AbstractDamagable
{
    [SerializeField]
    MonsterStats monsterStats;

    private Transform _player;
    
    private void Start()
    {
        _isDied = false;
        _anim = GetComponent<Animator>();
        _player = PlayerManager.Instance.getPlayer().transform;
    }

    public override void TakeDamage(int damage, GameObject attacker)
    {
        if (!_isDied)
        {
            SetHp(damage);
            HitStart(attacker);
            bool isRight = _anim.GetBool("isRightMoving");

            if (isRight)
                _anim.SetTrigger("RightHit");
            else
                _anim.SetTrigger("LeftHit");

            _anim.SetBool("isStop", true);

            StartCoroutine("StartHitTime");
        }
    }



    public void HitStart(GameObject attacker)
    {
        StartCoroutine("HitCoroutine");
    }

    IEnumerator HitCoroutine()
    {
        float direction = transform.position.x - _player.position.x > 0 ? 1 : -1;
        
        Vector2 dir = new Vector2(direction / 30, 0);
        for (int i = 0; i < 30; i++)
        {
            transform.Translate(dir);
            yield return null;
        }
    }

    public virtual void SetHp(int damage)
    {
        if (monsterStats.hp - damage > 0)
            monsterStats.hp -= damage;

        else
        {
            monsterStats.hp = 0;
            _isDied = true;
            _anim.SetBool("isDied", _isDied);
        }

        Debug.Log(monsterStats.hp);

    }

    IEnumerator StartHitTime()
    {
        yield return new WaitForSeconds(monsterStats.hitTime);
        _anim.SetBool("isStop", false);
    }
}

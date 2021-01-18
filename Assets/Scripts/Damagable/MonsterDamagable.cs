using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDamagable : AbstractDamagable
{
    [SerializeField]
    BossStats bossStats;

    private Transform _player;
    private void Start()
    {
        _isDied = false;
        _anim = GetComponent<Animator>();
        _hitTime = bossStats.hitTime;
        _hp = bossStats.hp;
        _maxHp = bossStats.maxHp;
        _player = PlayerManager.Instance.getPlayer().transform;
    }

    public override void TakeDamage(int damage, GameObject attacker)
    {
        if (!_isDied)
        {
            SetHp(damage);
            HitStart(attacker);
            Debug.Log(damage);
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
        if (_hp - damage > 0)
            _hp -= damage;
        else
        {
            _hp = 0;
            _isDied = true;
            _anim.SetBool("isDied", _isDied);
        }
    }

    IEnumerator StartHitTime()
    {
        yield return new WaitForSeconds(_hitTime);
        _anim.SetBool("isStop", false);
    }
}

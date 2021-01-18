using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamagable : AbstractDamagable
{
    private Transform _player;
    private void Start()
    {
        _isDied = false;
        _anim = GetComponent<Animator>();
        _hitTime = 1;
        _hp = 100f;
        _player = PlayerManager.Instance.getPlayer().transform;
    }

    public override void TakeDamage(int damage, GameObject attacker)
    {
        if (!_isDied)
        {
            SetHp(damage);
            HitStart(attacker);
            Debug.Log(damage);
            _anim.SetTrigger("Hit");

            //_anim.SetTrigger("Idle");
            //StartCoroutine("StartHitTime");
        }
    }

    public void DeathEvent()
    {
        Destroy(gameObject);
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
            _anim.SetTrigger("Dead");
        }
    }

    IEnumerator StartHitTime()
    {
        yield return new WaitForSeconds(_hitTime);
        _anim.SetTrigger("Run");
    }
}

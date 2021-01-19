using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class Fireball : MonoBehaviour
{

    [SerializeField]
    private GameObject hitEffect;

    private CapsuleCollider2D _collider2D;

    private float _speed;
    private float _moveCheckTime;
    private float _checkTime;
    private float _destoryTime;
    private int _dirc;
    private int _damage;

    private LayerMask _enemies;
    private Vector2 _moveVector;

    void Start()
    {
        _checkTime = 0.1f;
        _moveCheckTime = 0.02f;
        _collider2D = GetComponent<CapsuleCollider2D>();
        _enemies = LayerMask.NameToLayer("Enemy");
        _dirc = (int)transform.localScale.x;
        _speed = 30f;
        _destoryTime = 2f;
        _moveVector = new Vector2(_dirc * _speed , 0);
        _damage = 30;
        
        StartCoroutine("CollisionCheck");
        StartCoroutine("Move");
        Invoke("TimeDestory", _destoryTime);
    }

    public void TimeDestory()
    {
        Destroy(gameObject);
    }

    IEnumerator Move()
    {
        while (true)
        {
            transform.Translate(_moveVector * Time.deltaTime);
            yield return new WaitForSeconds(_moveCheckTime);
        }
    }

    IEnumerator CollisionCheck()
    {
        while (true)
        {
            Collider2D[] enemiesToDamage = Physics2D.OverlapCapsuleAll(_collider2D.transform.position, _collider2D.size, _collider2D.direction, 0, 1 << _enemies); 
            foreach(Collider2D hit in enemiesToDamage)
            {
                Debug.Log(hit.name);
                hit.GetComponent<AbstractDamagable>()?.TakeDamage(_damage, gameObject);
                Transform enemy = hit.GetComponent<Transform>();

                hitEffect.transform.position = enemy.position;
                GameObject effect = Instantiate(hitEffect);
                Destroy(effect, effect.GetComponent<ParticleSystem>().main.duration + 1f);
                Destroy(gameObject);
            }
            yield return new WaitForSeconds(_checkTime);
        }
    }
}

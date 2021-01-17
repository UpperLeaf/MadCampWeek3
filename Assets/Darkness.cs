using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Darkness : MonoBehaviour
{

    private CircleCollider2D _collider2D;
    private LayerMask enemies;
    private float  _destroyTime;
    private int _damage;
    private float _checkTime;
    
    void Start()
    {
        _destroyTime = 2.5f;
        _checkTime = 0.1f;
        _collider2D = GetComponent<CircleCollider2D>();
        enemies = LayerMask.NameToLayer("Enemy");
        StartCoroutine("CollisionCheck");
        Invoke("DestroyThis", _destroyTime);
    }

    public void SetDamage(int damage)
    {
        _damage = damage;
    }

    public void DestroyThis()
    {
        Destroy(gameObject);
    }

    IEnumerator CollisionCheck()
    {
        while (true)
        {
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(_collider2D.transform.position, _collider2D.radius, 1 << enemies);
            foreach (Collider2D hit in enemiesToDamage)
                hit.GetComponent<AbstractDamagable>().TakeDamage(_damage, gameObject);
            yield return new WaitForSeconds(_checkTime);
        }
    }
}

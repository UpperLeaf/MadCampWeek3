using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeController : MonoBehaviour
{
    protected AbstractMonsterAttack _attackStrategy;
    Vector2 velocity;

    [SerializeField]
    public float speed;

    BoxCollider2D boxCollider;

    public int direction; // 1: right, -1: left

    void Start()
    {
        boxCollider = transform.GetComponent<BoxCollider2D>();
        _attackStrategy = GetComponent<SpikeAttack>();

    }
    public void SetVelocity(Vector2 frontVector)
    {
        velocity = frontVector * speed;

        direction = (frontVector.x > 0) ? -1 : 1;

        Debug.Log("SetVelocity: " + frontVector);
    }


    private void Update()
    {

        if (DetectWall()) Destroy(gameObject);
        transform.Translate(velocity * Time.deltaTime);
        _attackStrategy.Attack();
       

    }

    bool DetectWall()
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, boxCollider.size, 0);
        foreach (Collider2D hit in hits)
        {

            if (hit.gameObject.tag == "PopWall")
            {
                return true;
            }
        }
        return false;

    }

}

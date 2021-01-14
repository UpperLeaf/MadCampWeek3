using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnim : MonoBehaviour
{
    Rigidbody2D rigid;

    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigid.velocity = new Vector2(1, rigid.velocity.y); // y 값으로 0을 넣으면 안됨!

    }
}

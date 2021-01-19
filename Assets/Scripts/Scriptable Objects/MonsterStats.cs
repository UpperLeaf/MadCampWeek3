using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "MonsterStats")]
public class MonsterStats : ScriptableObject
{
    public float maxHp;
    public float hp;
    public int hitTime;

    public float maxSpeed;
    public float speed;

    public float coolTime;
    public float idleTime;
    public int damage;
    public float attackField;
    public float sight;

}

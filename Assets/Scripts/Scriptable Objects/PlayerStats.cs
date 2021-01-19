using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "PlayerStats")]
public class PlayerStats : ScriptableObject
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


    public int skillPoints;

    public bool canUseDash;
    public bool canUseFireball;
    public bool canUseDarkness;

    public int attackDamage;
    public int magicDamage;




}

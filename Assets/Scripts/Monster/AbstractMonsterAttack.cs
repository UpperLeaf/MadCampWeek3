using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractMonsterAttack : MonoBehaviour
{
    protected float cooltime;

    protected int damage;

    protected LayerMask enemies;
}

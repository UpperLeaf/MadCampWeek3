using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractDamagable : MonoBehaviour, IDamagable
{
    public abstract void TakeDamage(int damage);
}

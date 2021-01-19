using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "HitPoints")]
public class HitPoints : ScriptableObject
{
    private float hp;

    public float HP
    {
        get => hp;
        set
        {
            hp = value;
        }
    }
}

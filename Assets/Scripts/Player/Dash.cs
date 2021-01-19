using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{

    [SerializeField]
    private float coolTime;
    
    [SerializeField]
    private bool isDashable;
    
    [SerializeField]
    private bool isNoneDamaged;

    private void Start()
    {
        coolTime = 1f;
        isDashable = true;
    }

    public bool IsNoneDamaged()
    {
        return isNoneDamaged;
    }
    public bool IsDashAble()
    {
        return isDashable;
    }

    public void ExecuteDash()
    {
        isDashable = false; 
        StartCoroutine("CoolTime");
    }


    IEnumerator CoolTime()
    {
        yield return new WaitForSeconds(coolTime);
        isDashable = true;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;


public class ShockwaveUnityEvent : MonoBehaviour
{
    public UnityEvent shock;
    void Start()
    {
        InvokeRepeating("ShockwaveEvent", 3f, 4f);
    }

    private void ShockwaveEvent()
    {
        shock.Invoke();
    }


}

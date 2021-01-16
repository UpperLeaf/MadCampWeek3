using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class ShockwaveListenerExample : MonoBehaviour
{
    private CinemachineImpulseSource source;

    private void Awake()
    {
        source = GetComponent<CinemachineImpulseSource>();
    }

    void Start()
    {
        //InvokeRepeating("Shake", 3f, 4f);
    }

    private void Update()
    {
        // 원하는 조건으로 변경
        if (Input.GetKeyDown(KeyCode.K))
        {
            // impulse 만드는 함수
            Invoke("Shake", 2f); // 두번째 인자는 이벤트 발생 지연 시간
        }
    }

    public void Shake()
    {
        source.GenerateImpulse();
    }
}

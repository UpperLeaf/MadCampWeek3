using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 스크립트에서 UI element 사용
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    // PlayerController 내부의 hitPoints 주소를 저장하게 되는 변수
    // Player와 데이터를 공유하기 위해 만듦
    [SerializeField]
    public HitPoints hitPoints;

    // Player의 maxHitPoints 값을 얻기 위해 현재 Player 오브젝트 필요
    // 현재 Player 오브젝트의 주소를 저장하는 변수
    [SerializeField]
    public Player player; // 

    public Image meterImage;

    [SerializeField]
    float maxHitPoints;

    // Start is called before the first frame update
    void Start()
    {
        maxHitPoints = player.maxHitPoints;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(hitPoints.HP);
        StartCoroutine("HpBarUpdate");
    }


    IEnumerator HpBarUpdate()
    {
        while (true)
        {
            if (player != null)
            {
                meterImage.fillAmount = (float)hitPoints.HP / maxHitPoints;
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}

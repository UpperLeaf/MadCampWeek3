using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BackToHomeButton : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("로깅");

    }
    public void OnClickHomeButton()
    {
        SceneManager.LoadScene("StartScene");
        Debug.Log("홈으로 돌아가기");
    }
}

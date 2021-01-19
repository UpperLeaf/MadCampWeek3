using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BackToMapEvent : MonoBehaviour
{
    public void OnClickBackButton()
    {
        SceneManager.LoadScene("NodeScene");
        MapManager.Instance.mapGenerator.gameObject.SetActive(true);
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine("MoveNextStage");
    }


    IEnumerator MoveNextStage()
    {
        while (true)
        {
            Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, GetComponent<BoxCollider2D>().size, 0);

            foreach(Collider2D hit in hits)
            {
                if(hit.tag == "Player")
                {
                    if (Input.GetKeyDown(KeyCode.UpArrow) && hits.Length > 0 && GameManager.Instance.CheckNoneMonster())
                    {
                        PlayerManager.Instance.getPlayer().SetActive(false);
                        MapManager.Instance.mapGenerator.gameObject.SetActive(true);
                        SceneManager.LoadScene("NodeScene");
                    }
                }
            }
            yield return new WaitForSeconds(0.01f);
        }
    }
}
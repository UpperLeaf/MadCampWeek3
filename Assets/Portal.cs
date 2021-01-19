using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine("MoveNextStage");
    }

    bool IsStageCleared()
    {
        foreach(Transform child in GameManager.Instance.map.transform)
        {
            if (child.name.Equals("Monster"))
            {
                if (child.transform.childCount == 0)
                    return true;
            }

        }
        // 맵에 몬스터 태그를 가진 물체가 하나도 없으면 return true
        return false;
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
                    if (Input.GetKeyDown(KeyCode.UpArrow) && hits.Length > 0 && IsStageCleared())
                    {
                        switch (MapManager.Instance.now.GetNodeType())
                        {
                            case NodeSpace.Node.NodeType.ELIETE:
                                PlayerManager.Instance.getPlayer().GetComponent<PlayerSkillManager>().skillPoints += 3;
                                break;

                            case NodeSpace.Node.NodeType.NORAML:
                                PlayerManager.Instance.getPlayer().GetComponent<PlayerSkillManager>().skillPoints += 1;
                                break;
                        }

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private static GameManager instance;
    public NodeSpace.Node.NodeType nodeType;
    
    private GameObject map;

    [SerializeField]
    private GameObject[] mapsObject;

    [SerializeField]
    private GameObject[] elieteMap;


    public void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
    }
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }
    public void Start()
    {
        nodeType = MapManager.Instance.now.GetNodeType();

        if (nodeType.Equals(NodeSpace.Node.NodeType.NORAML))
        {
            int randomMap = Random.Range(0, mapsObject.Length);
            map = Instantiate(mapsObject[randomMap]);
        }
        else
        {
            int randomMap = Random.Range(0, elieteMap.Length);
            map = Instantiate(elieteMap[randomMap]);
        }

        GameObject _player = PlayerManager.Instance.getPlayer();
        _player.SetActive(true);
        _player.transform.position = map.GetComponent<MapController>().startPosition;
    }


    public bool CheckNoneMonster()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.tag == "Monster")
                return false;
        }
        return true;
    }
}

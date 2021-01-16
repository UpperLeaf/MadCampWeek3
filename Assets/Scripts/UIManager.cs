using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

    [SerializeField]
    private GameObject _mapPanel;
    private Transform[] _mapNodes;

    private void Awake()
    {
        if (null == _instance)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static UIManager Instance
    {
        get
        {
            return _instance;
        }
    }
        

    private void Start()
    {
        _mapNodes = GetComponentsInChildren<Transform>();
        Node[][] maps = MakeDAG();


        for(int i = 0; i < maps.Length; i++)
        {
            for (int j = 0; j < maps.Length; j++)
            {
                if (maps[i][j] != null)
                    _mapNodes[19 * j + i].ToString();
            }
        }
        
    }

    public Node[][] MakeDAG()
    {
        Node[][] map = new Node[9][];

        for (int i = 0; i < 9; i++)
            map[i] = new Node[17];

        map[4][0] = new Node(Node.NodeType.START, 4, 0);



        for (int i = 1; i < 8; i++)
        {
            for (int j = 0; j < 17; j++)
            {
                int isCreateNode = Random.Range(0, 3);

                if (isCreateNode == 0 || isCreateNode == 1)
                    map[i][j] = null;
                else
                {
                    int randomNodeType = Random.Range(0, 10);

                    if (randomNodeType <= 5)
                    {
                        map[i][j] = new Node(Node.NodeType.NORAML, i, j);
                    }
                    else if (randomNodeType <= 8)
                    {
                        map[i][j] = new Node(Node.NodeType.ELIETE, i, j);
                    }
                    else if (randomNodeType <= 9)
                    {
                        map[i][j] = new Node(Node.NodeType.STORE, i, j);
                    }
                }
            }
        }

        map[8][4] = new Node(Node.NodeType.BOSS, 8, 4);
        return map;
    }


    public void DebugMap(Node[][] map)
    {

        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 17; j++)
            {
                if (map[i][j] != null)
                    Debug.Log(map[i][j]);
            }
        }
    }
}

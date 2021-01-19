using NodeSpace;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{

    [SerializeField]
    private Sprite start;

    [SerializeField]
    private Sprite normalMonster;

    [SerializeField]
    private Sprite eleliteMonster;

    [SerializeField]
    private Sprite boss;

    [SerializeField]
    private Sprite store;

    [SerializeField]
    private GameObject lines;

    [SerializeField]
    private GameObject line;

    private Node[][] maps;


    [SerializeField]
    private GameObject nodePrefab;
    
    private GameObject[][] mapNode;

    private static int WIDTH = 8;
    private static int HEIGHT= 4;

    public Node getNodeByVector2(Vector2 pos)
    {
        int x = (int)Mathf.Ceil(pos.x) + WIDTH;
        int y = (int)Mathf.Ceil(pos.y) + HEIGHT;
        return maps[y][x];
    }

    public Node getStartNode()
    {
        return maps[HEIGHT][0];
    }

    public Node[][] createMap()
    {
        mapNode = new GameObject[HEIGHT * 2 + 1][];
        for (int i = 0; i < HEIGHT * 2 + 1; i++)
            mapNode[i] = new GameObject[WIDTH * 2 + 1];
        maps = MakeDAG();

        for (int i = 0; i < HEIGHT * 2 + 1; i++)
        {
            for (int j = 0; j < WIDTH * 2 + 1; j++)
            {
                Node.NodeType nodeType = maps[i][j].GetNodeType();
                if (maps[i][j].IsVisited() && !nodeType.Equals(Node.NodeType.NONE))
                {
                    Vector3 pos = new Vector3(j - WIDTH, i - HEIGHT, -1);
                    nodePrefab.transform.position = pos;
                    mapNode[i][j] = Instantiate(nodePrefab, gameObject.transform, true);
                    mapNode[i][j].GetComponent<NodeInfo>().nodeType = nodeType;
                    mapNode[i][j].GetComponent<NodeInfo>().isVisited = maps[i][j].IsVisited();
                    switch (nodeType)
                    {
                        case Node.NodeType.NORAML:
                            mapNode[i][j].GetComponent<SpriteRenderer>().sprite = normalMonster;
                            break;
                        case Node.NodeType.ELIETE:
                            mapNode[i][j].GetComponent<SpriteRenderer>().sprite = eleliteMonster;
                            break;
                        case Node.NodeType.BOSS:
                            mapNode[i][j].GetComponent<SpriteRenderer>().sprite = boss;
                            break;
                        case Node.NodeType.START:
                            mapNode[i][j].GetComponent<SpriteRenderer>().sprite = start;
                            break;
                        case Node.NodeType.STORE:
                            mapNode[i][j].GetComponent<SpriteRenderer>().sprite = store;
                            break;
                    }
                }
            }
        }

        Node startNode = maps[HEIGHT][0];
        Queue<Node> nodes = new Queue<Node>();
        nodes.Enqueue(startNode);
        while (nodes.Count > 0)
        {
            Node node = nodes.Dequeue();
            if (node.isLineDrawed)
                continue;
            node.isLineDrawed = true;
            List<Node> nextNodes = node.GetNextNodes();
            foreach (Node nextNode in nextNodes)
            {
                GameObject lineInstance = Instantiate(line, lines.transform, true);
                LineRenderer lineRenderer = lineInstance.GetComponent<LineRenderer>();

                Vector3 startVec = new Vector3(node._position_x, node._position_y, -1);
                Vector3 endVec = new Vector3(nextNode._position_x, nextNode._position_y, -1);

                lineRenderer.SetPositions(new Vector3[] { startVec, endVec });

                nodes.Enqueue(nextNode);
            }
        }
        return maps;
    }


    private Node[][] MakeDAG()
    {
        Node[][] map = new Node[HEIGHT * 2 + 1][];

        for (int i = 0; i < HEIGHT * 2 + 1; i++)
            map[i] = new Node[WIDTH * 2 + 1];

        for(int i = 0; i < HEIGHT * 2 + 1; i++)
        {
            for(int j = 0; j < WIDTH * 2 + 1; j++)
            {
                map[i][j] = new Node(Node.NodeType.NONE, i, j);
                map[i][j]._position_x = j - WIDTH;
                map[i][j]._position_y = i - HEIGHT;
            }
        }

        map[HEIGHT][0].SetNodeType(Node.NodeType.START);
                    
        for (int i = 0; i < map.Length; i++)
        {
            for (int j = 1; j < map[i].Length - 1; j++)
            {
                int isCreateNode = Random.Range(0, 30);
                if (isCreateNode == 2)
                {
                    int randomNodeType = Random.Range(0, 12);
                    if (randomNodeType <= 6)
                    {
                        map[i][j].SetNodeType(Node.NodeType.NORAML);
                    }
                    else if (randomNodeType <= 8)
                    {
                        map[i][j].SetNodeType(Node.NodeType.ELIETE);
                    }
                    else if (randomNodeType <= 11)
                    {
                        map[i][j].SetNodeType(Node.NodeType.STORE);
                    }
                }
            }
        }
        map[HEIGHT][WIDTH * 2].SetNodeType(Node.NodeType.BOSS);
        map[HEIGHT][WIDTH * 2].SetVisited(true);        
        MakeGraphEdge(map);
        return map;
    }

    private void MakeGraphEdge(Node[][] maps)
    {
        Queue<Node> nodes = new Queue<Node>();
        Node startNode = maps[HEIGHT][0];
        startNode.SetVisited(true);
        nodes.Enqueue(startNode);

        while(nodes.Count > 0)
        {
            Node node = nodes.Dequeue();
            bool isSearched = false;

            for (int i = node._x + 1; i < WIDTH * 2 - 1; i++)
            {
                if (i == WIDTH * 2 - 2)
                {
                    Node nextNode = maps[HEIGHT][WIDTH * 2];
                    nextNode.SetVisited(true);
                    node.addNextNode(nextNode);
                    break;
                }

                if (node._y > 0 && !maps[node._y - 1][i].GetNodeType().Equals(Node.NodeType.NONE))
                {
                    Node addNode = maps[node._y - 1][i];
                    addNode.SetVisited(true);
                    if (!node.GetNextNodes().Contains(addNode))
                    {
                        nodes.Enqueue(addNode);
                        node.addNextNode(addNode);
                    }
                    isSearched = true;
                }

                if (!maps[node._y][i].GetNodeType().Equals(Node.NodeType.NONE))
                {
                    Node addNode = maps[node._y][i];
                    addNode.SetVisited(true);
                    if (!node.GetNextNodes().Contains(addNode))
                    {
                        nodes.Enqueue(addNode);
                        node.addNextNode(addNode);
                    }
                    isSearched = true;
                }

                if (node._y < HEIGHT * 2 && !maps[node._y + 1][i].GetNodeType().Equals(Node.NodeType.NONE))
                {
                    Node addNode = maps[node._y + 1][i];
                    addNode.SetVisited(true);
                    if (!node.GetNextNodes().Contains(addNode))
                    {
                        nodes.Enqueue(addNode);
                        node.addNextNode(addNode);
                    }
                    isSearched = true;
                }
                if (isSearched)
                    break;
            }
        }
    }
}

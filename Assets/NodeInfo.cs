using NodeSpace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static NodeSpace.Node;

public class NodeInfo : MonoBehaviour
{
    [SerializeField]
    private NodeType _nodeType;

    [SerializeField]
    private List<Node> nextNodeInfo;

    public bool isVisited;

    private int _x;
    private int _y;

    public NodeType nodeType
    {
        get => _nodeType;
        set
        {
            _nodeType = value;
        }
    }

}

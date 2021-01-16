using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    private NodeType _nodeType;

    private List<Node> node;
    public int _x;
    public int _y;

    public Node(NodeType nodeType, int x, int y)
    {
        node = new List<Node>();
        _nodeType = nodeType;
        _x = x;
        _y = y;
    }

    public void addNextNode(Node node)
    {
        this.node.Add(node);
    }

    public enum NodeType{
        NONE,
        START,
        NORAML,
        ELIETE,
        STORE,
        BOSS
    }
}

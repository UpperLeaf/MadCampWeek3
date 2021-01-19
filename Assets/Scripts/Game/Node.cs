using System.Collections.Generic;

namespace NodeSpace{
    public class Node
    {
        private NodeType _nodeType;

        private List<Node> node;
        private bool isVisited;

        public int _x;
        public int _y;

        public int _position_x;
        public int _position_y;

        public bool isLineDrawed;

        public Node(NodeType nodeType, int y, int x)
        {
            node = new List<Node>();
            _nodeType = nodeType;
            _x = x;
            _y = y;
            isLineDrawed = false;
        }

        public List<Node> GetNextNodes()
        {
            return node;
        }
        public NodeType GetNodeType()
        {
            return _nodeType;
        }

        public void SetNodeType(NodeType nodeType)
        {
            _nodeType = nodeType;
        }

        public void addNextNode(Node node)
        {
            this.node.Add(node);
        }

        public bool IsVisited()
        {
            return isVisited;
        }
        public void SetVisited(bool value)
        {
            isVisited = value;
        }

        public enum NodeType {
            NONE,
            START,
            NORAML,
            ELIETE,
            STORE,
            BOSS
        }
    }


}
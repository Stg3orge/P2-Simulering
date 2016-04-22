using System;
using System.Collections.Generic;
using System.Drawing;

namespace A319TS
{
    [Serializable]
    public class Node
    {
        public enum NodeType { Yield, Home, Parking, Light, None }
        public NodeType Type;
        public List<Road> Roads = new List<Road>();
        public Point Position;
        public bool Green;
        public int ID { get { return Position.GetHashCode(); } }

        protected Node() { } // Serialize
        public Node(Point position, NodeType type) : this(position)
        {
            Type = type;
        }
        public Node(Point position)
        {
            Position = position;
            Type = NodeType.None;
            Green = true;
        }
        
        public override string ToString()
        {
            return "(" + Position.X + "," + Position.Y + ") " + Type;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;

namespace A319TS
{
    [Serializable]
    public class Node
    {
        enum NodeType { Yield, Home, Parking, Light, None }


        public List<Road> Roads;
        public Point Position;
        private Node() { } // Serialize


        public Node(Point position)
        {
            Position = position;
        }


        public Node(Point position, NodeType nodeType)
        {
            Position = position;



        }


        public override string ToString()
        {
            return "(" + Position.X + "," + Position.Y + ")";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;

namespace A319TS
{
    [Serializable]
    public class Node
    {
        public List<Road> Roads = new List<Road>();
        public Point Position;
        private Node() { } // Serialize
        public Node(Point position)
        {
            Position = position;
        }
        public override string ToString()
        {
            return "(" + Position.X + "," + Position.Y + ")"; 
        }
    }
}

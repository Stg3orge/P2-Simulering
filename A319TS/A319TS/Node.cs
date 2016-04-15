using System;
using System.Collections.Generic;
using System.Drawing;

namespace A319TS
{
    [Serializable]
    public class Node
    {
        public List<Road> Roads;
        public Point Position;
        private Node() { } // Serialize
        public Node(Point position)
        {
            Position = position;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace A319TS
{
    [Serializable]
    public class Node
    {
        public List<Road> Roads;
        public Point Position;
        private Node(){} // Serialize
        public Node(Point position)
        {
            Position = position;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace A319TS
{
    [Serializable]
    public class Road
    {
        public Node Destination;
        public RoadType Type;
        private Road(){} // Serialize
        public Road(Node destination, RoadType type)
        {
            Destination = destination;
            Type = type;
        }

        public double Cost { get; set; }

    }
}

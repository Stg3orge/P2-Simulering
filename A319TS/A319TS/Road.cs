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
        public Node From { get; set; }
        public Node Destination { get; set; }
        public RoadType Type { get; set; }
        public enum RoadDifferentiation { Primary, Secondary, Shared };
        public RoadDifferentiation Differentiation;

        protected Road() { } // Serialize
        public Road(Node from, Node destination, RoadType type, RoadDifferentiation differentiation = RoadDifferentiation.Shared) // added default value
        {
            From = from;
            Destination = dest;
            Type = type;
            Differentiation = differentiation;
        }

        private double GetLength()
        {
            Differentiation = diff;
        }
    }
}
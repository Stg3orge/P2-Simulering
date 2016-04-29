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
        public RoadDifferentiation Differentiation { get; set; }
        public double Length { get { return GetLength(); } }
        public double Cost { get; set; }

        protected Road() { } // Serialize
        public Road(Node from, Node destination, RoadType type, RoadDifferentiation differentiation = RoadDifferentiation.Shared) // added default value
        {
            From = from;
            Destination = destination;
            Type = type;
            Differentiation = differentiation;
        }

        private double GetLength()
        {
            return (Math.Sqrt(Math.Pow(Math.Abs(From.Position.X - Destination.Position.X), 2)
                            + Math.Pow(Math.Abs(From.Position.Y - Destination.Position.Y), 2)));
        }
    }
}
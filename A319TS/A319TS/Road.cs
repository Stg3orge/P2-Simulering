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
        public Node To { get; set; }
        public RoadType Type { get; set; }
        public Partitions Partition { get; set; }
        public List<Vehicle> Vehicles { get; set; }
        public double Length
        {
            get { return MathExtension.Distance(From.Position, To.Position); }
        }
        
        public Road(Node from, Node to, RoadType type, Partitions partition)
        {
            From = from;
            To = to;
            Type = type;
            Partition = partition;
            Vehicles = new List<Vehicle>();
        }
    }
}
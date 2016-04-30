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
        public Partitions Partition;

        protected Road() { } // Serialize
        public Road(Node from, Node dest, RoadType type, Partitions partition)
        {
            From = from;
            Destination = dest;
            Type = type;
            Partition = partition;
        }
    }
    public enum Partitions { Shared, Primary, Secondary };
}
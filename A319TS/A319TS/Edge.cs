using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A319TS
{
    class Edge : Road
    {
        public Road Source;
        public Vertex VertexFrom;
        public Vertex VertexTo;
        public double Cost { get; private set; }

        public Edge(Road road, Vertex from, Vertex to) : base(road.From, road.To, road.Type, road.Partition)
        {
            Source = road;
            VertexFrom = from;
            VertexTo = to;
            Cost = MathExtension.Distance(VertexFrom.Position, VertexTo.Position) / Type.Speed;
        }
    }
}
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
        public double Length { get; private set; }
        public double Cost { get; private set; }

        public Edge(Road road, Vertex from, Vertex to) : base(road.From, road.To, road.Type, road.Partition)
        {
            Source = road;
            VertexFrom = from;
            VertexTo = to;
            Cost = GetLength() / Type.Speed;
        }

        private double GetLength()
        {
            return (Math.Sqrt(Math.Pow(Math.Abs(VertexFrom.Position.X - VertexTo.Position.X), 2)
                            + Math.Pow(Math.Abs(VertexFrom.Position.Y - VertexTo.Position.Y), 2)));
        }
    }
}

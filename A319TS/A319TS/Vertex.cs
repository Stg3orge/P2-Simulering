using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A319TS
{
    class Vertex : Node, IComparable<Vertex>, IPositionable
    {
        public Node Source;
        public List<Edge> Edges;
        public Vertex Previous;
        public double Cost;
        public double Estimate;

        public Vertex(Node node) : base(node.Type, node.Roads, node.Position, node.Green)
        {
            Source = node;
            Edges = new List<Edge>();
            Cost = Double.MaxValue;
            Estimate = Double.MaxValue;
        }

        public int CompareTo(Vertex other)
        {
            if (this.Estimate < other.Estimate) return -1;
            if (this.Estimate == other.Estimate) return 0;
            return 1;
        }
        public void CalculateCostEstimate(Vertex previous, Edge edge, Vertex end, int maxSpeed)
        {
            Cost = previous.Cost + edge.Cost;
            double heuristic = MathExtension.Distance(this.Position, end.Position) / maxSpeed;
            Estimate = Cost + heuristic;
        }
    }
}

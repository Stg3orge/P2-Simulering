using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace A319TS
{
    public enum NodeTypes { None, Yield, Home, Parking, Light, Inbound, Outbound }

    [Serializable]
    public class Node : IPositionable
    {
        public NodeTypes Type { get; set; }
        public List<Road> Roads = new List<Road>();
        public Point Position { get; set; }
        public bool Green { get; set; }
        
        public Node(Point position)
        {
            Position = position;
            Type = NodeTypes.None;
            Green = true;
        }

        public override string ToString()
        {
            return "(" + Position.X + "," + Position.Y + ") " + Type;
        }
        
        // Vertex Constructor
        protected Node(NodeTypes type, List<Road> roads, Point position, bool green)
        {
            Type = type;
            Roads = roads;
            Position = position;
            Green = green;
        }
    }
}
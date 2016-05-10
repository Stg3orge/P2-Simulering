using System;
using System.Collections.Generic;
using System.Drawing;

namespace A319TS
{
    public enum NodeTypes { None, Yield, Home, Parking, Light, Inbound, Outbound }

    [Serializable]
    public class Node : IPositionable
    {
        public NodeTypes Type { get; set; }
        public List<Road> Roads { get; set; }
        public Point Position { get; set; }
        public bool Green { get; set; }
        public List<Vehicle> IncomingVehicles { get; set; }
        public bool IsEmpty { get; set; }


        public Node(Point position)
        {
            Type = NodeTypes.None;
            Roads = new List<Road>();
            Position = position;
            Green = true;
            IncomingVehicles = new List<Vehicle>();
            IsEmpty = true;
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
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace A319TS
{
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

        /*

        // A star stuff //
        public Node(int x, int y, bool isWalkable, Point endNodeLocation)
        {
            Position = new Point(x, y);
            State = NodeState.Untested;
            IsWalkable = isWalkable;
            H = CalculateCost(Position, endNodeLocation);
            G = 0;
        }
        
        // True when the node may be traversed, otherwise false
        public bool IsWalkable { get; set; }

        // Cost from start node to here
        public float G { get; private set; }

        // Estimated cost from here to end node
        public float H { get; private set; }

        // Flags whether the node is open, closed or untested by the PathFinder
        public enum NodeState { Untested, Open, Closed }
        public NodeState State { get; set; }

        // Estimated total cost (F = G + H)
        public float F
        {
            get { return G + H; }
        }

        // Gets or sets the parent node. The start node's parent will always be null.
        private Node parentNode;
        public Node ParentNode
        {
            get { return parentNode; }
            set
            {
                // When setting the parentnode, the traversal cost from the start node to here (the 'G' value) will be calculated
                parentNode = value;
                G = parentNode.G + CalculateCost(Position, parentNode.Position);
            }
        }

        // Gets the distance cost between two points
        internal static float CalculateCost(Point position1, Point position2)
        {
            float deltaX = position2.X - position1.X;
            float deltaY = position2.Y - position1.Y;
            return (float)Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
        }

        */
    }
    public enum NodeTypes { None, Yield, Home, Parking, Light, Inbound, Outbound }
}
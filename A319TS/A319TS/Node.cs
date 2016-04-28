using System;
using System.Collections.Generic;
using System.Drawing;

namespace A319TS
{
    [Serializable]
    public class Node
    {
        public enum NodeType { Yield, Home, Parking, Light, None }
        public NodeType Type;
        public List<Road> Roads = new List<Road>();
        public Point Position;
        public bool Green;
        public int ID { get { return Position.GetHashCode(); } }

        protected Node() { } // Serialize
        public Node(Point position, NodeType type) : this(position)
        {
            Type = type;
        }
        public Node(Point position)
        {
            Position = position;
            Type = NodeType.None;
            Green = true;
        }


        public override string ToString()
        {
            return "(" + Position.X + "," + Position.Y + ") " + Type;
        }




        // A star stuff //

        public Node(int x, int y, bool isWalkable, Point endNodeLocation)
        {
            Position = new Point(x, y);
            State = NodeState.Untested;
            IsWalkable = isWalkable;
            H = CalculateCost(Position, endNodeLocation);
            G = 0;
        }


        public enum NodeState { Untested, Open, Closed }

        private Node parentNode;

        // True when the node may be traversed, otherwise false
        public bool IsWalkable { get; set; }

        // Cost from start node to here
        public float G { get; private set; }

        // Estimated cost from here to end node
        public float H { get; private set; }

        // Flags whether the node is open, closed or untested by the PathFinder
        public NodeState State { get; set; }

        // Estimated total cost (F = G + H)
        public float F
        {
            get { return G + H; }
        }

        // Gets or sets the parent node. The start node's parent will always be null.
        public Node ParentNode
        {
            get { return parentNode; }
            set
            {
                // When setting the parentnide, the traversal cost from the start node to here (the 'G' value) will be calculated
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

    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace A319TS
{
    class Pathfinder
    {
        /*

        private int width;
        private int height;
        private Node[,] nodes;
        private Node startNode;
        private Node endNode;

        public Point StartNodeLocation { get; set; }
        public Point EndNodeLocation { get; set; }
        public bool[,] Gridmap { get; set; }


        public List<Point> FindPath(Point startLocation, Point endLocation, bool[,] gridmap)
        {
            StartNodeLocation = startLocation;
            EndNodeLocation = endLocation;
            Gridmap = gridmap;

            InitializeNodes(Gridmap);
            startNode = nodes[StartNodeLocation.X, StartNodeLocation.Y];
            startNode.State = Node.NodeState.Open;
            endNode = nodes[EndNodeLocation.X, EndNodeLocation.Y];

            // PathFinding - A Star
            List<Point> path = new List<Point>();
            bool success = Search(startNode);
            
            if (success)
            {
                // If a path was found, follow the parents from the end node to build a list of locations
                Node node = endNode;
                while (node.ParentNode != null)
                {
                    path.Add(node.Position);
                    node = node.ParentNode;
                }

                // Reverse the list so it's in the correct order when returned
                path.Reverse();
            }

            return path;
        }

        // Creating the nodes[] and calculates the cost to endNode for each node. Also sets if(walkeable)
        private void InitializeNodes(bool[,] gridmap)
        {
            width = gridmap.GetLength(0);
            height = gridmap.GetLength(1);
            nodes = new Node[width, height];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    nodes[x, y] = new Node(x, y, gridmap[x, y], EndNodeLocation);
                }
            }
        }


        private bool Search(Node currentNode)
        {
            // Sets the current node state to Closed, since it cannot be traversed more than once
            currentNode.State = Node.NodeState.Closed;
            List<Node> nextNodes = GetAdjacentWalkableNodes(currentNode);

            // Sort nodes[] by F-value so that the shortest possible routes are considered first
            nextNodes.Sort((node1, node2) => node1.F.CompareTo(node2.F));

            foreach (Node node in nextNodes)
            {
                // Check if the endnode has been reached
                if (node.Position == endNode.Position)
                {
                    return true;
                }
                else
                {
                    // Recursive : until the endnode is reached
                    if (Search(node))
                        return true;
                }
            }

            // The method returns false if the path leads to a dead end
            return false;
        }


        private List<Node> GetAdjacentWalkableNodes(Node currentNode)
        {
            List<Node> walkableNodes = new List<Node>();
            IEnumerable<Point> adjacenPositions = GetAdjacentPositions(currentNode.Position);

            foreach (Point location in adjacenPositions)
            {
                int x = location.X;
                int y = location.Y;

                // Makeing sure positions aint out of grid size
                if (x < 0 || x >= width || y < 0 || y >= height)
                {
                    continue;
                }

                Node node = nodes[x, y];
                // Ignore non-walkable nodes
                if (!node.IsWalkable)
                {
                    continue;
                }

                // Ignore already-closed nodes
                if (node.State == Node.NodeState.Closed)
                {
                    continue;
                }

                // Already open nodes will only be added to the list if their G-value is lower by going the current route
                if (node.State == Node.NodeState.Open)
                {
                    float travelCost = Node.CalculateCost(node.Position, node.ParentNode.Position);
                    float gValue = currentNode.G + travelCost;

                    if (gValue < node.G)
                    {
                        node.ParentNode = currentNode;
                        walkableNodes.Add(node);
                    }
                }
                else
                {
                    // If the node is untested. Sets the parent and flag it as Open
                    node.ParentNode = currentNode;
                    node.State = Node.NodeState.Open;
                    walkableNodes.Add(node);
                }
            }

            return walkableNodes;
        }

        // Gets the locations of all the adjancent nodes from a current node
        private IEnumerable<Point> GetAdjacentPositions(Point currentNode)
        {
            return new Point[]
            {
                new Point(currentNode.X-1, currentNode.Y-1),
                new Point(currentNode.X-1, currentNode.Y  ),
                new Point(currentNode.X-1, currentNode.Y+1),
                new Point(currentNode.X,   currentNode.Y+1),
                new Point(currentNode.X+1, currentNode.Y+1),
                new Point(currentNode.X+1, currentNode.Y  ),
                new Point(currentNode.X+1, currentNode.Y-1),
                new Point(currentNode.X,   currentNode.Y-1)
            };
        }





        // Test Metode til print i console
        public void ShowRoute(IEnumerable<Point> path, bool[,] gridmap)
        {
            for (int y = gridmap.GetLength(1) - 1; y >= 0; y--) // Invert the Y-axis so that coordinate 0,0 is shown in the bottom-left
            {
                for (int x = 0; x < gridmap.GetLength(0); x++)
                {
                    if (StartNodeLocation.Equals(new Point(x, y)))
                        // Show the start position
                        Console.Write('A');
                    else if (EndNodeLocation.Equals(new Point(x, y)))
                        // Show the end position
                        Console.Write('B');
                    else if (gridmap[x, y] == false)
                        // Show any barriers
                        Console.Write('░');
                    else if (path.Where(p => p.X == x && p.Y == y).Any())
                        // Show the path in between
                        Console.Write('*');
                    else
                        // Show nodes that aren't part of the path
                        Console.Write('·');
                }

                Console.WriteLine();
            }
        }

        */
    }
}

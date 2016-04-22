using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace A319TS
{
    static class Pathfinder
    {
        
        // Resulting arrays {disk = arc, path = nodes}(components to create final graph)
        static private double[] dist { get; set; }
        static private double[] path { get; set; }

        // Holds queue for the nodes to be evaluated 
        static private List<int> queue = new List<int>();   // NOTE: Use nodeList[i].ID insted?


        static double[,] FindPath(List<Node> nodeList, Node startNode, Node endNode)
        {
            double[,] adj = CreateAdjMatrix(nodeList);

            Dijkstra(adj, startNode, endNode);

            // Createing final route
            double[,] Route = To2DArray(path, dist);

            return Route;
        }

        // Creates adj Matrix from a List of Nodes
        static private double[,] CreateAdjMatrix(List<Node> nodeList)
        {
            double[,] adj = new double[nodeList.Count, nodeList.Count];

            for (int i = 0; i < nodeList.Count; i++)
            {
                Node node1 = nodeList[i];

                for (int j = 0; j < nodeList.Count; j++)
                {
                    Node node2 = nodeList[j];

                    Road arc = node1.Roads.FirstOrDefault(a => a.Destination == node2);

                    if (arc != null)
                    {
                        adj[i, j] = arc.Length;
                    }
                }
            }
            return adj;
        }

        // Takes a adjacency matrix[graph], and a start/end nodes
        static private void Dijkstra(double[,] adj, Node startNode, Node endNode)
        {
            int len = adj.GetLength(0);

            Initialize(startNode.ID, len);

            while (queue.Count > 0)
            {
                int u = GetNextVertex();

                // Find the nodes that u connects to and perform relax
                for (int v = 0; v < len; v++)
                {
                    // Checks for edges(roads) with negative weight
                    if (adj[u, v] < 0)
                    {
                        throw new ArgumentException("Graph contains road(s) with negative length");
                    }
                    // checking if the next node is the endNode
                    if(u == endNode.ID)
                    {
                        break;
                    }

                    // Check for an edge(road) between u and v
                    if (adj[u, v] > 0)
                    {
                        // Edge exists
                        if (dist[v] > dist[u] + adj[u, v])
                        {
                            dist[v] = dist[u] + adj[u, v];
                            path[v] = u;
                        }
                    }
                }
            }
        }

        // Sets up initial settings
        static private void Initialize(int startNodeID, int len)
        {
            dist = new double[len];
            path = new double[len];

            // Set distance to all nodes to MAX
            for (int i = 0; i < len; i++)
            {
                dist[i] = double.MaxValue;

                queue.Add(i);
            }

            // Set distance to 0 for starting point and the previous node to null (-1)
            dist[startNodeID] = 0;
            path[startNodeID] = -1;
        }

        // Retrives next node to evaluate from the queue
        static private int GetNextVertex()
        {
            double min = double.MaxValue;
            int Vertex = -1;

            // Search through queue to find the next node having the smallest distance
            foreach (int j in queue)
            {
                if (dist[j] <= min)
                {
                    min = dist[j];
                    Vertex = j;
                }
            }

            queue.Remove(Vertex);

            return Vertex;
        }



        // http://stackoverflow.com/questions/18735376/combining-two-double-arrays-into-double/18735632#18735632
        static private T[,] To2DArray<T>(params T[][] arrays)
        {
            if (arrays == null) throw new ArgumentNullException("arrays");
            foreach (var a in arrays)
            {
                if (a == null) throw new ArgumentException("can not contain null arrays");
                if (a.Length != arrays[0].Length) throw new ArgumentException("input arrays should have the same length");
            }

            var height = arrays.Length;
            var width = arrays[0].Length;

            var result = new T[width, height];

            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                {
                    result[i, j] = arrays[i][j];
                }

            return result;
        }
        
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;

namespace A319TS
{
    static class Pathfinder
    {

        /* Resulting arrays with distances to nodes and how to get there */
        static double[] dist { get; set; }
        static int[] path { get; set; }

        /* Queue List */
        static List<int> nodeQueue = new List<int>();

        static List<Point> FindPath(List<Node> nodeList, Point startPoint, Point endPoint)
        {
            List<Point> Route = new List<Point>();

            CreateRoute(nodeList, startPoint, endPoint);

            return Route;
        }


        static void CreateRoute(List<Node> nodeList, Point startPoint, Point endPoint)
        {
            if (nodeList.Count <= 0)
            {
                throw new ArgumentException("List of nodes is empty.");
            }



            int nodeListLen = nodeList.Count;
            dist = new double[nodeListLen];
            path = new int[nodeListLen];

            for (int i = 0; i < nodeListLen; i++)
            {
                dist[i] = int.MaxValue;
                nodeQueue.Add(i);
            }

            /* Set distance to 0 for starting point and the previous node to null (-1) */
            //dist[startPoint] = 0;
            //path[startPoint] = -1;


            while (nodeQueue.Count != null)
            {



            }
        }
    }
}

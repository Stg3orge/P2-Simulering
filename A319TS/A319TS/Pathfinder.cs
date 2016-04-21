using System;
using System.Collections.Generic;
using System.Drawing;

namespace A319TS
{
    static class Pathfinder
    {

        /* Resulting arrays with distances to nodes and how to get there */
        //static double[] dist { get; set; }
        static int[] path { get; set; }

        /* Queue List */
        //static List<int> nodeQueue = new List<int>();

        static List<Point> FindPath(List<Node> nodeList, Node startNode, Node endNode)
        {
            List<Point> Route = new List<Point>();

            CreateRoute(nodeList, startNode, endNode);

            return Route;
        }


        static void CreateRoute(List<Node> nodeList, Node startNode, Node endNode)
        {
    
            int nodeListLen = nodeList.Count;
            //dist = new double[nodeListLen];
            path = new int[nodeListLen];

            if (nodeListLen <= 0)
            {
                throw new ArgumentException("List of nodes is empty.");
            }


            /* Initalize */
            for (int i = 0; i < nodeListLen; i++)
            {
                foreach (Road item in nodeList[i].Roads)
                {
                    item.Cost = double.MaxValue;
                }
            }

            /* Needs startnode.cost == 0, and pervious node == -1*/

            CalcuelateCost(nodeList, nodeListLen, startNode);


        }


        static void CalcuelateCost(List<Node> nodeList, int nodeListLen, Node startNode)
        {
            Node currentNode = startNode;

            while (nodeListLen > 0)
            {
                Tuple<int, int> lowestCost = FindLowestCost(nodeList, nodeListLen, currentNode);
                


            }

        }


        static Tuple<int, int> FindLowestCost(List<Node> nodeList, int nodeListLen, Node currentNode)
        {
            int lowestNodeCostIndex = 0;
            int lowestRoadCostIndex = 0;
            double lowestValue = double.MaxValue;

            for (int i = 0; i < nodeListLen; i++)
            {
                for (int n = 0; n < nodeList[i].Roads.Count; n++)
                {
                    if (nodeList[i].Roads[n].Cost < lowestValue)
                    {
                        lowestValue = nodeList[i].Roads[n].Cost;
                        lowestNodeCostIndex = i;
                        lowestRoadCostIndex = n;
                    }
                }
            }

            if (lowestValue == double.MaxValue)
            {
                throw new ArgumentException("Lowest Road Length was never changed from default.");
            }

             return Tuple.Create(lowestNodeCostIndex, lowestRoadCostIndex);
        }
















}
}

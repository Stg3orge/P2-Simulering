﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A319TS
{
    static class Pathwerk
    {
        // SetProject takes a project and converts the nodes and roads to vertices and edges. Only has to be done once.
        public static void SetProject(Project project, Partitions partition)
        {
            Vertices = new List<Vertex>();
            ConvertNodes(project);
            ConvertRoads(project, partition);
            MaxSpeed = project.RoadTypes.Max().Speed;
        }
        private static void ConvertNodes(Project project)
        {
            foreach (Node node in project.Nodes)
                Vertices.Add(new Vertex(node));
        }
        private static void ConvertRoads(Project project, Partitions partition)
        {
            foreach (Node node in project.Nodes)
                foreach (Road road in node.Roads)
                    foreach (Vertex vertex in Vertices)
                        if (road.From.Position == vertex.Position && (road.Partition == partition || road.Partition == Partitions.Shared))
                            vertex.Edges.Add(new Edge(road, vertex, Vertices.Find(v => v.Position == road.Destination.Position)));
        }

        private static int MaxSpeed;
        private static List<Vertex> Vertices;
        private static List<Vertex> Closed;
        private static List<Vertex> Open;
        private static Vertex Start;
        private static Vertex End;

        // Initialize to remove old data;
        private static void InitLists()
        {
            Closed = new List<Vertex>();
            Open = new List<Vertex>();
            Start = null;
            End = null;
        }
        private static void SetStartEnd(Node start, Node end)
        {
            foreach (Vertex vertex in Vertices)
            {
                if (vertex.Position == start.Position)
                    Start = vertex;
                else if (vertex.Position == end.Position)
                    End = vertex;
            }
        }

        public static List<Road> FindPath(Node start, Node end)
        {
            if (Vertices == null || start == null || end == null)
                throw new ArgumentNullException("Why you want hurt Path?");

            InitLists();
            SetStartEnd(start, end);
            Start.Cost = 0;
            Open.Add(Start);

            Vertex current;
            while (Open.LongCount() > 0)
            {
                current = Open.Min();
                Console.WriteLine(current.ToString());
                if (current == End)
                {
                    return TracePath();
                } 
                else
                {
                    MoveToClosed(current);
                    EstimateNeighbors(current);
                }
            }

            throw new Exception("No more play?"); // Start doesn't connect with end.
        }
        
        private static void MoveToClosed(Vertex vertex)
        {
            Open.Remove(vertex);
            Closed.Add(vertex);
        }
        // Looks at neighbor vertices that haven't been evaluated, and evaluates them.
        private static void EstimateNeighbors(Vertex current)
        {
            foreach (Edge edge in current.Edges)
            {
                Vertex neighbor = edge.VertexTo;
                if (!Open.Contains(neighbor) && !Closed.Contains(neighbor)) // Skip evaluated
                {
                    neighbor.CalculateEstimate(current, edge, End, MaxSpeed); 
                    Open.Add(neighbor);
                    if (neighbor.Cost <= current.Cost + edge.Cost) 
                        neighbor.Previous = current;
                }
            }
        }
        // Traces the path from the end verte to the start vertex, via vertex.Previous.
        private static List<Road> TracePath()
        {
            List<Road> roads = new List<Road>();
            Vertex current = End;
            while (current.Previous != null) // When the previous vertex is null, we are at the start.
            {
                roads.Add(current.Previous.Edges.Find(edge => edge.VertexTo == current).Source);
                current = current.Previous;
            }
            roads.Reverse();
            return roads;
        }
    }
}
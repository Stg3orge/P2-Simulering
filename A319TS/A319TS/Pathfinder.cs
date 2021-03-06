﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace A319TS
{
    static class Pathfinder
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
                            vertex.Edges.Add(new Edge(road, vertex, Vertices.Find(v => v.Position == road.To.Position)));
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
            Vertices.ForEach(v => v.Previous = null);
            Vertices.ForEach(v => v.Cost = double.MaxValue);
            Vertices.ForEach(v => v.Estimate = double.MaxValue);
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
                throw new ArgumentNullException();

            InitLists();
            SetStartEnd(start, end);
            Start.Cost = 0;
            Open.Add(Start);

            Vertex current;
            while (Open.Count > 0)
            {
                current = Open.Min();
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

            throw new Exception("No route found between " + start + " and " + end);
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
                    Open.Add(neighbor);
                    double PossibleCost = current.Cost + edge.Cost;
                    if (neighbor.Cost > PossibleCost)
                    {
                        neighbor.Cost = PossibleCost;
                        neighbor.Previous = current;
                        double heuristic = MathExtension.Distance(neighbor.Position, End.Position) / MaxSpeed;
                        neighbor.Estimate = neighbor.Cost + heuristic;
                    }
                }
            }
        }
        // Traces the path from the end vertex to the start vertex, via vertex.Previous.
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
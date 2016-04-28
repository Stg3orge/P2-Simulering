﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace A319TS
{
    [Serializable]
    public class Road
    {
<<<<<<< HEAD
        public Node From;
        public Node Destination;
        public RoadType Type;
        public enum RoadDifferentiation { Primary, Secondary, Shared };
        public RoadDifferentiation Differentiation;
=======
        public Node From { get; set; }
        public Node Destination { get; set; }
        public RoadType Type { get; set; }
        public enum RoadDifferentiation {Primary, Secondary, Shared };
>>>>>>> b4df7891640376b80944b35bbd0bb0db3f8c048e
        public double Length { get { return GetLength(); } }
        public double Cost { get; set; }

        private Road(){} // Serialize
        public Road(Node from, Node destination, RoadType type)
        {
            From = from;
            Destination = destination;
            Type = type;
        }

        private double GetLength()
        {
            return (Math.Sqrt(Math.Pow(Math.Abs(From.Position.X - Destination.Position.X), 2) 
                            + Math.Pow(Math.Abs(From.Position.Y - Destination.Position.Y), 2)));
        }
    }
}

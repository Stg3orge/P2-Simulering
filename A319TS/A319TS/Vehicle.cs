using System;
using System.Drawing;

namespace A319TS
{
    public class Vehicle : IPositionable
    {
        public Node Home;
        public Destination Destination;
        public Point Position { get; set; }
        public VehicleType Type;
        public int ToDestTime;
        public int ToHomeTime;
        
        public Vehicle(Node home, Destination dest, VehicleType type, int toDestTime, int toHomeTime)
        {
            Home = home;
            Destination = dest;
            Position = Home.Position;
            Type = type;
            ToDestTime = toDestTime;
            ToHomeTime = toHomeTime;
        }

        public PointF Drive(int stepSize)
        {
            return new PointF(-1, -1);
        }
    }
}
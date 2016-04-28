using System;
using System.Drawing;

namespace A319TS
{
    [Serializable]
    class Vehicle
    {
        public Node Home;
        public Destination Destination;
        public Point Position;
        public VehicleType Type;
        public int ToDestTime;
        public int ToHomeTime;

        protected Vehicle(){} // Serialize
        public Vehicle(Node home, Destination destination, VehicleType type, int toDestTime, int toHomeTime)
        {
            Home = home;
            Destination = destination;
            Position = Home.Position;
            Type = type;
            ToDestTime = toDestTime;
            ToHomeTime = toHomeTime;
        }
    }
}
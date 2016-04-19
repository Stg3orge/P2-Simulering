using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace A319TS
{
    class Vehicle
    {
        public Node Home;
        public Destination Destination;
        public Point Position;
        public VehicleType Type;
        public int TravelTime;
        public int HomeTime;

        public Vehicle(Node home, Destination destination, VehicleType type, int travelTime, int homeTime)
        {
            Home = home;
            Destination = destination;
            Position = Home.Position;
            Type = type;
            TravelTime = travelTime;
            HomeTime = homeTime;
        }
    }
}

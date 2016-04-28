using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace A319TS
{
    public class VehicleType
    {
        public string Name { get; set; }
        public int MaxSpeed { get; set; }
        public double Acceleration { get; set; }
        public double Deceleration { get; set; }

        public VehicleType(string name, int maxSpeed, double acceleration, double deceleration)
        {
            Name = name;
            MaxSpeed = maxSpeed;
            Acceleration = acceleration;
            Deceleration = deceleration;
        }
    }
}

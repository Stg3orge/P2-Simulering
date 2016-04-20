using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace A319TS
{
    class VehicleType
    {
        public string Name;
        public int MaxSpeed;
        public int Acceleration;
        public int Deceleration;

        public VehicleType(string name, int maxSpeed, int acceleration, int deceleration)
        {
            Name = name;
            MaxSpeed = maxSpeed;
            Acceleration = acceleration;
            Deceleration = deceleration;
        }
    }
}

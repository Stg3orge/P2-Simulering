using System;
using System.Drawing;

namespace A319TS
{
    [Serializable]
    public class VehicleType : IColorable, IDistributable
    {
        public string Name { get; set; }
        public int MaxSpeed { get; set; }
        public double Acceleration { get; set; }
        public double Deceleration { get; set; }
        public Color Color { get; set; }
        public double Distribution { get; set; }

        protected VehicleType(){} // Serialize
        public VehicleType(string name, int maxSpeed, double acceleration, double deceleration, Color color)
        {
            Name = name;
            MaxSpeed = maxSpeed;
            Acceleration = acceleration;
            Deceleration = deceleration;
            Color = color;
            Distribution = 0;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
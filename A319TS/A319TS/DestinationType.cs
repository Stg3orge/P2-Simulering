using System;
using System.Drawing;

namespace A319TS
{
    [Serializable]
    public class DestinationType
    {
        public string Name { get; set; }
        public Color Color { get; set; }
        public double Destribution { get; set; }

        private DestinationType(){} // Serialize
        public DestinationType(string name, Color color)
        {
            Name = name;
            Color = color;
            Destribution = 0;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
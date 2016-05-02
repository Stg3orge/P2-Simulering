using System;
using System.Drawing;

namespace A319TS
{
    [Serializable]
    public class DestinationType : IColorable, IDistributable
    {
        public string Name { get; set; }
        public Color Color { get; set; }
        public double Distribution { get; set; }
        
        public DestinationType(string name, Color color)
        {
            Name = name;
            Color = color;
            Distribution = 0;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
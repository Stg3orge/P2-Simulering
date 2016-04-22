using System;
using System.Drawing;

namespace A319TS
{
    [Serializable]
    public class DestinationType
    {
        public string Name;
        public Color Color;

        private DestinationType(){} // Serialize
        public DestinationType(string name, Color color)
        {
            Name = name;
            Color = color;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace A319TS
{
    public class DestinationType
    {
        public string Name;
        public Color Color;

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

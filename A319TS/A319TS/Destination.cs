using System;
using System.Drawing;

namespace A319TS
{
    [Serializable]
    public class Destination : IPositionable
    {
        public DestinationType Type { get; set; }
        public Point Position { get; set; }

        public Destination(Point position, DestinationType type)
        {
            Position = position;
            Type = type;
        }

        public override string ToString()
        {
            return "(" + Position.X + "," + Position.Y + ") " + Type.Name;
        }
    }
}
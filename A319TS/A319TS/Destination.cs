using System;
using System.Drawing;

namespace A319TS
{
    [Serializable]
    public class Destination : Entity, IPositionable
    {
        public DestinationType Type { get; set; }

        public Destination(Point position, DestinationType type) : base(position)
        {
            Type = type;
        }

        public override string ToString()
        {
            return "(" + Position.X + "," + Position.Y + ") " + Type.Name;
        }
    }
}
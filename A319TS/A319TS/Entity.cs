using System;
using System.Drawing;

namespace A319TS
{
    [Serializable]
    public abstract class Entity : IPositionable
    {
        public Point Position { get; set; }
        public Entity(Point position)
        {
            Position = position;
        }
    }
}
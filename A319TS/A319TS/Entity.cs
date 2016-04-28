using System;
using System.Drawing;

namespace A319TS
{
    [Serializable]
    public abstract class Entity
    {
        public Point Position { get; set; }
        protected Entity(){} // Serialize
        public Entity(Point position)
        {
            Position = position;
        }
    }
}
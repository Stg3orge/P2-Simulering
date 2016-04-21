using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace A319TS
{
    public abstract class Entity
    {
        public Point Position;
        public Entity(Point position)
        {
            Position = position;
        }
    }
}

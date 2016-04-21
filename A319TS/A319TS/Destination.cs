using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace A319TS
{
    public class Destination : Entity
    {
        public DestinationType Type;
        public Destination(Point position, DestinationType type) : base(position)
        {
            Type = type;
        }
    }
}

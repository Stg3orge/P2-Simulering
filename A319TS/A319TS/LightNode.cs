using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace A319TS
{
    class LightNode : Node
    {
        public bool Green = true;
        public LightNode(Point position) : base(position, NodeType.Light)
        {

        }
    }
}

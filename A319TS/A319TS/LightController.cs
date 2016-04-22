using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace A319TS
{
    public class LightController : Entity
    {
        public List<Node> Lights { get; private set; }
        public int FirstTime;
        public int SecondTime;
        private int _current;
        private int _counter = 0;

        public LightController(Point position) : base(position)
        {
            Lights = new List<Node>();
            FirstTime = 10;
            SecondTime = 10;
            _current = FirstTime;
            
        }

        public void AddLight(Node node)
        {
            if (node.Type != Node.NodeType.Light)
                throw new Exception("Wrong Type");
            else
                Lights.Add(node);
        }

        public void Update(int ms)
        {
            _counter += ms;
            if (_counter > _current)
            {
                if (_current == FirstTime)
                    _current = SecondTime;
                else
                    _current = FirstTime;
                ToggleLights();
                _counter = 0;
            }
        }

        private void ToggleLights()
        {
            foreach (Node light in Lights)
                light.Green = !light.Green;
        }
    }
}

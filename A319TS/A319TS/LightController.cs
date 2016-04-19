using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace A319TS
{
    class LightController : Entity
    {
        public List<LightNode> Lights = new List<LightNode>();
        public int FirstTime;
        public int SecondTime;
        private int _current;
        private int _counter = 0;

        public LightController(Point position, int firstTime, int secondTime) : base(position)
        {
            _current = firstTime;
            FirstTime = firstTime;
            SecondTime = secondTime;
            
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
            foreach (LightNode light in Lights)
                light.Green = !light.Green;
        }
    }
}

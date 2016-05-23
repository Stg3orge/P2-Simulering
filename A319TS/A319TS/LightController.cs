using System;
using System.Collections.Generic;
using System.Drawing;

namespace A319TS
{
    [Serializable]
    public class LightController : IPositionable
    {
        public Point Position { get; set; }
        public List<Node> Lights { get; private set; }
        public int FirstTime { get; set; }
        public int SecondTime { get; set; }
        private int _current;
        private int _counter = 0;
        
        public LightController(Point position)
        {
            Position = position;
            Lights = new List<Node>();
            FirstTime = 10000;
            SecondTime = 10000;
            _current = FirstTime;
            
        }

        public override string ToString()
        {
            return "(" + Position.X + "," + Position.Y + ") " + FirstTime + "/" + SecondTime;
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

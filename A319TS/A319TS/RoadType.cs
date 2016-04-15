using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A319TS
{
    [Serializable]
    public class RoadType
    {
        public string Name;
        public int Speed;
        private RoadType(){} // Serialize
        public RoadType(string name, int speed)
        {
            Name = name;
            Speed = speed;
        }
    }
}

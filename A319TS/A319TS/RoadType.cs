using System;

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

        public override string ToString()
        {
            return Name;
        }
    }
}
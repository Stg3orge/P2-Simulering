using System;

namespace A319TS
{
    [Serializable]
    public class RoadType : IComparable<RoadType>
    {
        public string Name { get; set; }
        public int Speed { get; set; }

        protected RoadType(){} // Serialize
        public RoadType(string name, int speed)
        {
            Name = name;
            Speed = speed;
        }

        public override string ToString()
        {
            return Name;
        }
        public int CompareTo(RoadType other)
        {
            if (this.Speed < other.Speed) return -1;
            if (this.Speed == other.Speed) return 0;
            return 1;
        }
    }
}
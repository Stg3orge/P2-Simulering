using System;
using System.Collections.Generic;

namespace A319TS
{
    [Serializable]
    public class Project
    {
        public string Name;
        public List<Node> Nodes = new List<Node>();
        public List<Destination> Destinations = new List<Destination>();
        public List<LightController> LightControllers = new List<LightController>();
        private Project() { } // Serialize
        public Project(string name)
        {
            Name = name;
        }
    }
}

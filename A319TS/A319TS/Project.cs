using System;
using System.Collections.Generic;

namespace A319TS
{
    [Serializable]
    public class Project
    {
        public string Name;
        public List<Node> Nodes = new List<Node>();
        private Project() { } // Serialize
        public Project(string name)
        {
            Name = name;
        }
    }
}

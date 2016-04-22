using System;
using System.Collections.Generic;
using System.Drawing;

namespace A319TS
{
    [Serializable]
    public class Project
    {
        public string Name;
        public List<Node> Nodes = new List<Node>();
        public List<Destination> Destinations = new List<Destination>();
        public List<LightController> LightControllers = new List<LightController>();
        public List<RoadType> RoadTypes = new List<RoadType>();
        public List<DestinationType> DestinationTypes = new List<DestinationType>();
        public List<VehicleType> VehicleTypes = new List<VehicleType>();
        private Project() { } // Serialize
        public Project(string name)
        {
            Name = name;
            RoadTypes.Add(new RoadType("Default", 50));
            DestinationTypes.Add(new DestinationType("Default", Color.LightSlateGray));
            VehicleTypes.Add(new VehicleType("Default", 130, 5, 5));
        }
    }
}

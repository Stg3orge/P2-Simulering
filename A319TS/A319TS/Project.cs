using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace A319TS
{
    [Serializable]
    public class Project : ICloneable
    {
        public string Name;
        public List<Node> Nodes = new List<Node>();
        public List<Destination> Destinations = new List<Destination>();
        public List<LightController> LightControllers = new List<LightController>();
        public List<RoadType> RoadTypes = new List<RoadType>();
        public List<DestinationType> DestinationTypes = new List<DestinationType>();
        public List<VehicleType> VehicleTypes = new List<VehicleType>();
        public List<SimulationData> Simulations = new List<SimulationData>();
        public SimulationSettings Settings = new SimulationSettings();
        
        public Project(string name)
        {
            Name = name;
            RoadTypes.Add(new RoadType("Default", 50));
            DestinationTypes.Add(new DestinationType("Default", Color.LightSlateGray) { Distribution = 100 });
            VehicleTypes.Add(new VehicleType("Default", 130, 5, 5, Color.LightSlateGray) { Distribution = 100 });
        }

        public object Clone()
        {
            MemoryStream memory = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(memory, this);
            memory.Position = 0;
            return formatter.Deserialize(memory);
        }
    }
}

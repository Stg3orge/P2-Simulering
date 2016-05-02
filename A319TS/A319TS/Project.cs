using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

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
            return DeepCopy(this);
        }

        // http://www.codeproject.com/Articles/38270/Deep-copy-of-objects-in-C
        private static object DeepCopy(object obj)
        {
            if (obj == null)
                return null;
            Type type = obj.GetType();

            if (obj is string || type.IsValueType)
                return obj;
            else if (type.IsArray)
            {
                Type elementType = Type.GetType(type.FullName.Replace("[]", string.Empty));
                var array = obj as Array;
                Array copied = Array.CreateInstance(elementType, array.Length);
                for (int i = 0; i < array.Length; i++)
                    copied.SetValue(DeepCopy(array.GetValue(i)), i);
                return Convert.ChangeType(copied, obj.GetType());
            }
            else if (type.IsClass)
            {
                object toret = Activator.CreateInstance(obj.GetType());
                FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                foreach (FieldInfo field in fields)
                {
                    object fieldValue = field.GetValue(obj);
                    if (fieldValue == null)
                        continue;
                    field.SetValue(toret, DeepCopy(fieldValue));
                }
                return toret;
            }
            else throw new ArgumentException("Unknown Type");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace A319TS
{
    [Serializable]
    public class SimulationData
    {
        public Project Project { get; private set; }
        public List<VehicleData> PrimaryData { get; private set; }
        public List<VehicleData> SecondaryData { get; private set; }
        public DateTime Date { get; private set; }
        public string Filename
        {
            get { return Project.Name + "_" + Date.ToString("dd") + "-" + Date.ToString("MM") + "-" + 
                    Date.ToString("yy") + "_" + Date.ToString("HH") + Date.ToString("mm") + ".sim"; }
        }
        
        public SimulationData(Project project, List<VehicleData> primary, List<VehicleData> secondary)
        {
            Project = project;
            PrimaryData = primary;
            SecondaryData = secondary;
            Date = DateTime.Now;
        }

        public override string ToString()
        {
            return Project.Name + " " + Date;
        }
    }
}

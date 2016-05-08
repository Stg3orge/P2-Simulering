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
        public List<Vehicle> PrimaryVehicles { get; private set; }
        public List<Vehicle> SecondaryVehicles { get; private set; }
        public PointD[,] PrimaryRecord { get; private set; }
        public PointD[,] SecondaryRecord { get; private set; }
        public DateTime Date { get; private set; }
        
        public SimulationData(Project project, List<Vehicle> primaryVehicles, List<Vehicle> secondaryVehicles, 
                              PointD[,] primaryRecord, PointD[,] secondaryRecord)
        {
            Project = project;
            PrimaryVehicles = primaryVehicles;
            SecondaryVehicles = secondaryVehicles;
            PrimaryRecord = primaryRecord;
            SecondaryRecord = secondaryRecord;
            Date = DateTime.Now;
        }

        public override string ToString()
        {
            return Date.ToString();
        }
    }
}

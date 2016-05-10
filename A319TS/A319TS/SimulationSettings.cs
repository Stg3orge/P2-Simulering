using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A319TS
{
    [Serializable]
    public class SimulationSettings
    {
        // Shared
        public int StepSize { get; set; }
        public int VehicleSpace { get; set; }
        public int IncommingRange { get; set; }

        // Primary
        public int PrimaryCarCount { get; set; }
        public int PrimaryInbound { get; set; }
        public int PrimaryOutbound { get; set; }
        public int PrimaryToDestTime { get; set; }
        public int PrimaryToHomeTime { get; set; }
        public int PrimaryTimeSpread { get; set; }

        // Secondary
        public int SecondaryCarCount { get; set; }
        public int SecondaryInbound { get; set; }
        public int SecondaryOutbound { get; set; }
        public int SecondaryToDestTime { get; set; }
        public int SecondaryToHomeTime { get; set; }
        public int SecondaryTimeSpread { get; set; }

        // Defaults
        public SimulationSettings()
        {
            StepSize = 100;
            VehicleSpace = 2;
            IncommingRange = 10;
            PrimaryCarCount = 10000;
            PrimaryInbound = 100;
            PrimaryOutbound = 100;
            PrimaryToDestTime = 28800000; // 08:00
            PrimaryToHomeTime = 57600000; // 16:00
            PrimaryTimeSpread = 14400000; // 4h
            SecondaryCarCount = 10000;
            SecondaryInbound = 100;
            SecondaryOutbound = 100;
            SecondaryToDestTime = 28800000; // 08:00
            SecondaryToHomeTime = 57600000; // 16:00
            SecondaryTimeSpread = 14400000; // 4h
        }
    }
}

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
        public int TrailingSpeed { get; set; }

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
            TrailingSpeed = 100;
            PrimaryCarCount = 1000;
            PrimaryInbound = 10;
            PrimaryOutbound = 10;
            PrimaryToDestTime = 28800000; // 08:00
            PrimaryToHomeTime = 57600000; // 16:00
            PrimaryTimeSpread = 14400000; // 4h
            SecondaryCarCount = 1000;
            SecondaryInbound = 10;
            SecondaryOutbound = 10;
            SecondaryToDestTime = 28800000; // 08:00
            SecondaryToHomeTime = 57600000; // 16:00
            SecondaryTimeSpread = 14400000; // 4h
        }
    }
}

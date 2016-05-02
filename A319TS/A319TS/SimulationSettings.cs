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

        // Primary
        public int PrimaryCarCount { get; set; }
        public int PrimaryInbound { get; set; }
        public int PrimaryOutbound { get; set; }

        // Secondary
        public int SecondaryCarCount { get; set; }
        public int SecondaryInbound { get; set; }
        public int SecondaryOutbound { get; set; }

        // Defaults
        public SimulationSettings()
        {
            StepSize = 100;
            PrimaryCarCount = 1000;
            PrimaryInbound = 100;
            PrimaryOutbound = 100;
            SecondaryCarCount = 1000;
            SecondaryInbound = 100;
            SecondaryOutbound = 100;
        }
    }
}

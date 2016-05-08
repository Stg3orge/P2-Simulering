using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A319TS
{
    [Serializable]
    public class VehicleData
    {
        public VehicleType Type { get; private set; }
        public PointD[] ToDestRecord { get; private set; }
        public PointD[] ToHomeRecord { get; private set; }
        public int ToDestTime { get; private set; }
        public int ToHomeTime { get; private set; }
        public VehicleData(VehicleType type, List<PointD> toDestRecord, List<PointD> toHomeRecord, int toDestTime, int toHomeTime)
        {
            Type = type;
            ToDestRecord = toDestRecord.ToArray();
            ToHomeRecord = toHomeRecord.ToArray();
            ToDestTime = toDestTime;
            ToHomeTime = toHomeTime;
        }
    }
}

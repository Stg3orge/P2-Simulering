using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace A319TS
{
    class SimulationViewport : Viewport
    {
        private const int VehicleSize = 16;
        public Layer Vehicles = new Layer();
        public SimulationData SimData;
        public Partitions CurrentPartition = Partitions.Primary;
        private int _time = 0;
        public int Time
        {
            get { return _time; }
            set
            {
                if (value > 86400000) _time = 86400000;
                else if (value < 0) _time = 0;
                else _time = value;
            }
        }

        public SimulationViewport(SimulationData data) : base(data.Project)
        {
            SimData = data;
            
            Vehicles.Dock = DockStyle.Fill;
            Vehicles.BackColor = Color.Transparent;
            Vehicles.Paint += DrawVehicles;

            Grid.Paint -= DrawGrid;
            Entities.Controls.Remove(Information);
            Entities.Controls.Add(Vehicles);
            Vehicles.Controls.Add(Input);
            Input.BringToFront();
        }

        private void DrawVehicles(object sender, PaintEventArgs args)
        {
            ScaleTranslateSmooth(SmoothingMode.HighQuality, args);

            List<VehicleData> vehicleData;
            if (CurrentPartition == Partitions.Primary) vehicleData = SimData.PrimaryData;
            else vehicleData = SimData.SecondaryData;

            int vehiclesDrawn = 0;

            foreach (VehicleData data in SimData.PrimaryData)
            {
                if (Time > data.ToDestTime && Time < data.ToDestTime + (data.ToDestRecord.Count() * Simulation.RecordInterval))
                {
                    DrawVehicle(data, true, args);
                    vehiclesDrawn++;
                }
                else if (Time > data.ToHomeTime && Time < data.ToHomeTime + (data.ToHomeRecord.Count() * Simulation.RecordInterval))
                {
                    DrawVehicle(data, false, args);
                    vehiclesDrawn++;
                }
            }
            Console.WriteLine("VDrawn: " + vehiclesDrawn);
        }
        private void DrawVehicle(VehicleData vehicle, bool toDest, PaintEventArgs args)
        {
            Pen pen = new Pen(vehicle.Type.Color);
            PointF position;
            if (toDest) position = GetDrawPosition(vehicle.ToDestRecord[GetRecordIndex(vehicle.ToDestTime)]);
            else position = GetDrawPosition(vehicle.ToHomeRecord[GetRecordIndex(vehicle.ToHomeTime)]);
            int offset = VehicleSize / 2;
            position.X -= offset;
            position.Y -= offset;
            args.Graphics.DrawEllipse(Pens.Black, position.X, position.Y, VehicleSize, VehicleSize);
            args.Graphics.DrawEllipse(pen, position.X + 1, position.Y + 1, VehicleSize - 2, VehicleSize - 2);
            args.Graphics.DrawEllipse(pen, position.X + 2, position.Y + 2, VehicleSize - 4, VehicleSize - 4);
            args.Graphics.DrawEllipse(Pens.Black, position.X + 3, position.Y + 3, VehicleSize - 6, VehicleSize - 6);
        }
        private PointF GetDrawPosition(PointD position)
        {
            return new PointF(Convert.ToSingle(position.X) * GridSize, Convert.ToSingle(position.Y) * GridSize);
        }
        private int GetRecordIndex(int recordStartTime)
        {
            return Convert.ToInt32((Time - recordStartTime) / Simulation.RecordInterval); // -1 ??
        }
    }
}

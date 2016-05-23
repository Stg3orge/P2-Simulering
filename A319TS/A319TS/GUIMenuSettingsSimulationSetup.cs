using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace A319TS
{
    partial class GUIMenuSettingsSimulation : Form
    {
        private void Setup()
        {
            Text = "Simulation Settings";
            Size = new Size(381, 416);
            MinimumSize = new Size(381, 416);
            MaximumSize = new Size(381, 416);
            ShowIcon = false;
            MinimizeBox = false;
            MaximizeBox = false;
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterParent;

            Partitions.Text = "Partitions";
            Partitions.Location = new Point(12, 12);
            Partitions.Size = new Size(339, 206);
            Controls.Add(Partitions);
            
            PrimaryVehicleCount.Location = new Point(147, 38);
            PrimaryVehicleCount.Size = new Size(90, 20);
            PrimaryVehicleCount.Maximum = 10000;
            Partitions.Controls.Add(PrimaryVehicleCount);

            PrimaryInbound.Location = new Point(147, 66);
            PrimaryInbound.Size = new Size(90, 20);
            Partitions.Controls.Add(PrimaryInbound);

            PrimaryOutbound.Location = new Point(147, 94);
            PrimaryOutbound.Size = new Size(90, 20);
            Partitions.Controls.Add(PrimaryOutbound);

            PrimaryTimeSpread.Location = new Point(147, 122);
            PrimaryTimeSpread.Size = new Size(90, 20);
            PrimaryTimeSpread.Maximum = Simulation.MsInDay;
            Partitions.Controls.Add(PrimaryTimeSpread);

            PrimaryToDestTime.Location = new Point(147, 150);
            PrimaryToDestTime.Size = new Size(90, 20);
            PrimaryToDestTime.Maximum = Simulation.MsInDay;
            Partitions.Controls.Add(PrimaryToDestTime);

            PrimaryToHomeTime.Location = new Point(147, 178);
            PrimaryToHomeTime.Size = new Size(90, 20);
            PrimaryToHomeTime.Maximum = Simulation.MsInDay;
            Partitions.Controls.Add(PrimaryToHomeTime);

            SecondaryVehicleCount.Location = new Point(243, 38);
            SecondaryVehicleCount.Size = new Size(90, 20);
            SecondaryVehicleCount.Maximum = 10000;
            Partitions.Controls.Add(SecondaryVehicleCount);

            SecondaryInbound.Location = new Point(243, 66);
            SecondaryInbound.Size = new Size(90, 20);
            Partitions.Controls.Add(SecondaryInbound);

            SecondaryOutbound.Location = new Point(243, 94);
            SecondaryOutbound.Size = new Size(90, 20);
            Partitions.Controls.Add(SecondaryOutbound);

            SecondaryTimeSpread.Location = new Point(243, 122);
            SecondaryTimeSpread.Size = new Size(90, 20);
            SecondaryTimeSpread.Maximum = Simulation.MsInDay;
            Partitions.Controls.Add(SecondaryTimeSpread);

            SecondaryToDestTime.Location = new Point(243, 150);
            SecondaryToDestTime.Size = new Size(90, 20);
            SecondaryToDestTime.Maximum = Simulation.MsInDay;
            Partitions.Controls.Add(SecondaryToDestTime);

            SecondaryToHomeTime.Location = new Point(243, 178);
            SecondaryToHomeTime.Size = new Size(90, 20);
            SecondaryToHomeTime.Maximum = Simulation.MsInDay;
            Partitions.Controls.Add(SecondaryToHomeTime);

            Primary.Text = "Primary";
            Primary.Location = new Point(144, 18);
            Primary.AutoSize = true;
            Partitions.Controls.Add(Primary);

            Secondary.Text = "Secondary";
            Secondary.Location = new Point(240, 18);
            Secondary.AutoSize = true;
            Partitions.Controls.Add(Secondary);

            VehicleCountLabel.Text = "Vehicle Count";
            VehicleCountLabel.Location = new Point(46, 40);
            VehicleCountLabel.AutoSize = true;
            Partitions.Controls.Add(VehicleCountLabel);

            InboundLabel.Text = "Inbound";
            InboundLabel.Location = new Point(82, 68);
            InboundLabel.AutoSize = true;
            Partitions.Controls.Add(InboundLabel);

            OutboundLabel.Text = "Outbound";
            OutboundLabel.Location = new Point(70, 96);
            OutboundLabel.AutoSize = true;
            Partitions.Controls.Add(OutboundLabel);

            TimeSpreadLabel.Text = "Time Spread";
            TimeSpreadLabel.Location = new Point(52, 124);
            TimeSpreadLabel.AutoSize = true;
            Partitions.Controls.Add(TimeSpreadLabel);

            ToDestTimeLabel.Text = "To Destination Time";
            ToDestTimeLabel.Location = new Point(6, 152);
            ToDestTimeLabel.AutoSize = true;
            Partitions.Controls.Add(ToDestTimeLabel);

            ToHomeTimeLabel.Text = "To Home Time";
            ToHomeTimeLabel.Location = new Point(40, 180);
            ToHomeTimeLabel.AutoSize = true;
            Partitions.Controls.Add(ToHomeTimeLabel);

            Shared.Text = "Shared";
            Shared.Location = new Point(12, 224);
            Shared.Size = new Size(229, 135);
            Controls.Add(Shared);

            SharedStepSize.DropDownStyle = ComboBoxStyle.DropDownList;
            SharedStepSize.Location = new Point(133, 21);
            SharedStepSize.Size = new Size(90, 24);
            SharedStepSize.Items.Add(10);
            SharedStepSize.Items.Add(100);
            Shared.Controls.Add(SharedStepSize);

            SharedVehicleSpace.Location = new Point(133, 51);
            SharedVehicleSpace.Size = new Size(90, 22);
            Shared.Controls.Add(SharedVehicleSpace);

            SharedIncommingRange.Location = new Point(133, 79);
            SharedIncommingRange.Size = new Size(90, 22);
            Shared.Controls.Add(SharedIncommingRange);

            SharedTrailingSpeed.Location = new Point(133, 107);
            SharedTrailingSpeed.Size = new Size(90, 22);
            Shared.Controls.Add(SharedTrailingSpeed);

            StepSizeLabel.Text = "Step Size";
            StepSizeLabel.Location = new Point(59, 24);
            StepSizeLabel.AutoSize = true;
            Shared.Controls.Add(StepSizeLabel);

            VehicleSpaceLabel.Text = "Vehicle Space";
            VehicleSpaceLabel.Location = new Point(29, 53);
            VehicleSpaceLabel.AutoSize = true;
            Shared.Controls.Add(VehicleSpaceLabel);

            IncommingRangeLabel.Text = "Incomming Range";
            IncommingRangeLabel.Location = new Point(6, 81);
            IncommingRangeLabel.AutoSize = true;
            Shared.Controls.Add(IncommingRangeLabel);

            TrailingSpeedLabel.Text = "Trailing Speed";
            TrailingSpeedLabel.Location = new Point(27, 109);
            TrailingSpeedLabel.AutoSize = true;
            Shared.Controls.Add(TrailingSpeedLabel);

            InfoButton.Text = "Info";
            InfoButton.Location = new Point(276, 278);
            InfoButton.Size = new Size(75, 23);
            InfoButton.Click += InfoButtonClick;
            Controls.Add(InfoButton);

            DefaultsButton.Text = "Defaults";
            DefaultsButton.Location = new Point(276, 307);
            DefaultsButton.Size = new Size(75, 23);
            DefaultsButton.Click += DefaultsButtonClick;
            Controls.Add(DefaultsButton);

            SaveButton.Text = "Save";
            SaveButton.Location = new Point(276, 336);
            SaveButton.Size = new Size(75, 23);
            SaveButton.Click += SaveButtonClick;
            Controls.Add(SaveButton);
        }
    }
}

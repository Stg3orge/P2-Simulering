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
            Size = new Size(306, 355);
            MinimumSize = new Size(306, 355);
            MaximumSize = new Size(306, 355);
            ShowIcon = false;
            MinimizeBox = false;
            MaximizeBox = false;
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterParent;

            Partitions.Text = "Partitions";
            Partitions.Location = new Point(12, 12);
            Partitions.Size = new Size(266, 188);
            Controls.Add(Partitions);
            
            PrimaryVehicleCount.Location = new Point(114, 32);
            PrimaryVehicleCount.Size = new Size(70, 20);
            PrimaryVehicleCount.Maximum = 10000;
            Partitions.Controls.Add(PrimaryVehicleCount);

            PrimaryInbound.Location = new Point(114, 58);
            PrimaryInbound.Size = new Size(70, 20);
            PrimaryInbound.Maximum = 10000;
            Partitions.Controls.Add(PrimaryInbound);

            PrimaryOutbound.Location = new Point(114, 84);
            PrimaryOutbound.Size = new Size(70, 20);
            PrimaryOutbound.Maximum = 10000;
            Partitions.Controls.Add(PrimaryOutbound);

            PrimaryTimeSpread.Location = new Point(114, 110);
            PrimaryTimeSpread.Size = new Size(70, 20);
            PrimaryTimeSpread.Maximum = Simulation.MsInDay;
            Partitions.Controls.Add(PrimaryTimeSpread);

            PrimaryToDestTime.Location = new Point(114, 136);
            PrimaryToDestTime.Size = new Size(70, 20);
            PrimaryToDestTime.Maximum = Simulation.MsInDay;
            Partitions.Controls.Add(PrimaryToDestTime);

            PrimaryToHomeTime.Location = new Point(114, 162);
            PrimaryToHomeTime.Size = new Size(70, 20);
            PrimaryToHomeTime.Maximum = Simulation.MsInDay;
            Partitions.Controls.Add(PrimaryToHomeTime);

            SecondaryVehicleCount.Location = new Point(190, 32);
            SecondaryVehicleCount.Size = new Size(70, 20);
            SecondaryVehicleCount.Maximum = 10000;
            Partitions.Controls.Add(SecondaryVehicleCount);

            SecondaryInbound.Location = new Point(190, 58);
            SecondaryInbound.Size = new Size(70, 20);
            SecondaryInbound.Maximum = 10000;
            Partitions.Controls.Add(SecondaryInbound);

            SecondaryOutbound.Location = new Point(190, 84);
            SecondaryOutbound.Size = new Size(70, 20);
            SecondaryOutbound.Maximum = 10000;
            Partitions.Controls.Add(SecondaryOutbound);

            SecondaryTimeSpread.Location = new Point(190, 110);
            SecondaryTimeSpread.Size = new Size(70, 20);
            SecondaryTimeSpread.Maximum = Simulation.MsInDay;
            Partitions.Controls.Add(SecondaryTimeSpread);

            SecondaryToDestTime.Location = new Point(190, 136);
            SecondaryToDestTime.Size = new Size(70, 20);
            SecondaryToDestTime.Maximum = Simulation.MsInDay;
            Partitions.Controls.Add(SecondaryToDestTime);

            SecondaryToHomeTime.Location = new Point(190, 162);
            SecondaryToHomeTime.Size = new Size(70, 20);
            SecondaryToHomeTime.Maximum = Simulation.MsInDay;
            Partitions.Controls.Add(SecondaryToHomeTime);

            Primary.Text = "Primary";
            Primary.Location = new Point(111, 16);
            Primary.AutoSize = true;
            Partitions.Controls.Add(Primary);

            Secondary.Text = "Secondary";
            Secondary.Location = new Point(187, 16);
            Secondary.AutoSize = true;
            Partitions.Controls.Add(Secondary);

            VehicleCountLabel.Text = "Vehicle Count";
            VehicleCountLabel.Location = new Point(35, 34);
            VehicleCountLabel.AutoSize = true;
            Partitions.Controls.Add(VehicleCountLabel);

            InboundLabel.Text = "Inbound";
            InboundLabel.Location = new Point(62, 60);
            InboundLabel.AutoSize = true;
            Partitions.Controls.Add(InboundLabel);

            OutboundLabel.Text = "Outbound";
            OutboundLabel.Location = new Point(54, 86);
            OutboundLabel.AutoSize = true;
            Partitions.Controls.Add(OutboundLabel);

            TimeSpreadLabel.Text = "Time Spread";
            TimeSpreadLabel.Location = new Point(41, 112);
            TimeSpreadLabel.AutoSize = true;
            Partitions.Controls.Add(TimeSpreadLabel);

            ToDestTimeLabel.Text = "To Destination Time";
            ToDestTimeLabel.Location = new Point(6, 138);
            ToDestTimeLabel.AutoSize = true;
            Partitions.Controls.Add(ToDestTimeLabel);

            ToHomeTimeLabel.Text = "To Home Time";
            ToHomeTimeLabel.Location = new Point(31, 164);
            ToHomeTimeLabel.AutoSize = true;
            Partitions.Controls.Add(ToHomeTimeLabel);

            Shared.Text = "Shared";
            Shared.Location = new Point(12, 206);
            Shared.Size = new Size(181, 98);
            Controls.Add(Shared);

            SharedStepSize.DropDownStyle = ComboBoxStyle.DropDownList;
            SharedStepSize.Location = new Point(105, 19);
            SharedStepSize.Size = new Size(70, 21);
            SharedStepSize.Items.Add(10);
            SharedStepSize.Items.Add(100);
            Shared.Controls.Add(SharedStepSize);

            SharedVehicleSpace.Location = new Point(105, 46);
            SharedVehicleSpace.Size = new Size(70, 20);
            Shared.Controls.Add(SharedVehicleSpace);

            SharedIncommingRange.Location = new Point(105, 72);
            SharedIncommingRange.Size = new Size(70, 20);
            Shared.Controls.Add(SharedIncommingRange);
            
            StepSizeLabel.Text = "Step Size";
            StepSizeLabel.Location = new Point(47, 22);
            StepSizeLabel.AutoSize = true;
            Shared.Controls.Add(StepSizeLabel);

            VehicleSpaceLabel.Text = "Vehicle Space";
            VehicleSpaceLabel.Location = new Point(23, 48);
            VehicleSpaceLabel.AutoSize = true;
            Shared.Controls.Add(VehicleSpaceLabel);

            IncommingRangeLabel.Text = "Incomming Range";
            IncommingRangeLabel.Location = new Point(6, 74);
            IncommingRangeLabel.AutoSize = true;
            Shared.Controls.Add(IncommingRangeLabel);

            InfoButton.Text = "Info";
            InfoButton.Location = new Point(203, 223);
            InfoButton.Size = new Size(75, 23);
            InfoButton.Click += InfoButtonClick;
            Controls.Add(InfoButton);

            DefaultsButton.Text = "Defaults";
            DefaultsButton.Location = new Point(203, 249);
            DefaultsButton.Size = new Size(75, 23);
            DefaultsButton.Click += DefaultsButtonClick;
            Controls.Add(DefaultsButton);

            SaveButton.Text = "Save";
            SaveButton.Location = new Point(203, 275);
            SaveButton.Size = new Size(75, 23);
            SaveButton.Click += SaveButtonClick;
            Controls.Add(SaveButton);
        }
    }
}

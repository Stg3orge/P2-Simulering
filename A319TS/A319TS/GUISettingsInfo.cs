﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace A319TS
{
    class GUISettingsInfo : Form
    {
        GroupBox VehicleCount = new GroupBox();
        GroupBox InboundOutbound = new GroupBox();
        GroupBox TimeSpread = new GroupBox();
        GroupBox ToDestinationTime = new GroupBox();
        GroupBox ToHomeTime = new GroupBox();
        GroupBox StepSize = new GroupBox();
        GroupBox VehicleSpace = new GroupBox();
        GroupBox IncommingRange = new GroupBox();
        RichTextBox VehicleCountInfo = new RichTextBox();
        RichTextBox InboundOutboundInfo = new RichTextBox();
        RichTextBox TimeSpreadInfo = new RichTextBox();
        RichTextBox ToDestinationTimeInfo = new RichTextBox();
        RichTextBox ToHomeTimeInfo = new RichTextBox();
        RichTextBox StepSizeInfo = new RichTextBox();
        RichTextBox VehicleSpaceInfo = new RichTextBox();
        RichTextBox IncommingRangeInfo = new RichTextBox();

        public GUISettingsInfo()
        {
            Text = "Settings Info";
            Size = new Size(330, 524);
            MinimumSize = new Size(330, 524);
            MaximumSize = new Size(330, 524);
            ShowIcon = false;
            MinimizeBox = false;
            MaximizeBox = false;
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterParent;

            VehicleCount.Text = "Vehicle Count";
            VehicleCount.Location = new Point(12, 12);
            VehicleCount.Size = new Size(290, 40);
            Controls.Add(VehicleCount);

            VehicleCountInfo.Dock = DockStyle.Fill;
            VehicleCountInfo.BackColor = SystemColors.Control;
            VehicleCountInfo.BorderStyle = BorderStyle.None;
            VehicleCountInfo.ReadOnly = true;
            VehicleCountInfo.Text = "The amount of vehicles to simulate.";
            VehicleCount.Controls.Add(VehicleCountInfo);

            InboundOutbound.Text = "Inbound and Outbound";
            InboundOutbound.Location = new Point(12, 58);
            InboundOutbound.Size = new Size(290, 65);
            Controls.Add(InboundOutbound);

            InboundOutboundInfo.Dock = DockStyle.Fill;
            InboundOutboundInfo.BackColor = SystemColors.Control;
            InboundOutboundInfo.BorderStyle = BorderStyle.None;
            InboundOutboundInfo.ReadOnly = true;
            InboundOutboundInfo.Text = "The amount of cars that enter and leave from nodes with either Inbound or Outbound as their selected type. Cannot be higher than vehicle count.";
            InboundOutbound.Controls.Add(InboundOutboundInfo);

            TimeSpread.Text = "Time Spread";
            TimeSpread.Location = new Point(12, 129);
            TimeSpread.Size = new Size(290, 65);
            Controls.Add(TimeSpread);

            TimeSpreadInfo.Dock = DockStyle.Fill;
            TimeSpreadInfo.BackColor = SystemColors.Control;
            TimeSpreadInfo.BorderStyle = BorderStyle.None;
            TimeSpreadInfo.ReadOnly = true;
            TimeSpreadInfo.Text = "The amount in milliseconds that vehicles travel times can deviate from 'To Destination Time' and 'To Home Time'. Cannot be allowed to exceed the bounds of 0 to 8640000.";
            TimeSpread.Controls.Add(TimeSpreadInfo);

            ToDestinationTime.Text = "To Destination Time";
            ToDestinationTime.Location = new Point(12, 197);
            ToDestinationTime.Size = new Size(290, 50);
            Controls.Add(ToDestinationTime);

            ToDestinationTimeInfo.Dock = DockStyle.Fill;
            ToDestinationTimeInfo.BackColor = SystemColors.Control;
            ToDestinationTimeInfo.BorderStyle = BorderStyle.None;
            ToDestinationTimeInfo.ReadOnly = true;
            ToDestinationTimeInfo.Text = "The time of day, in milliseconds, which vehicles start traveling towards their destination.";
            ToDestinationTime.Controls.Add(ToDestinationTimeInfo);

            ToHomeTime.Text = "To Home Time";
            ToHomeTime.Location = new Point(12, 250);
            ToHomeTime.Size = new Size(290, 50);
            Controls.Add(ToHomeTime);

            ToHomeTimeInfo.Dock = DockStyle.Fill;
            ToHomeTimeInfo.BackColor = SystemColors.Control;
            ToHomeTimeInfo.BorderStyle = BorderStyle.None;
            ToHomeTimeInfo.ReadOnly = true;
            ToHomeTimeInfo.Text = "The time of day, in milliseconds, which vehicles start traveling towards their home.";
            ToHomeTime.Controls.Add(ToHomeTimeInfo);

            StepSize.Text = "Step Size";
            StepSize.Location = new Point(12, 306);
            StepSize.Size = new Size(290, 65);
            Controls.Add(StepSize);

            StepSizeInfo.Dock = DockStyle.Fill;
            StepSizeInfo.BackColor = SystemColors.Control;
            StepSizeInfo.BorderStyle = BorderStyle.None;
            StepSizeInfo.ReadOnly = true;
            StepSizeInfo.Text = "The time in milliseconds that the simulation jumps with each cycle. A lower step size means vehicles react more frequently, but the simulation will take longer to process.";
            StepSize.Controls.Add(StepSizeInfo);

            VehicleSpace.Text = "Vehicle Space";
            VehicleSpace.Location = new Point(12, 377);
            VehicleSpace.Size = new Size(290, 40);
            Controls.Add(VehicleSpace);

            VehicleSpaceInfo.Dock = DockStyle.Fill;
            VehicleSpaceInfo.BackColor = SystemColors.Control;
            VehicleSpaceInfo.BorderStyle = BorderStyle.None;
            VehicleSpaceInfo.ReadOnly = true;
            VehicleSpaceInfo.Text = "The space in meters between vehicles.";
            VehicleSpace.Controls.Add(VehicleSpaceInfo);

            IncommingRange.Text = "Incomming Range";
            IncommingRange.Location = new Point(12, 423);
            IncommingRange.Size = new Size(290, 50);
            Controls.Add(IncommingRange);

            IncommingRangeInfo.Dock = DockStyle.Fill;
            IncommingRangeInfo.BackColor = SystemColors.Control;
            IncommingRangeInfo.BorderStyle = BorderStyle.None;
            IncommingRangeInfo.ReadOnly = true;
            IncommingRangeInfo.Text = "The distance in meters, in which vehicles look for incomming vehicles when waiting at a yield node.";
            IncommingRange.Controls.Add(IncommingRangeInfo);
        }
    }
}
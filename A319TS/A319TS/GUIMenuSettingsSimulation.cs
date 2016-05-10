using System;
using System.Drawing;
using System.Windows.Forms;

namespace A319TS
{
    class GUIMenuSettingsSimulation : Form
    {
        private Project Project;
        private GroupBox Vehicle;
        private GroupBox Time;
        private Label StepSizeLabel;
        private Label VehicleSpaceLabel;
        private Label RangeLabel;
        private Label VehicleCountLabel;
        private Label InBoundLabel;
        private Label OutBoundLabel;
        private Label TimeSpreadLabel;
        private Label ToDestTimeLabel;
        private Label ToHomeTimeLabel;
        private ComboBox StepSize;
        private NumericUpDown VehicleSpace;
        private NumericUpDown Range;
        private NumericUpDown VehicleCount;
        private NumericUpDown InBound;
        private NumericUpDown OutBound;
        private TextBox TimeSpread;
        private TextBox ToDestTime;
        private TextBox ToHomeTime;
        private Button SetPrimary;
        private Button SetSecondary;
        private Button SetBoth;
        private Button Reset;


        public GUIMenuSettingsSimulation(Project project)
        {
            Project = project;
            Setup();
        }

        private void Setup()
        {
            // Simulation Form
            Text = "Simulation";
            Size = new Size(566, 279);
            MinimumSize = new Size(566, 279);
            MaximumSize = new Size(566, 279);
            ShowIcon = false;
            MinimizeBox = false;
            MaximizeBox = false;
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterParent;

            // Labels
            StepSizeLabel = new Label();
            StepSizeLabel.Text = "StepSize";
            StepSizeLabel.Location = new Point(55, 37);
            StepSizeLabel.Size = new Size(64, 17);
            Controls.Add(StepSizeLabel);

            VehicleSpaceLabel = new Label();
            VehicleSpaceLabel.Text = "VehicleSpace";
            VehicleSpaceLabel.Location = new Point(25, 66);
            VehicleSpaceLabel.Size = new Size(94, 17);
            Controls.Add(VehicleSpaceLabel);

            RangeLabel = new Label();
            RangeLabel.Text = "IncommingRange";
            RangeLabel.Location = new Point(2, 97);
            RangeLabel.Size = new Size(117, 17);
            Controls.Add(RangeLabel);

            VehicleCountLabel = new Label();
            VehicleCountLabel.Text = "VehicleCount";
            VehicleCountLabel.Location = new Point(28, 122);
            VehicleCountLabel.Size = new Size(91, 17);
            Controls.Add(VehicleCountLabel);

            InBoundLabel = new Label();
            InBoundLabel.Text = "Inbound";
            InBoundLabel.Location = new Point(60, 150);
            InBoundLabel.Size = new Size(59, 17);
            Controls.Add(InBoundLabel);

            OutBoundLabel = new Label();
            OutBoundLabel.Text = "Outbound";
            OutBoundLabel.Location = new Point(48, 181);
            OutBoundLabel.Size = new Size(71, 17);
            Controls.Add(OutBoundLabel);

            TimeSpreadLabel = new Label();
            TimeSpreadLabel.Text = "TimeSpread";
            TimeSpreadLabel.Location = new Point(59, 41);
            TimeSpreadLabel.Size = new Size(85, 17);
            Controls.Add(TimeSpreadLabel);

            ToDestTimeLabel = new Label();
            ToDestTimeLabel.Text = "ToDestinationTime";
            ToDestTimeLabel.Location = new Point(17, 69);
            ToDestTimeLabel.Size = new Size(127, 17);
            Controls.Add(ToDestTimeLabel);

            ToHomeTimeLabel = new Label();
            ToHomeTimeLabel.Text = "ToHomeTime";
            ToHomeTimeLabel.Location = new Point(51, 97);
            ToHomeTimeLabel.Size = new Size(93, 17);
            Controls.Add(ToHomeTimeLabel);

            // buttons 
            SetPrimary = new Button();
            SetPrimary.Text = "SetPrimary";
            SetPrimary.Location = new Point(282, 150);
            SetPrimary.Size = new Size(119, 35);
            SetPrimary.Click += SetPrimaryClick;
            Controls.Add(SetPrimary);

            SetSecondary = new Button();
            SetSecondary.Text = "SetSecondary";
            SetSecondary.Location = new Point(417, 150);
            SetSecondary.Size = new Size(119, 35);
            SetSecondary.Click += SetSecondaryClick;
            Controls.Add(SetSecondary);

            SetBoth = new Button();
            SetBoth.Text = "SetBoth";
            SetBoth.Location = new Point(282, 192);
            SetBoth.Size = new Size(119, 35);
            SetBoth.Click += SetBothClick;
            Controls.Add(SetBoth);

            Reset = new Button();
            Reset.Text = "Reset";
            Reset.Location = new Point(417, 191);
            Reset.Size = new Size(119, 35);
            Reset.Click += ResetClick;
            Controls.Add(Reset);

            // NumericsUpDown
            VehicleSpace = new NumericUpDown();
            VehicleSpace.Text = "VehicleSpace";
            VehicleSpace.Location = new Point(125, 64);
            VehicleSpace.Size = new Size(120, 22);
            Controls.Add(VehicleSpace);

            Range = new NumericUpDown();
            Range.Text = "Range";
            Range.Location = new Point(125, 92);
            Range.Size = new Size(120, 22);
            Controls.Add(Range);

            VehicleCount = new NumericUpDown();
            VehicleCount.Text = "VehicleCount";
            VehicleCount.Location = new Point(125, 120);
            VehicleCount.Size = new Size(120, 22);
            Controls.Add(VehicleCount);

            InBound = new NumericUpDown();
            InBound.Text = "InBound";
            InBound.Location = new Point(125, 148);
            InBound.Size = new Size(120, 22);
            Controls.Add(InBound);

            OutBound = new NumericUpDown();
            OutBound.Text = "OutBound";
            OutBound.Location = new Point(125, 179);
            OutBound.Size = new Size(120, 22);
            Controls.Add(OutBound);

            // TextBoxs
            TimeSpread = new TextBox();
            TimeSpread.Location = new Point(150, 38);
            TimeSpread.Size = new Size(100, 22);
            Controls.Add(TimeSpread);

            ToDestTime = new TextBox();
            ToDestTime.Location = new Point(150, 66);
            ToDestTime.Size = new Size(100, 22);
            Controls.Add(ToDestTime);

            ToHomeTime = new TextBox();
            ToHomeTime.Location = new Point(150, 94);
            ToHomeTime.Size = new Size(100, 22);
            Controls.Add(ToHomeTime);

            // ComboBox
            StepSize = new ComboBox();
            StepSize.Location = new Point(124, 34);
            StepSize.Size = new Size(121, 24);
            Controls.Add(StepSize);

            //GroupBox
            Vehicle = new GroupBox();
            Vehicle.Text = "Vehicle";
            Vehicle.Location = new Point(12, 12);
            Vehicle.Size = new Size(254, 215);
            Controls.Add(Vehicle);
            Vehicle.Controls.Add(ToHomeTimeLabel);
            Vehicle.Controls.Add(OutBound);
            Vehicle.Controls.Add(InBound);
            Vehicle.Controls.Add(VehicleCount);
            Vehicle.Controls.Add(Range);
            Vehicle.Controls.Add(StepSizeLabel);
            Vehicle.Controls.Add(VehicleSpace);
            Vehicle.Controls.Add(VehicleSpaceLabel);
            Vehicle.Controls.Add(RangeLabel);
            Vehicle.Controls.Add(VehicleCountLabel);
            Vehicle.Controls.Add(InBoundLabel);
            Vehicle.Controls.Add(OutBoundLabel);
            

            Time = new GroupBox();
            Time.Text = "Time";
            Time.Location = new Point(273, 12);
            Time.Size = new Size(263, 132);
            Controls.Add(Time);
            Time.Controls.Add(TimeSpreadLabel);
            Time.Controls.Add(ToDestTimeLabel);
            Time.Controls.Add(TimeSpreadLabel);
            Time.Controls.Add(TimeSpread);
            Time.Controls.Add(ToDestTime);
            Time.Controls.Add(ToHomeTime);
            
        }
        //Methods
        private void SetPrimaryClick(object sender, EventArgs args)
        {

        }
        private void SetSecondaryClick(object sender, EventArgs args)
        {

        }
        private void SetBothClick(object sender, EventArgs args)
        {

        }
        private void ResetClick(object sender, EventArgs args)
        {

        }
    }
}

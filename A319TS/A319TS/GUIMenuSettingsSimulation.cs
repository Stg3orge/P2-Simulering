using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace A319TS
{
    class GUIMenuSettingsSimulation : Form
    {
        List<Label> labels = new List<Label>();
        private SimulationSettings simulationsettings;
        private GroupBox SimulationGroup;
        private GroupBox Shared;
        private Label StepSizeLabel;
        private Label VehicleSpaceLabel;
        private Label RangeLabel;
        private Label VehicleCountLabel;
        private Label InboundLabel;
        private Label OutboundLabel;
        private Label TimeSpreadLabel;
        private Label ToDestTimeLabel;
        private Label ToHomeTimeLabel;
        private Label Primary;
        private Label Secondary;
        private ComboBox StepSize;
        private NumericUpDown VehicleSpace;
        private NumericUpDown Range;
        private NumericUpDown PrimaryVehicleCount;
        private NumericUpDown PrimaryInBound;
        private NumericUpDown PrimaryOutBound;
        private NumericUpDown SecondaryVehicleCount;
        private NumericUpDown SecondaryInBound;
        private NumericUpDown SecondaryOutBound;
        private TextBox PrimaryTimeSpread;
        private TextBox PrimaryToDestTime;
        private TextBox PrimaryToHomeTime;
        private TextBox SecondaryTimeSpread;
        private TextBox SecondaryToDestTime;
        private TextBox SecondaryToHomeTime;
        private Button Save;
        private Button SetDefault;


        public GUIMenuSettingsSimulation(Project project)
        {
            simulationsettings = project.Settings;
            Load += ReadSettings;
            Setup();
            labels.AddRange(new Label[] { StepSizeLabel, VehicleSpaceLabel, RangeLabel, VehicleCountLabel,
                InboundLabel, OutboundLabel, TimeSpreadLabel, ToDestTimeLabel, ToHomeTimeLabel, Primary, Secondary });
        }

        private void ReadSettings(object sender, EventArgs args)
        {
            StepSize.SelectedItem = simulationsettings.StepSize;
            VehicleSpace.Value = simulationsettings.VehicleSpace;
            Range.Value = simulationsettings.IncommingRange;
            PrimaryVehicleCount.Value = simulationsettings.PrimaryCarCount;
            PrimaryInBound.Value = simulationsettings.PrimaryInbound;
            PrimaryOutBound.Value = simulationsettings.PrimaryOutbound;
            SecondaryVehicleCount.Value = simulationsettings.SecondaryCarCount;
            SecondaryInBound.Value = simulationsettings.SecondaryInbound;
            SecondaryOutBound.Value = simulationsettings.SecondaryOutbound;
            PrimaryTimeSpread.Text = Convert.ToString(simulationsettings.PrimaryTimeSpread);
            PrimaryToDestTime.Text = Convert.ToString(simulationsettings.PrimaryToDestTime);
            PrimaryToHomeTime.Text = Convert.ToString(simulationsettings.PrimaryToHomeTime);
            SecondaryTimeSpread.Text = Convert.ToString(simulationsettings.SecondaryTimeSpread);
            SecondaryToDestTime.Text = Convert.ToString(simulationsettings.SecondaryToDestTime);
            SecondaryToHomeTime.Text = Convert.ToString(simulationsettings.SecondaryToHomeTime);
        }

        private void Setup()
        {
            // Simulation Form
            Text = "Simulation";
            Size = new Size(720, 288);
            MinimumSize = new Size(720, 288);
            MaximumSize = new Size(720, 288);
            ShowIcon = false;
            MinimizeBox = false;
            MaximizeBox = false;
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterParent;

            // Labels
            Primary = new Label();
            Primary.Text = "Primary";
            Primary.Location = new Point(152, 18);
            Primary.Size = new Size(56, 17);
            Controls.Add(Primary);

            Secondary = new Label();
            Secondary.Text = "Secondary";
            Secondary.Location = new Point(284, 18);
            Secondary.Size = new Size(76, 17);
            Controls.Add(Secondary);

            StepSizeLabel = new Label();
            StepSizeLabel.Text = "StepSize";
            StepSizeLabel.Location = new Point(74, 24);
            StepSizeLabel.Size = new Size(64, 17);
            Controls.Add(StepSizeLabel);

            VehicleSpaceLabel = new Label();
            VehicleSpaceLabel.Text = "VehicleSpace";
            VehicleSpaceLabel.Location = new Point(44, 53);
            VehicleSpaceLabel.Size = new Size(94, 17);
            Controls.Add(VehicleSpaceLabel);

            RangeLabel = new Label();
            RangeLabel.Text = "IncommingRange";
            RangeLabel.Location = new Point(21, 84);
            RangeLabel.Size = new Size(117, 17);
            Controls.Add(RangeLabel);

            VehicleCountLabel = new Label();
            VehicleCountLabel.Text = "VehicleCount";
            VehicleCountLabel.Location = new Point(39, 49);
            VehicleCountLabel.Size = new Size(91, 17);
            Controls.Add(VehicleCountLabel);

            InboundLabel = new Label();
            InboundLabel.Text = "Inbound";
            InboundLabel.Location = new Point(71, 77);
            InboundLabel.Size = new Size(59, 17);
            Controls.Add(InboundLabel);

            OutboundLabel = new Label();
            OutboundLabel.Text = "Outbound";
            OutboundLabel.Location = new Point(59, 108);
            OutboundLabel.Size = new Size(71, 17);
            Controls.Add(OutboundLabel);

            TimeSpreadLabel = new Label();
            TimeSpreadLabel.Text = "TimeSpread";
            TimeSpreadLabel.Location = new Point(45, 140);
            TimeSpreadLabel.Size = new Size(85, 17);
            Controls.Add(TimeSpreadLabel);

            ToDestTimeLabel = new Label();
            ToDestTimeLabel.Text = "ToDestinationTime";
            ToDestTimeLabel.Location = new Point(3, 168);
            ToDestTimeLabel.Size = new Size(127, 17);
            Controls.Add(ToDestTimeLabel);

            ToHomeTimeLabel = new Label();
            ToHomeTimeLabel.Text = "ToHomeTime";
            ToHomeTimeLabel.Location = new Point(37, 196);
            ToHomeTimeLabel.Size = new Size(93, 17);
            Controls.Add(ToHomeTimeLabel);

            // buttons 
            SetDefault = new Button();
            SetDefault.Text = "SetDefault";
            SetDefault.Location = new Point(552, 199);
            SetDefault.Size = new Size(139, 35);
            SetDefault.Click += SetDefaultClick;
            Controls.Add(SetDefault);

            Save = new Button();
            Save.Text = "Save";
            Save.Location = new Point(407, 199);
            Save.Size = new Size(139, 35);
            Save.Click += SaveClick;
            Controls.Add(Save);

            // NumericsUpDown
            VehicleSpace = new NumericUpDown();
            VehicleSpace.Location = new Point(144, 51);
            VehicleSpace.Size = new Size(120, 22);
            Controls.Add(VehicleSpace);
            VehicleSpace.Maximum = 10;
            VehicleSpace.Minimum = 1;

            Range = new NumericUpDown();
            Range.Location = new Point(144, 79);
            Range.Size = new Size(120, 22);
            Range.Maximum = 50;
            Range.Minimum = 0;
            Controls.Add(Range);

            PrimaryVehicleCount = new NumericUpDown();
            PrimaryVehicleCount.Location = new Point(136, 47);
            PrimaryVehicleCount.Size = new Size(120, 22);
            Controls.Add(PrimaryVehicleCount);
            PrimaryVehicleCount.Maximum = 2000;
            PrimaryVehicleCount.Minimum = 0;

            PrimaryInBound = new NumericUpDown();
            PrimaryInBound.Location = new Point(136, 75);
            PrimaryInBound.Size = new Size(120, 22);
            Controls.Add(PrimaryInBound);
            PrimaryInBound.Maximum = 2000;
            PrimaryInBound.Minimum = 0;

            PrimaryOutBound = new NumericUpDown();
            PrimaryOutBound.Location = new Point(136, 106);
            PrimaryOutBound.Size = new Size(120, 22);
            Controls.Add(PrimaryOutBound);
            PrimaryOutBound.Maximum = 2000;
            PrimaryOutBound.Minimum = 0;

            SecondaryVehicleCount = new NumericUpDown();
            SecondaryVehicleCount.Location = new Point(262, 47);
            SecondaryVehicleCount.Size = new Size(120, 22);
            Controls.Add(SecondaryVehicleCount);
            SecondaryVehicleCount.Maximum = 2000;
            SecondaryVehicleCount.Minimum = 0;

            SecondaryInBound = new NumericUpDown();
            SecondaryInBound.Location = new Point(262, 75);
            SecondaryInBound.Size = new Size(120, 22);
            Controls.Add(SecondaryInBound);
            SecondaryInBound.Maximum = 2000;
            SecondaryInBound.Minimum = 0;

            SecondaryOutBound = new NumericUpDown();
            SecondaryOutBound.Location = new Point(262, 106);
            SecondaryOutBound.Size = new Size(120, 22);
            Controls.Add(SecondaryOutBound);
            SecondaryOutBound.Maximum = 2000;
            SecondaryOutBound.Minimum = 0;

            // TextBoxs
            PrimaryTimeSpread = new TextBox();
            PrimaryTimeSpread.Location = new Point(136, 137);
            PrimaryTimeSpread.Size = new Size(120, 22);
            Controls.Add(PrimaryTimeSpread);

            PrimaryToDestTime = new TextBox();
            PrimaryToDestTime.Location = new Point(136, 165);
            PrimaryToDestTime.Size = new Size(120, 22);
            Controls.Add(PrimaryToDestTime);

            PrimaryToHomeTime = new TextBox();
            PrimaryToHomeTime.Location = new Point(136, 193);
            PrimaryToHomeTime.Size = new Size(120, 22);
            Controls.Add(PrimaryToHomeTime);

            SecondaryTimeSpread = new TextBox();
            SecondaryTimeSpread.Location = new Point(262, 137);
            SecondaryTimeSpread.Size = new Size(120, 22);
            Controls.Add(SecondaryTimeSpread);

            SecondaryToDestTime = new TextBox();
            SecondaryToDestTime.Location = new Point(262, 165);
            SecondaryToDestTime.Size = new Size(120, 22);
            Controls.Add(SecondaryToDestTime);

            SecondaryToHomeTime = new TextBox();
            SecondaryToHomeTime.Location = new Point(262, 193);
            SecondaryToHomeTime.Size = new Size(120, 22);
            Controls.Add(SecondaryToHomeTime);

            // ComboBox
            StepSize = new ComboBox();
            StepSize.Location = new Point(143, 21);
            StepSize.Size = new Size(121, 24);
            StepSize.Items.Add(10);
            StepSize.Items.Add(100);
            StepSize.Items.Add(500);
            StepSize.DropDownStyle = ComboBoxStyle.DropDownList;
            Controls.Add(StepSize);

            //GroupBox
            SimulationGroup = new GroupBox();
            SimulationGroup.Text = "Simulation";
            SimulationGroup.Location = new Point(12, 12);
            SimulationGroup.Size = new Size(389, 223);
            Controls.Add(SimulationGroup);
            SimulationGroup.Controls.Add(Primary);
            SimulationGroup.Controls.Add(Secondary);
            SimulationGroup.Controls.Add(VehicleCountLabel);
            SimulationGroup.Controls.Add(InboundLabel);
            SimulationGroup.Controls.Add(OutboundLabel);
            SimulationGroup.Controls.Add(TimeSpreadLabel);
            SimulationGroup.Controls.Add(ToDestTimeLabel);
            SimulationGroup.Controls.Add(ToHomeTimeLabel);

            SimulationGroup.Controls.Add(PrimaryVehicleCount);
            SimulationGroup.Controls.Add(PrimaryInBound);
            SimulationGroup.Controls.Add(PrimaryOutBound);
            SimulationGroup.Controls.Add(SecondaryVehicleCount);
            SimulationGroup.Controls.Add(SecondaryInBound);
            SimulationGroup.Controls.Add(SecondaryOutBound);

            SimulationGroup.Controls.Add(PrimaryTimeSpread);
            SimulationGroup.Controls.Add(PrimaryToDestTime);
            SimulationGroup.Controls.Add(PrimaryToHomeTime);
            SimulationGroup.Controls.Add(SecondaryTimeSpread);
            SimulationGroup.Controls.Add(SecondaryToDestTime);
            SimulationGroup.Controls.Add(SecondaryToHomeTime);


            Shared = new GroupBox();
            Shared.Text = "Shared";
            Shared.Location = new Point(407, 12);
            Shared.Size = new Size(284, 123);
            Controls.Add(Shared);
            Shared.Controls.Add(StepSize);
            Shared.Controls.Add(StepSizeLabel);
            Shared.Controls.Add(VehicleSpaceLabel);
            Shared.Controls.Add(RangeLabel);
            Shared.Controls.Add(VehicleSpace);
            Shared.Controls.Add(Range);

        }
        //Methods
        private void SetDefaultClick(object sender, EventArgs args)
        {
            simulationsettings = new SimulationSettings();
            MessageBox.Show("Default Values are set.");
        }
        private void SaveClick(object sender, EventArgs args)
        {
            simulationsettings.StepSize = Convert.ToInt32(StepSize.SelectedItem);
            simulationsettings.VehicleSpace = Convert.ToInt32(VehicleSpace.Value);
            simulationsettings.IncommingRange = Convert.ToInt32(Range.Value);

            if (PrimaryTimeSpread.Text.Length > 0 && SecondaryTimeSpread.Text.Length > 0)
            {
                try
                {
                    if (Convert.ToInt32(SecondaryToDestTime.Text) - Convert.ToInt32(SecondaryTimeSpread.Text) > 0
                       && Convert.ToInt32(SecondaryToDestTime.Text) + Convert.ToInt32(SecondaryTimeSpread.Text) < Simulation.MsInDay
                       && Convert.ToInt32(SecondaryToHomeTime.Text) - Convert.ToInt32(SecondaryTimeSpread.Text) > 0
                       && Convert.ToInt32(SecondaryToHomeTime.Text) + Convert.ToInt32(SecondaryTimeSpread.Text) < Simulation.MsInDay)
                    {
                        simulationsettings.SecondaryTimeSpread = Convert.ToInt32(SecondaryTimeSpread.Text);
                        TimeSpreadLabel.ForeColor = Color.Black;
                    }
                    else
                    {
                        MessageBox.Show("SecondaryTimeSpread can not exceed 86400000 and can not be lower than 0");
                    }
                }
                catch (Exception e)
                {
                    TimeSpreadLabel.ForeColor = Color.Red;
                    MessageBox.Show(e.Message);
                }

                try
                {
                    if (Convert.ToInt32(PrimaryToDestTime.Text) - Convert.ToInt32(PrimaryTimeSpread.Text) > 0
                       && Convert.ToInt32(PrimaryToDestTime.Text) + Convert.ToInt32(PrimaryTimeSpread.Text) < Simulation.MsInDay
                       && Convert.ToInt32(PrimaryToHomeTime.Text) - Convert.ToInt32(PrimaryTimeSpread.Text) > 0
                       && Convert.ToInt32(PrimaryToHomeTime.Text) + Convert.ToInt32(PrimaryTimeSpread.Text) < Simulation.MsInDay)
                    {
                        simulationsettings.PrimaryTimeSpread = Convert.ToInt32(PrimaryTimeSpread.Text);
                        TimeSpreadLabel.ForeColor = Color.Black;
                    }
                    else
                    {
                        MessageBox.Show("PrimaryTimeSpread can not exceed 86400000 and can not be lower than 0");
                    }
                }
                catch (Exception e)
                {
                    TimeSpreadLabel.ForeColor = Color.Red;
                    MessageBox.Show(e.Message);
                }
            }
            else
            {
                TimeSpreadLabel.ForeColor = Color.Red;
            }

            if (PrimaryToHomeTime.Text.Length > 0 && SecondaryToHomeTime.Text.Length > 0)
            {
                try
                {
                    simulationsettings.PrimaryToHomeTime = Convert.ToInt32(PrimaryToHomeTime.Text);
                    simulationsettings.SecondaryToHomeTime = Convert.ToInt32(SecondaryToHomeTime.Text);
                    ToHomeTimeLabel.ForeColor = Color.Black;
                }
                catch (Exception e)
                {
                    ToHomeTimeLabel.ForeColor = Color.Red;
                    MessageBox.Show(e.Message);
                }

            }
            else
            {
                ToHomeTimeLabel.ForeColor = Color.Red;
            }

            if (PrimaryToDestTime.Text.Length > 0 && SecondaryToDestTime.Text.Length > 0)
            {
                try
                {
                    simulationsettings.PrimaryToDestTime = Convert.ToInt32(PrimaryToDestTime.Text);
                    simulationsettings.SecondaryToDestTime = Convert.ToInt32(SecondaryToDestTime.Text);
                    ToDestTimeLabel.ForeColor = Color.Black;
                }
                catch (Exception e)
                {

                    ToDestTimeLabel.ForeColor = Color.Red;
                    MessageBox.Show(e.Message);
                }
            }
            else
            {
                ToDestTimeLabel.ForeColor = Color.Red;
            }

            if (PrimaryVehicleCount.Value > 0 && SecondaryVehicleCount.Value > 0)
            {
                if ((SecondaryVehicleCount.Value > SecondaryOutBound.Value) && (PrimaryVehicleCount.Value > PrimaryOutBound.Value)
                    && (SecondaryVehicleCount.Value > SecondaryInBound.Value && PrimaryVehicleCount.Value > PrimaryInBound.Value))
                {
                    simulationsettings.PrimaryCarCount = Convert.ToInt32(PrimaryVehicleCount.Value);
                    simulationsettings.SecondaryCarCount = Convert.ToInt32(SecondaryVehicleCount.Value);
                    VehicleCountLabel.ForeColor = Color.Black;
                }
            }
            else
            {
                VehicleCountLabel.ForeColor = Color.Red;
            }

            if ((PrimaryOutBound.Value < PrimaryVehicleCount.Value) && (SecondaryOutBound.Value < SecondaryVehicleCount.Value))
            {
                simulationsettings.PrimaryOutbound = Convert.ToInt32(PrimaryOutBound.Value);
                simulationsettings.SecondaryOutbound = Convert.ToInt32(SecondaryOutBound.Value);
                OutboundLabel.ForeColor = Color.Black;
            }
            else
            {
                OutboundLabel.ForeColor = Color.Red;
            }

            if ((PrimaryInBound.Value < PrimaryVehicleCount.Value) && (SecondaryInBound.Value < SecondaryVehicleCount.Value))
            {
                simulationsettings.PrimaryInbound = Convert.ToInt32(PrimaryInBound.Value);
                simulationsettings.SecondaryInbound = Convert.ToInt32(SecondaryInBound.Value);
                InboundLabel.ForeColor = Color.Black;
            }
            else
            {
                InboundLabel.ForeColor = Color.Red;
            }

            bool succes = true;
            foreach (Label label in labels)
            {
                if (label.ForeColor == Color.Red)
                {
                    succes = false;
                }
            }
            if (succes)
            {
                MessageBox.Show("Settings are saved!");
            }
        }
    }
}

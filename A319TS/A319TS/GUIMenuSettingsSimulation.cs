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
        GroupBox Partitions = new GroupBox();
        GroupBox Shared = new GroupBox();
        Label Primary = new Label();
        Label Secondary = new Label();
        Label VehicleCountLabel = new Label();
        Label InboundLabel = new Label();
        Label OutboundLabel = new Label();
        Label TimeSpreadLabel = new Label();
        Label ToDestTimeLabel = new Label();
        Label ToHomeTimeLabel = new Label();
        Label StepSizeLabel = new Label();
        Label VehicleSpaceLabel = new Label();
        Label IncommingRangeLabel = new Label();
        Label TrailingSpeedLabel = new Label();
        NumericUpDown PrimaryVehicleCount = new NumericUpDown();
        NumericUpDown PrimaryInbound = new NumericUpDown();
        NumericUpDown PrimaryOutbound = new NumericUpDown();
        NumericUpDown PrimaryTimeSpread = new NumericUpDown();
        NumericUpDown PrimaryToDestTime = new NumericUpDown();
        NumericUpDown PrimaryToHomeTime = new NumericUpDown();
        NumericUpDown SecondaryVehicleCount = new NumericUpDown();
        NumericUpDown SecondaryInbound = new NumericUpDown();
        NumericUpDown SecondaryOutbound = new NumericUpDown();
        NumericUpDown SecondaryTimeSpread = new NumericUpDown();
        NumericUpDown SecondaryToDestTime = new NumericUpDown();
        NumericUpDown SecondaryToHomeTime = new NumericUpDown();
        ComboBox SharedStepSize = new ComboBox();
        NumericUpDown SharedVehicleSpace = new NumericUpDown();
        NumericUpDown SharedIncommingRange = new NumericUpDown();
        NumericUpDown SharedTrailingSpeed = new NumericUpDown();
        Button InfoButton = new Button();
        Button DefaultsButton = new Button();
        Button SaveButton = new Button();
        Project Project;

        public GUIMenuSettingsSimulation(Project project)
        {
            Project = project;
            Setup();
            SetValuesToProject();
        }

        private void InfoButtonClick(object sender, EventArgs args)
        {
            GUIInfoSettings info = new GUIInfoSettings();
            info.ShowDialog();
        }
        private void DefaultsButtonClick(object sender, EventArgs args)
        {
            Project.Settings = new SimulationSettings();
            SetValuesToProject();
        }
        private void SaveButtonClick(object sender, EventArgs args)
        {
            if (CheckTimeSpread())
            {
                SetProjectToValues();
                MessageBox.Show("Settings saved");
            }
        }
        private bool CheckTimeSpread()
        {
            bool primaryDestLowerBounds = PrimaryToDestTime.Value - PrimaryTimeSpread.Value > 0;
            bool primaryDestUpperBounds = PrimaryToDestTime.Value + PrimaryTimeSpread.Value < Simulation.MsInDay;
            bool primaryHomeLowerBounds = PrimaryToHomeTime.Value - PrimaryTimeSpread.Value > 0;
            bool primaryHomeUpperBounds = PrimaryToHomeTime.Value + PrimaryTimeSpread.Value < Simulation.MsInDay;
            bool secondaryDestLowerBounds = SecondaryToDestTime.Value - SecondaryTimeSpread.Value > 0;
            bool secondaryDestUpperBounds = SecondaryToDestTime.Value + SecondaryTimeSpread.Value < Simulation.MsInDay;
            bool secondaryHomeLowerBounds = SecondaryToHomeTime.Value - SecondaryTimeSpread.Value > 0;
            bool secondaryHomeUpperBounds = SecondaryToHomeTime.Value + SecondaryTimeSpread.Value < Simulation.MsInDay;

            if (!primaryDestLowerBounds) { TimeSpreadErrorMessage("Primary", "lower", "Destination"); return false; }
            else if (!primaryDestUpperBounds) { TimeSpreadErrorMessage("Primary", "upper", "Destination"); return false; }
            else if (!primaryHomeLowerBounds) { TimeSpreadErrorMessage("Primary", "lower", "Home"); return false; }
            else if (!primaryHomeUpperBounds) { TimeSpreadErrorMessage("Primary", "upper", "Home"); return false; }
            else if (!secondaryDestLowerBounds) { TimeSpreadErrorMessage("Secondary", "lower", "Destination"); return false; }
            else if (!secondaryDestUpperBounds) { TimeSpreadErrorMessage("Secondary", "upper", "Destination"); return false; }
            else if (!secondaryHomeLowerBounds) { TimeSpreadErrorMessage("Secondary", "lower", "Home"); return false; }
            else if (!secondaryHomeUpperBounds) { TimeSpreadErrorMessage("Secondary", "upper", "Home"); return false; }
            else
            {
                TimeSpreadLabel.ForeColor = Color.Black;
                return true;
            }
        }
        private void TimeSpreadErrorMessage(string partition, string bounds, string time)
        {
            MessageBox.Show(partition + " Time Spread exceeds " + bounds + " bounds for To " + time + " Time");
            TimeSpreadLabel.ForeColor = Color.Red;
        }
        private void SetValuesToProject()
        {
            PrimaryVehicleCount.Value = Project.Settings.PrimaryCarCount;
            PrimaryInbound.Value = Project.Settings.PrimaryInbound;
            PrimaryOutbound.Value = Project.Settings.PrimaryOutbound;
            PrimaryTimeSpread.Value = Project.Settings.PrimaryTimeSpread;
            PrimaryToDestTime.Value = Project.Settings.PrimaryToDestTime;
            PrimaryToHomeTime.Value = Project.Settings.PrimaryToHomeTime;
            SecondaryVehicleCount.Value = Project.Settings.SecondaryCarCount;
            SecondaryInbound.Value = Project.Settings.SecondaryInbound;
            SecondaryOutbound.Value = Project.Settings.SecondaryOutbound;
            SecondaryTimeSpread.Value = Project.Settings.SecondaryTimeSpread;
            SecondaryToDestTime.Value = Project.Settings.SecondaryToDestTime;
            SecondaryToHomeTime.Value = Project.Settings.SecondaryToHomeTime;
            SharedStepSize.SelectedItem = Project.Settings.StepSize;
            SharedVehicleSpace.Value = Project.Settings.VehicleSpace;
            SharedIncommingRange.Value = Project.Settings.IncommingRange;
            SharedTrailingSpeed.Value = Project.Settings.TrailingSpeed;
        }
        private void SetProjectToValues()
        {
            Project.Settings.PrimaryCarCount = decimal.ToInt32(PrimaryVehicleCount.Value);
            Project.Settings.PrimaryInbound = decimal.ToInt32(PrimaryInbound.Value);
            Project.Settings.PrimaryOutbound = decimal.ToInt32(PrimaryOutbound.Value);
            Project.Settings.PrimaryTimeSpread = decimal.ToInt32(PrimaryTimeSpread.Value);
            Project.Settings.PrimaryToDestTime = decimal.ToInt32(PrimaryToDestTime.Value);
            Project.Settings.PrimaryToHomeTime = decimal.ToInt32(PrimaryToHomeTime.Value);
            Project.Settings.SecondaryCarCount = decimal.ToInt32(SecondaryVehicleCount.Value);
            Project.Settings.SecondaryInbound = decimal.ToInt32(SecondaryInbound.Value);
            Project.Settings.SecondaryOutbound = decimal.ToInt32(SecondaryOutbound.Value);
            Project.Settings.SecondaryTimeSpread = decimal.ToInt32(SecondaryTimeSpread.Value);
            Project.Settings.SecondaryToDestTime = decimal.ToInt32(SecondaryToDestTime.Value);
            Project.Settings.SecondaryToHomeTime = decimal.ToInt32(SecondaryToHomeTime.Value);
            Project.Settings.StepSize = Convert.ToInt32(SharedStepSize.SelectedItem);
            Project.Settings.VehicleSpace = decimal.ToInt32(SharedVehicleSpace.Value);
            Project.Settings.IncommingRange = decimal.ToInt32(SharedIncommingRange.Value);
            Project.Settings.TrailingSpeed = decimal.ToInt32(SharedTrailingSpeed.Value);
        }
    }
}
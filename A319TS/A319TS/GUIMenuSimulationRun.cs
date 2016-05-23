using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace A319TS
{
    class GUIMenuSimulationRun : Form
    {
        private Project Project;
        private Simulation Simulation;
        private RichTextBox Information = new RichTextBox();
        private ProgressBar ProgressBar = new ProgressBar();
        private Label ProcessLabel = new Label();
        private Button Start = new Button();
        private Button Cancel = new Button();
        private int _primaryProgress = 0;
        private int _secondaryProgress = 0;

        public GUIMenuSimulationRun(Project project)
        {
            Project = project;
            Setup();
            Simulation = new Simulation(project);
            Simulation.PrimaryWorker.ProgressChanged += PrimaryProgressChanged;
            Simulation.SecondaryWorker.ProgressChanged += SecondaryProgressChanged;
            Simulation.SimulationDone += OnSimulationDone;
            FormClosing += OnFormClosing;
        }

        private void StartClick(object sender, EventArgs args)
        {
            Start.Enabled = false;
            Cancel.Enabled = true;
            try
            {
                InformationWriteLine("Simulating...");
                Simulation.Run();
            }
            catch (Exception e)
            {
                InformationWriteLine("ERROR: " + e.Message);
                ProcessLabel.Text = "Failure";
                Simulation = null;
                Cancel.Enabled = false;
            }
        }
        private void CancelClick(object sender, EventArgs args)
        {
            Simulation.Cancel();
            Cancel.Enabled = false;
        }
        private void InformationWriteLine(string text)
        {
            Information.AppendText(text + "\r");
        }
        private void PrimaryProgressChanged(object sender, ProgressChangedEventArgs args)
        {
            _primaryProgress = args.ProgressPercentage;
            ProgressBar.Value = (_primaryProgress + _secondaryProgress) / 2;
            InformationWriteLine(args.UserState as string);
        }
        private void SecondaryProgressChanged(object sender, ProgressChangedEventArgs args)
        {
            _secondaryProgress = args.ProgressPercentage;
            ProgressBar.Value = (_primaryProgress + _secondaryProgress) / 2;
            InformationWriteLine(args.UserState as string);
        }
        private void OnSimulationDone(object sender, EventArgs args)
        {
            ProgressBar.Value = Simulation.MsInDay;
            ProcessLabel.Text = "Success";
            InformationWriteLine("Simulation saved as: " + Simulation.Filename);
            Simulation = null;
            Cancel.Enabled = false;
        }
        private void OnFormClosing(object sender, EventArgs args)
        {
            if (Cancel.Enabled)
            {
                Simulation.Cancel();
                Simulation = null;
                Cancel.Enabled = false;
            }
        }

        private void Setup()
        {
            Text = "Run";
            Size = new Size(600, 400);
            MinimumSize = new Size(600, 400);
            MaximumSize = new Size(600, 400);
            ShowIcon = false;
            MinimizeBox = false;
            MaximizeBox = false;
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterParent;

            Information.Location = new Point(12, 12);
            Information.Size = new Size(558, 273);
            Information.Font = new Font("Consolas", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0); // ((byte)(0)));
            Information.BackColor = SystemColors.WindowText;
            Information.ForeColor = SystemColors.ControlDark;
            Information.ReadOnly = true;
            Controls.Add(Information);

            ProgressBar.Location = new Point(12, 291);
            ProgressBar.Size = new Size(558, 23);
            ProgressBar.Maximum = 86400000;
            Controls.Add(ProgressBar);

            ProcessLabel.Location = new Point(12, 317);
            ProcessLabel.Size = new Size(203, 13);
            Controls.Add(ProcessLabel);

            Start.Text = "Start";
            Start.Location = new Point(414, 320);
            Start.Size = new Size(75, 23);
            Start.Click += StartClick;
            Controls.Add(Start);

            Cancel.Text = "Cancel";
            Cancel.Location = new Point(495, 320);
            Cancel.Size = new Size(75, 23);
            Cancel.Enabled = false;
            Cancel.Click += CancelClick;
            Controls.Add(Cancel);
        }
    }
}

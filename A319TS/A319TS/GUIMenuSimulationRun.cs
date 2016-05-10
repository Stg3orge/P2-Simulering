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

        public GUIMenuSimulationRun(Project project)
        {
            Project = project;
            Setup();
            Simulation = new Simulation(project);
            Simulation.ProgressChanged += WorkerProgressChanged;
        }

        private void Setup()
        {
            Text = "Run";
            Size = new Size(500, 300);
            MinimumSize = new Size(500, 300);
            MaximumSize = new Size(500, 300);
            ShowIcon = false;
            MinimizeBox = false;
            MaximizeBox = false;
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterParent;
            
            Information.Location = new Point(12, 12);
            Information.Size = new Size(460, 179);
            Information.Font = new Font("Consolas", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0); // ((byte)(0)));
            Information.BackColor = SystemColors.WindowText;
            Information.ForeColor = SystemColors.ControlDark;
            Information.ReadOnly = true;
            Controls.Add(Information);
            
            ProgressBar.Location = new Point(12, 197);
            ProgressBar.Size = new Size(460, 23);
            Controls.Add(ProgressBar);
            
            ProcessLabel.Location = new Point(12, 223);
            ProcessLabel.Size = new Size(203, 13);
            Controls.Add(ProcessLabel);

            Start.Text = "Start";
            Start.Location = new Point(316, 226);
            Start.Size = new Size(75, 23);
            Start.Click += StartClick;
            Controls.Add(Start);

            Cancel.Text = "Cancel";
            Cancel.Location = new Point(397, 226);
            Cancel.Size = new Size(75, 23);
            Cancel.Enabled = false;
            Cancel.Click += CancelClick;
            Controls.Add(Cancel);
        }

        private void StartClick(object sender, EventArgs args)
        {
            Start.Enabled = false;
            Cancel.Enabled = true;
            try
            {
                Information.AppendText("Simulating...");
                Simulation.Start();
            }
            catch (OperationCanceledException e)
            {
                Information.AppendText("\r" + e.Message);
                ProcessLabel.Text = "Cancelled";
            }
            catch (Exception e)
            {
                Information.AppendText("\r" + "ERROR: " + e.Message);
                ProcessLabel.Text = "Failure";
            }
            finally
            {
                Simulation = null;
                Cancel.Enabled = false;
            }
        }
        private void CancelClick(object sender, EventArgs args)
        {
            Simulation.Cancel();
            Cancel.Enabled = false;
        }
        private void WorkerProgressChanged(object sender, ProgressChangedEventArgs args)
        {
            ProgressBar.Value = args.ProgressPercentage;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace A319TS
{
    class GUIMenuSimulationRun : Form
    {
        private Project Project;
        private RichTextBox Information = new RichTextBox();
        private ProgressBar ProgressBar = new ProgressBar();
        private Label ProcessLabel = new Label();

        public GUIMenuSimulationRun(Project project)
        {
            Project = project;
            Setup();
            Simulation sim = new Simulation(project);
            sim.ProgressChanged += OnProgressChanged;
            sim.ProcessStart += OnProcessStart;
            sim.OperationCompleted += OnOperationCompleted;
            sim.Run();
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

            Information = new RichTextBox();
            Information.Location = new Point(12, 12);
            Information.Size = new Size(460, 195);
            Information.BackColor = SystemColors.WindowText;
            Information.ForeColor = SystemColors.Control;
            Information.ReadOnly = true;
            Controls.Add(Information);

            ProgressBar = new ProgressBar();
            ProgressBar.Location = new Point(12, 213);
            ProgressBar.Size = new Size(460, 23);
            Controls.Add(ProgressBar);

            ProcessLabel = new Label();
            ProcessLabel.Location = new Point(45, 13);
            Controls.Add(ProcessLabel);
        }

        private void OnProgressChanged(object sender, ProgressEventArgs args)
        {
            ProgressBar.Value = args.Progress;
        }
        private void OnProcessStart(object sender, MessageEventArgs args)
        {
            ProcessLabel.Text = args.Message;
        }
        public void OnOperationCompleted(object sender, MessageEventArgs args)
        {
            Information.AppendText("\r" + args.Message);
        }
    }
}

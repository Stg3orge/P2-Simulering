using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace A319TS
{
    class GUIMenuSimulationView : Form
    {
        private Color CustomGreen = ColorTranslator.FromHtml("#96B587");
        private Color CustomRed = ColorTranslator.FromHtml("#A57070");
        private bool Started = false;

        private Panel SimContainer = new Panel();
        private SimulationViewport SimViewport;
        private ProgressBar ProgressBar = new ProgressBar();
        private Button Open = new Button();
        private TextBox OpenLabel = new TextBox();
        private Button Primary = new Button();
        private Button Secondary = new Button();
        private Button StartStop = new Button();
        private ComboBox UpdateRate = new ComboBox();
        private TextBox TimeLabel = new TextBox();
        private NumericUpDown SetTimeBox = new NumericUpDown();
        private Button SetTime = new Button();
        private Timer TimeTimer = new Timer();

        public GUIMenuSimulationView()
        {
            Setup();
            Load += OnLoad;
        }

        private void OpenClick(object sender, EventArgs args)
        {
            SimulationData data = FileHandler.OpenSimulation();
            if (data != null)
            {
                if (SimContainer.Controls.Contains(SimViewport))
                    SimContainer.Controls.Remove(SimViewport);
                SimViewport = new SimulationViewport(data);
                SimViewport.Dock = DockStyle.Fill;
                SimContainer.Controls.Add(SimViewport);
                ResetButtons();
                ProgressBar.Value = 0;
                OpenLabel.Text = data.Filename;
            }
        }
        private void ResetButtons()
        {
            Started = false;
            Primary.Enabled = false;
            Secondary.Enabled = true;
            StartStop.Enabled = true;
            UpdateRate.Enabled = true;
            SetTimeBox.Enabled = true;
            SetTime.Enabled = true;
        }
        private void PrimaryClick(object sender, EventArgs args)
        {
            Primary.Enabled = false;
            Secondary.Enabled = true;
            SimViewport.CurrentPartition = Partitions.Primary;
        }
        private void SecondaryClick(object sender, EventArgs args)
        {
            Secondary.Enabled = false;
            Primary.Enabled = true;
            SimViewport.CurrentPartition = Partitions.Secondary;
        }
        private void StartStopClick(object sender, EventArgs args)
        {
            Started = !Started;
            if (Started)
            {
                StartStop.BackColor = CustomGreen;
                TimeTimer.Enabled = true;
            }
            else
            {
                StartStop.BackColor = CustomRed;
                TimeTimer.Enabled = false;
            }

        }
        private void UpdateRateChanged(object sender, EventArgs args)
        {
            TimeTimer.Interval = Simulation.RecordInterval / GetUpdateRate();
            if (SimViewport != null)
                ProgressBar.Value = SimViewport.Time;
            
        }
        private void SetTimeClick(object sender, EventArgs args)
        {
            SimViewport.Time = decimal.ToInt32(SetTimeBox.Value);
            SetTimeBox.Text = SimViewport.Time.ToString();
            ProgressBar.Value = SimViewport.Time;
        }
        private void Tick(object sender, EventArgs args)
        {
            SimViewport.Time += Simulation.RecordInterval;
            SimViewport.Nodes.Refresh();
            ProgressBar.Value = SimViewport.Time;
            TimeLabel.Text = SimViewport.Time.ToString();
        }

        private void Setup()
        {
            ShowIcon = false;
            Size = new Size(900, 400);
            MinimumSize = new Size(900, 200);
            Text = "View Simulation";

            SimContainer.Location = new Point(12, 12);
            SimContainer.Size = new Size(858, 286);
            SimContainer.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            SimContainer.BorderStyle = BorderStyle.Fixed3D;
            SimContainer.BackColor = Color.White;
            Controls.Add(SimContainer);

            ProgressBar.Location = new Point(12, 304);
            ProgressBar.Size = new Size(858, 10);
            ProgressBar.Minimum = 0;
            ProgressBar.Maximum = 86400000;
            ProgressBar.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            Controls.Add(ProgressBar);

            Open.Location = new Point(12, 320);
            Open.Size = new Size(75, 23);
            Open.Text = "Open";
            Open.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
            Open.Click += OpenClick;
            Controls.Add(Open);

            OpenLabel.Location = new Point(93, 321);
            OpenLabel.Size = new Size(140, 22);
            OpenLabel.Enabled = false;
            OpenLabel.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            Controls.Add(OpenLabel);

            Primary.Location = new Point(239, 320);
            Primary.Size = new Size(90, 23);
            Primary.Text = "Primary";
            Primary.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            Primary.Enabled = false;
            Primary.Click += PrimaryClick;
            Controls.Add(Primary);

            Secondary.Location = new Point(335, 320);
            Secondary.Size = new Size(90, 23);
            Secondary.Text = "Secondary";
            Secondary.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            Secondary.Enabled = false;
            Secondary.Click += SecondaryClick;
            Controls.Add(Secondary);

            StartStop.Location = new Point(431, 320);
            StartStop.Size = new Size(90, 23);
            StartStop.Text = "Start / Stop";
            StartStop.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            StartStop.Enabled = false;
            StartStop.BackColor = CustomRed;
            StartStop.Click += StartStopClick;
            Controls.Add(StartStop);

            UpdateRate.Location = new Point(527, 319);
            UpdateRate.Size = new Size(70, 24);
            UpdateRate.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            UpdateRate.Enabled = false;
            UpdateRate.DropDownStyle = ComboBoxStyle.DropDownList;
            UpdateRate.Items.Add("1x");
            UpdateRate.Items.Add("2x");
            UpdateRate.Items.Add("4x");
            UpdateRate.Items.Add("8x");
            UpdateRate.Items.Add("16x");
            UpdateRate.Items.Add("32x");
            UpdateRate.SelectedValueChanged += UpdateRateChanged;
            Controls.Add(UpdateRate);

            TimeLabel.Location = new Point(603, 321);
            TimeLabel.Size = new Size(90, 22);
            TimeLabel.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            TimeLabel.Enabled = false;
            TimeLabel.Text = "0";
            Controls.Add(TimeLabel);

            SetTimeBox.Location = new Point(699, 321);
            SetTimeBox.Size = new Size(90, 22);
            SetTimeBox.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            SetTimeBox.Enabled = false;
            SetTimeBox.Maximum = 86400000;
            Controls.Add(SetTimeBox);

            SetTime.Location = new Point(795, 320);
            SetTime.Size = new Size(75, 23);
            SetTime.Text = "Set Time";
            SetTime.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            SetTime.Enabled = false;
            SetTime.Click += SetTimeClick;
            Controls.Add(SetTime);

            TimeTimer.Enabled = false;
            TimeTimer.Interval = Simulation.RecordInterval;
            TimeTimer.Tick += Tick;
        }
        private void OnLoad(object sender, EventArgs args)
        {
            UpdateRate.SelectedIndex = 0;
        }
        private int GetUpdateRate()
        {
            return Convert.ToInt32(((string)UpdateRate.SelectedItem).Replace("x", string.Empty));
        }
    }
}

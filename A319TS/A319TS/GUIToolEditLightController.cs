using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace A319TS
{
    class GUIToolEditLightController : Form
    {
        public LightController Controller;
        private Label FirstTimeLabel;
        private Label SecondTimeLabel;
        private NumericUpDown FirstTime;
        private NumericUpDown SecondTime;
        private Label LinksLabel;
        private DataGridView Links;
        private Button Remove;

        public GUIToolEditLightController(LightController controller)
        {
            Controller = controller;
            Setup();
            Load += ReadData;
            FormClosing += SaveData;
        }

        private void SetSize(int width, int height)
        {
            Size = new Size(width, height);
            MinimumSize = new Size(width, height);
            MaximumSize = new Size(width, height);
        }
        private void Setup()
        {
            Text = "Edit Light Controller";
            ShowIcon = false;
            MinimizeBox = false;
            MaximizeBox = false;
            SizeGripStyle = SizeGripStyle.Hide;
            SetSize(262, 119);

            FirstTimeLabel = new Label();
            FirstTimeLabel.Text = "First Time";
            FirstTimeLabel.Location = new Point(12, 14);
            FirstTimeLabel.AutoSize = true;
            Controls.Add(FirstTimeLabel);

            FirstTime = new NumericUpDown();
            FirstTime.Location = new Point(109, 12);
            FirstTime.Size = new Size(120, 22);
            FirstTime.Maximum = Simulation.MsInDay;
            FirstTime.Value = Controller.FirstTime;
            Controls.Add(FirstTime);

            SecondTimeLabel = new Label();
            SecondTimeLabel.Text = "Second Time";
            SecondTimeLabel.Location = new Point(12, 42);
            SecondTimeLabel.AutoSize = true;
            Controls.Add(SecondTimeLabel);

            SecondTime = new NumericUpDown();
            SecondTime.Location = new Point(109, 40);
            SecondTime.Size = new Size(120, 22);
            SecondTime.Maximum = Simulation.MsInDay;
            SecondTime.Value = Controller.SecondTime;
            Controls.Add(SecondTime);

            if (Controller.Lights.Count > 0)
            {
                SetSize(262, 400);

                LinksLabel = new Label();
                LinksLabel.Text = "Links";
                LinksLabel.Location = new Point(12, 71);
                LinksLabel.AutoSize = true;
                Controls.Add(LinksLabel);

                Links = new DataGridView();
                Links.Location = new Point(15, 92);
                Links.Size = new Size(214, 222);
                Links.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                Links.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                Links.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                Links.AllowUserToResizeColumns = false;
                Links.AllowUserToResizeRows = false;
                Links.RowHeadersVisible = false;
                Controls.Add(Links);

                Remove = new Button();
                Remove.Location = new Point(153, 320);
                Remove.Size = new Size(75, 23);
                Remove.Text = "Remove";
                Remove.Click += RemoveClick;
                Controls.Add(Remove);
            }
        }
        private void ReadData(object sender, EventArgs args)
        {
            if (Controller.Lights.Count > 0)
            {
                Links.DataSource = new BindingSource(new BindingList<Node>(Controller.Lights), null);
                foreach (DataGridViewColumn column in Links.Columns)
                    column.Visible = false;
                Links.Columns[1].Visible = true;
                Links.Columns[2].Visible = true;
                Links.Columns[1].ReadOnly = true;
            }
        }
        private void SaveData(object sender, EventArgs args)
        {
            Controller.FirstTime = Convert.ToInt32(FirstTime.Value);
            Controller.SecondTime = Convert.ToInt32(SecondTime.Value);
        }
        private void RemoveClick(object sender, EventArgs args)
        {
            foreach (DataGridViewRow row in Links.SelectedRows)
                Links.Rows.Remove(row);
        }
    }
}
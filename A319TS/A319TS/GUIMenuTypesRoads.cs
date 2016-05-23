using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

namespace A319TS
{
    class GUIMenuTypesRoads : Form
    {
        private Project Project;
        private Label NameLabel;
        private Label SpeedLabel;
        private TextBox SetName;
        private NumericUpDown SetSpeed;
        private Button Add;
        private Button Remove;
        private DataGridView Roads;
        
        public GUIMenuTypesRoads(Project project)
        {
            Project = project;
            Setup();
            Load += ReadData;
        }
        
        private void Setup()
        {
            Text = "Road Types";
            Size = new Size(243, 400);
            MinimumSize = new Size(243, 400);
            MaximumSize = new Size(243, 400);
            ShowIcon = false;
            MinimizeBox = false;
            MaximizeBox = false;
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterParent;
            
            SetName = new TextBox();
            SetName.Location = new Point(67, 12);
            SetName.Size = new Size(80, 22);
            Controls.Add(SetName);

            SetSpeed = new NumericUpDown();
            SetSpeed.Location = new Point(67, 40);
            SetSpeed.Size = new Size(80, 22);
            SetSpeed.Maximum = 10000;
            SetSpeed.Minimum = 0;
            Controls.Add(SetSpeed);

            NameLabel = new Label();
            NameLabel.Text = "Name";
            NameLabel.Location = new Point(16, 15);
            NameLabel.AutoSize = true;
            Controls.Add(NameLabel);

            SpeedLabel = new Label();
            SpeedLabel.Text = "Speed";
            SpeedLabel.Location = new Point(12, 42);
            SpeedLabel.AutoSize = true;
            Controls.Add(SpeedLabel);
            
            Add = new Button();
            Add.Text = "Add";
            Add.Location = new Point(169, 39); 
            Add.Size = new Size(50, 23);
            Add.Click += AddClick;
            Controls.Add(Add);

            Remove = new Button();
            Remove.Text = "Remove";
            Remove.Location = new Point(138, 320);
            Remove.Size = new Size(75, 23);
            Remove.Click += RemoveClick;
            Controls.Add(Remove);

            Roads = new DataGridView();
            Roads.Location = new Point(12, 68);
            Roads.Size = new Size(201, 246);
            Roads.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Roads.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            Roads.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Roads.AllowUserToResizeColumns = false;
            Roads.AllowUserToResizeRows = false;
            Roads.RowHeadersVisible = false;
            Roads.ReadOnly = true;
            Controls.Add(Roads);
            Roads.Show();
        }
        private void ReadData(object sender, EventArgs args)
        {
            Roads.DataSource = new BindingSource(new BindingList<RoadType>(Project.RoadTypes), null);
        }
        private void AddClick(object sender, EventArgs e)
        {
            if (SetName.Text.Length > 0 && Project.RoadTypes.Find(d => d.Name == SetName.Text) == null)
            {
                Project.RoadTypes.Add(new RoadType(SetName.Text, Convert.ToInt32(SetSpeed.Text)));
                Roads.DataSource = new BindingSource(new BindingList<RoadType>(Project.RoadTypes), null);
                NameLabel.ForeColor = Color.Black;
            }
            else
            {
                NameLabel.ForeColor = Color.Red;
            }
        }
        private void RemoveClick(object sender, EventArgs e)
        {
            // Set Road Types to Default if the type is to be removed.
            foreach (Node node in Project.Nodes)
                foreach (Road road in node.Roads)
                    foreach (DataGridViewRow row in Roads.SelectedRows)
                        if (road.Type == (RoadType)row.DataBoundItem)
                            road.Type = Project.RoadTypes[0]; // Default

            // Remove Selected Rows
            foreach (DataGridViewRow row in Roads.SelectedRows)
                if (((RoadType)row.DataBoundItem).Name != "Default")
                    Roads.Rows.Remove(row);
        }
    }
}
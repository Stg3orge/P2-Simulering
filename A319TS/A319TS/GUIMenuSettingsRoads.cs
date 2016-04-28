using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

namespace A319TS
{
    class GUIMenuSettingsRoads : Form
    {
        private Project Project;
        private Label NameLabel;
        private Label SpeedLabel;
        private TextBox SetName;
        private TextBox SetSpeed;
        private Button Add;
        private Button Remove;
        private DataGridView RoadData;
        
        public GUIMenuSettingsRoads(Project project)
        {
            Project = project;
            Setup();
            Load += ReadData;
        }
        
        private void ReadData(object sender, EventArgs args)
        {
            RoadData.DataSource = new BindingSource(new BindingList<RoadType>(Project.RoadTypes), null);
            RoadData.Show();
        }
        
        private void Setup()
        {
            Text = "Roads";
            Size = new Size(490, 269);
            MinimumSize = new Size(490, 269);
            MaximumSize = new Size(1000, 1000);
            ShowIcon = false;
            MinimizeBox = false;
            MaximizeBox = false;
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterParent;

            NameLabel = new Label();
            NameLabel.Text = "Name";
            NameLabel.Location = new Point(13, 12);
            NameLabel.AutoSize = true;
            Controls.Add(NameLabel);

            SetName = new TextBox();
            SetName.Location = new Point(64, 12);
            SetName.Size = new Size(131, 22);
            Controls.Add(SetName);

            SpeedLabel = new Label();
            SpeedLabel.Text = "Speed";
            SpeedLabel.Location = new Point(13, 46);
            SpeedLabel.AutoSize = true;
            Controls.Add(SpeedLabel);

            SetSpeed = new TextBox();
            SetSpeed.Location = new Point(64, 43);
            SetSpeed.Size = new Size(131, 24);
            Controls.Add(SetSpeed);

            Add = new Button();
            Add.Text = "Add";
            Add.Location = new Point(12, 105); 
            Add.Size = new Size(183, 46);
            Add.Click += AddClick;
            Controls.Add(Add);

            Remove = new Button();
            Remove.Text = "Remove";
            Remove.Location = new Point(12, 159);
            Remove.Size = new Size(183, 46);
            Remove.Click += RemoveClick;
            Controls.Add(Remove);

            RoadData = new DataGridView();
            RoadData.Location = new Point(211, 12);
            RoadData.Size = new Size(243, 193);
            Controls.Add(RoadData);
        }
        private void AddClick(object sender, EventArgs e)
        {
            if (SetName.Text.Length > 0 && Project.RoadTypes.Find(d => d.Name == SetName.Text) == null)
            {
                Project.RoadTypes.Add(new RoadType(SetName.Text, Convert.ToInt32(SetSpeed.Text)));
                RoadData.DataSource = new BindingSource(new BindingList<RoadType>(Project.RoadTypes), null);
                NameLabel.ForeColor = Color.Black;
            }
            else
            {
                NameLabel.ForeColor = Color.Red;
            }
        }
        private void RemoveClick(object sender, EventArgs e)
        {

            foreach (DataGridViewRow row in RoadData.SelectedRows)
                RoadData.Rows.Remove(row);
        }
    }
}
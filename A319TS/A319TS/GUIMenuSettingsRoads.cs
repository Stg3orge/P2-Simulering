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
        private TextBox NameSet;
        private TextBox SpeedSet;
        private Button Add;
        private Label NameLabel;
        private Label SpeedLabel;
        private DataGridView RoadData;
        Project Project;
        public GUIMenuSettingsRoads(Project project)
        {
            Project = project;
            Text = "Road";
            Size = new Size(582, 269);
            MinimumSize = new Size(582, 269);
            MaximumSize = new Size(1000, 1000);
            ShowIcon = false;
            MinimizeBox = false;
            MaximizeBox = false;
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterParent;
            InitializeGUISettingsRoads();

            Controls.Add(NameSet);
            Controls.Add(NameLabel);
            Controls.Add(SpeedSet);
            Controls.Add(Add);
            //Controls.Add(ColorLabel);
            Controls.Add(RoadData);
        }
        private void InitializeGUISettingsRoads()
        {
            NameSet = new TextBox();
            NameSet.Location = new Point(64, 12);
            NameSet.Size = new Size(131, 22);

            NameLabel = new Label();
            NameLabel.Text = "Name";
            NameLabel.Location = new Point(13, 12);

            
            SpeedLabel = new Label();
            SpeedLabel.Text = "Speed";
            SpeedLabel.Location = new Point(13, 46);
            
            SpeedSet = new TextBox();
            SpeedSet.Location = new Point(64, 43);
            SpeedSet.Size = new Size(131, 24);

            Add = new Button();
            Add.Text = "Add";
            Add.Location = new Point(12, 159);
            Add.Size = new Size(183, 46);
            Add.Click += AddClick;

            RoadData = new DataGridView();
            RoadData.Text = "SetOfRoads";
            RoadData.Location = new Point(218, 12);
            RoadData.Size = new Size(329, 193);
            RoadData.DataSource = Project.RoadTypes; //RoadData.DataSource = new BindingSource(new BindingList<RoadType>(Project.RoadTypes), null);
            RoadData.Show();
        }

        private void AddClick(object sender, EventArgs e)
        {


            if (NameSet.Text.Length > 0 && Project.RoadTypes.Find(d => d.Name == NameSet.Text) == null)
            {
                Project.RoadTypes.Add(new RoadType(NameSet.Text, Convert.ToInt32(SpeedSet.Text)));
                NameLabel.ForeColor = Color.Black;
            }
            else
            {
                NameLabel.ForeColor = Color.Red;
            }
            RoadData.DataSource = new BindingSource(new BindingList<RoadType>(Project.RoadTypes), null);
        }
    }
}

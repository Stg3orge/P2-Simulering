using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace A319TS
{
    class GUIMenuFileNew : Form
    {
        public Project NewProject { get; set; }
        public GUIMenuFileNew()
        {
            Text = "New"; // Window Title
            Size = new Size(210, 110);
            MinimumSize = new Size(210, 110);
            MaximumSize = new Size(210, 110);
            ShowIcon = false;
            MinimizeBox = false;
            MaximizeBox = false;
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterParent;

            InitializeGUIMenuFileNew();

            Controls.Add(Create);
            Controls.Add(ProjectName);
            Controls.Add(ProjectNameLabel);
        }
        private void InitializeGUIMenuFileNew()
        {
            ProjectName = new TextBox();
            ProjectName.Location = new Point(82, 12);
            ProjectName.Size = new Size(100, 20);
            ProjectNameLabel = new Label();
            ProjectNameLabel.Text = "Name";
            ProjectNameLabel.Location = new Point(12, 13);

            Create = new Button();
            Create.Text = "Create";
            Create.Location = new Point(12, 38);
            Create.Size = new Size(170, 23);
            Create.Click += CreateClick;
        }
        private TextBox ProjectName;
        private Label ProjectNameLabel;
        private Button Create;
        private void CreateClick(object sender, EventArgs e)
        {
            if (ProjectName.Text.Length > 0)
            {
                NewProject = new Project(ProjectName.Text);
                Close();
            }
            else
                ProjectNameLabel.ForeColor = Color.DarkRed;
        }
    }
}


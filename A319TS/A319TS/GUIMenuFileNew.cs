using System;
using System.Drawing;
using System.Windows.Forms;

namespace A319TS
{
    class GUIMenuFileNew : Form
    {
        public Project NewProject { get; set; }
        private TextBox ProjectName;
        private Label ProjectNameLabel;
        private Button Create;

        public GUIMenuFileNew()
        {
            Setup();
        }

        private void Setup()
        {
            Text = "New";
            Size = new Size(210, 110);
            MinimumSize = new Size(210, 110);
            MaximumSize = new Size(210, 110);
            ShowIcon = false;
            MinimizeBox = false;
            MaximizeBox = false;
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterParent;

            ProjectName = new TextBox();
            ProjectName.Location = new Point(82, 12);
            ProjectName.Size = new Size(100, 20);
            Controls.Add(ProjectName);

            ProjectNameLabel = new Label();
            ProjectNameLabel.Text = "Name";
            ProjectNameLabel.Location = new Point(12, 13);
            Controls.Add(ProjectNameLabel);

            Create = new Button();
            Create.Text = "Create";
            Create.Location = new Point(12, 38);
            Create.Size = new Size(170, 23);
            Create.Click += CreateClick;
            Controls.Add(Create);
        }
        
        private void CreateClick(object sender, EventArgs args)
        {
            if (ProjectName.Text.Length > 0)
            {
                NewProject = new Project(ProjectName.Text);
                Close();
            }
            else
            {
                ProjectNameLabel.ForeColor = Color.DarkRed;
            }
        }
    }
}
using System;
using System.Drawing;
using System.Windows.Forms;

namespace A319TS
{
    class GUIMenuSettingsProject : Form
    {
        public Project Project { get; set; }
        private TextBox NameOfProject;
        private Label ProjectNameLabel;
        private Button Ok;
        
        public GUIMenuSettingsProject(Project project)
        {
            Project = project;
            Setup();
        }

        private void Setup()
        {
            Text = "Project";
            Size = new Size(210, 110);
            MinimumSize = new Size(210, 110);
            MaximumSize = new Size(210, 110);
            ShowIcon = false;
            MinimizeBox = false;
            MaximizeBox = false;
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterParent;

            NameOfProject = new TextBox();
            NameOfProject.Location = new Point(82, 12);
            NameOfProject.Size = new Size(100, 20);
            Controls.Add(NameOfProject);

            ProjectNameLabel = new Label();
            ProjectNameLabel.Text = "Name";
            ProjectNameLabel.Location = new Point(12, 13);
            Controls.Add(ProjectNameLabel);
            
            Ok = new Button();
            Ok.Text = "Ok";
            Ok.Location = new Point(12, 38);
            Ok.Size = new Size(170, 23);
            Ok.Click += CreateClick;
            Controls.Add(Ok);
        }
        private void CreateClick(object sender, EventArgs e)
        {
            if (NameOfProject.Text.Length > 0)
            {
                Project.Name = NameOfProject.Text;
                Close();
            }
            else
            {
                ProjectNameLabel.ForeColor = Color.DarkRed;
            }
        }
    }
}

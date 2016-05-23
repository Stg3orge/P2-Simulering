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
            Size = new Size(274, 92);
            MinimumSize = new Size(274, 92);
            MaximumSize = new Size(274, 92);
            ShowIcon = false;
            MinimizeBox = false;
            MaximizeBox = false;
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterParent;

            NameOfProject = new TextBox();
            NameOfProject.Location = new Point(63, 12);
            NameOfProject.Size = new Size(100, 22);
            Controls.Add(NameOfProject);

            ProjectNameLabel = new Label();
            ProjectNameLabel.Text = "Name";
            ProjectNameLabel.Location = new Point(12, 15);
            Controls.Add(ProjectNameLabel);
            
            Ok = new Button();
            Ok.Text = "Ok";
            Ok.Location = new Point(169, 12);
            Ok.Size = new Size(75, 23);
            Ok.Click += OkClick;
            Controls.Add(Ok);
        }
        private void OkClick(object sender, EventArgs args)
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

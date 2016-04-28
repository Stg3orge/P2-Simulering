using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace A319TS
{
    class GUIMenuSettingsProject : Form
    {
        private TextBox NameOfProject;
        private Label ProjectNameLabel;
        private Button Ok;
        public Project Project { get; set; }
        public GUIMenuSettingsProject(Project project)
        {
            Project = project;
            Text = "Project";
            Size = new Size(210, 110);
            MinimumSize = new Size(210, 110);
            MaximumSize = new Size(210, 110);
            ShowIcon = false;
            MinimizeBox = false;
            MaximizeBox = false;
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterParent;

            InitializeGUISettingsProject();
            Controls.Add(Ok);
            Controls.Add(NameOfProject);
            Controls.Add(ProjectNameLabel);
        }
        private void InitializeGUISettingsProject()
        {
            NameOfProject = new TextBox();
            NameOfProject.Location = new Point(82, 12);
            NameOfProject.Size = new Size(100, 20);
            ProjectNameLabel = new Label();
            ProjectNameLabel.Text = "Name";
            ProjectNameLabel.Location = new Point(12, 13);

            Ok = new Button();
            Ok.Text = "Ok";
            Ok.Location = new Point(12, 38);
            Ok.Size = new Size(170, 23);
            Ok.Click += CreateClick;
        }
        private void CreateClick(object sender, EventArgs e)
        {
            if (NameOfProject.Text.Length > 0)
            {
                Project.Name = NameOfProject.Text;
                Close();
            }
            else
                ProjectNameLabel.ForeColor = Color.DarkRed;
        }
    }
}

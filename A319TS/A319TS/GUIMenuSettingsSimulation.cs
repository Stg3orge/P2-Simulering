using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace A319TS
{
    class GUIMenuSettingsSimulation : Form
    {
        private Project Project;

        public GUIMenuSettingsSimulation(Project project)
        {
            Project = project;
            Setup();
        }

        private void Setup()
        {
            Text = "Simulation";
            Size = new Size(368, 400);
            MinimumSize = new Size(368, 400);
            MaximumSize = new Size(368, 400);
            ShowIcon = false;
            MinimizeBox = false;
            MaximizeBox = false;
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterParent;
        }
    }
}

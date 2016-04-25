using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace A319TS
{
    partial class GUIToolEdit : Form
    {
        private Project _project;
        public GUIToolEdit(object obj, Project project)
        {
            _project = project;
            Setup();
            if (obj is Node)
                InitEditNode(obj as Node);
            else if (obj is Destination)
                InitEditDestination(obj as Destination);
            else if (obj is LightController)
                InitEditLightController(obj as LightController);
        }
        private void Setup()
        {
            Text = "Edit";
            ShowIcon = false;
            MinimizeBox = false;
            MaximizeBox = false;
            SizeGripStyle = SizeGripStyle.Hide;
        }
        private void SetSize(int width, int height)
        {
            Size = new Size(width, height);
            MinimumSize = new Size(width, height);
            MaximumSize = new Size(width, height);
        }
    }
}

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
        public GUIToolEdit(object obj)
        {
            Setup();
            if (obj is Node)
                InitEditNode(obj as Node);
            else if (obj is Destination)
                EditDestination(obj);
            else if (obj is LightController)
                EditLightController(obj);
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
        
        private void EditLightController(object obj)
        {
            SetSize(210, 110);

        }
        private void EditDestination(object obj)
        {
            SetSize(210, 110);

        }
    }
}

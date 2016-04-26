using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace A319TS
{
    class GUIToolEditLightController : Form
    {
        public LightController Controller;
        public GUIToolEditLightController(LightController controller)
        {
            Controller = controller;
            Setup();
        }
        private void Setup()
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace A319TS
{
    class GUIToolEditDestination : Form
    {
        public Destination Destination;
        public GUIToolEditDestination(Destination dest)
        {
            Destination = dest;
            Setup();
        }
        private void Setup()
        {

        }
    }
}

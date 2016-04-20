using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace A319TS
{
    class Layer : PictureBox
    {
        public Layer()
        {
            DoubleBuffered = true;
        }
    }
}

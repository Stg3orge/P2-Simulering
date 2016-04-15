using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace A319TS
{
    class Viewport : Panel
    {
        public Project Project;
        private double _zoom = 1;
        public double Zoom
        {
            get { return _zoom; }
            set
            {
                if (value > 2)
                    _zoom = 2;
                else if (value < 0.5)
                    _zoom = 0.5;
                else
                    _zoom = value;
            }
        }
        public Point Position = new Point(0, 0);
        public Viewport(Project project) : base()
        {
            Project = project;
            DoubleBuffered = true;
            Paint += Draw;
        }
        public void Reset()
        {
            Position.X = 0;
            Position.Y = 0;
            _zoom = 1;
        }
        private void Draw(object sender, PaintEventArgs e) {}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace A319TS
{
    partial class Viewport : Panel
    {
        public readonly int GridLength = 30000;
        public readonly int GridSize = 16;
        public readonly int NodeSize = 8;

        public Project Project;
        public Point MousePos = new Point(0, 0);
        public Point GridPos { get { return GetGridPos(); } }
        public Point ViewPos = new Point(0, 0);
        private float _zoom = 1;
        public float Zoom
        {
            get { return _zoom; }
            set { SetZoom(value); }
        }
        
        public Viewport(Project project) : base()
        {
            InitControls();
            DoubleBuffered = true;
            Project = project;
        }
        
        public void Reset()
        {
            ViewPos = new Point(0, 0);
            Zoom = 1;
            Refresh();
        }
        private void SetZoom(float value)
        {
            if (value > 1.25)
                _zoom = 1.25F;
            else if (value < 0.25)
                _zoom = 0.25F;
            else
                _zoom = value;
        }
        private Point GetGridPos()
        {
            int x = Convert.ToInt32((MousePos.X - ViewPos.X) / GridSize / Zoom);
            int y = Convert.ToInt32((MousePos.Y - ViewPos.Y) / GridSize / Zoom);
            return new Point(x, y);
        }
        private void OnMove(object sender, MouseEventArgs args)
        {
            if (args.Button == MouseButtons.Middle)
            {
                ViewPos.X += args.Location.X - MousePos.X;
                ViewPos.Y += args.Location.Y - MousePos.Y;
                Refresh();
            }
            MousePos = args.Location;
        }
        private void OnWheel(object sender, MouseEventArgs args)
        {
            if (args.Delta > 0)
                Zoom += 0.25F;
            else
                Zoom -= 0.25F;
            Refresh();
        }
    }
}
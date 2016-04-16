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
        public const int GridLength = 30000;
        private double _zoom;
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
        private Point _position;
        public Point Position
        {
            get { return _position; }
            set { SetPosition(value); }
        }
        private void SetPosition(Point value)
        {
            if (value.X < -16)
                _position.X = -16;
            else if (value.X > GridLength * 16)
                _position.X = GridLength * 16;
            else
                _position.X = value.X;

            if (value.Y < -16)
                _position.Y = -16;
            else if (value.Y > GridLength * 16)
                _position.Y = GridLength * 16;
            else
                _position.Y = value.Y;
        }
        public Viewport(Project project) : base()
        {
            Project = project;
            Position = new Point(-16, -16);
            Zoom = 1;
            DoubleBuffered = true;
            Paint += Draw;
        }
        public void Reset()
        {
            Position = new Point(-16, -16);
            _zoom = 1;
            Refresh();
        }
        private void Draw(object sender, PaintEventArgs args)
        {

        }
        private void DrawGrid(PaintEventArgs args)
        {

        }
    }
}

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

        public int Length { get { return Convert.ToInt32(GridLength * GridSize * Zoom); } }
        public Point MousePos = new Point(0, 0);
        public Point GridPos { get; private set; }
        public Project Project;
        private float _zoom = 1;
        public float Zoom
        {
            get { return _zoom; }
            set
            {
                if (value > 1.25)
                    _zoom = 1.25F;
                else if (value < 0.25)
                    _zoom = 0.25F;
                else
                    _zoom = value;
            }
        }
        
        public Viewport(Project project) : base()
        {
            DoubleBuffered = true;
            InitControls();
            MouseMove += OnMouseMove;
            GridPos = new Point(0, 0);
            Project = project;
        }
        
        public void Reset()
        {
            HScrollBar.Value = 0;
            VScrollBar.Value = 0;
            _zoom = 1;
            Refresh();
        }
        public void UpdateSize()
        {
            Size = new Size(Length, Length);
            HScrollBar.Maximum = Length - Width;
            VScrollBar.Maximum = Length - Height;
        }
        private void OnMouseMove(object sender, MouseEventArgs args)
        {
            int x = Convert.ToInt32((MousePos.X - HScrollBar.Value) * Zoom);
            int y = Convert.ToInt32((MousePos.Y - VScrollBar.Value) * Zoom);
            GridPos = new Point(x, y);
        }
        private void OnScrollValueChanged(object sender, EventArgs args)
        {
            Refresh();
        }
    }
}
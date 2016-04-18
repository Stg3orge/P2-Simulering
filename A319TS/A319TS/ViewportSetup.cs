using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace A319TS
{
    partial class Viewport : Panel
    {
        public HScrollBar HScrollBar = new HScrollBar();
        public VScrollBar VScrollBar = new VScrollBar();
        public PictureBox Grid = new PictureBox();
        public PictureBox Roads = new PictureBox();
        public PictureBox Nodes = new PictureBox();
        public PictureBox Entities = new PictureBox();
        public PictureBox Information = new PictureBox();

        private void InitControls()
        {
            HScrollBar.Dock = DockStyle.Bottom;
            VScrollBar.Dock = DockStyle.Right;
            HScrollBar.Minimum = 0;
            VScrollBar.Minimum = 0;
            HScrollBar.Maximum = Convert.ToInt32(((GridLength * GridSize) * Zoom) - Width);
            VScrollBar.Maximum = Convert.ToInt32(((GridLength * GridSize) * Zoom) - Height);
            HScrollBar.ValueChanged += OnScrollValueChanged;
            VScrollBar.ValueChanged += OnScrollValueChanged;

            Grid.Dock = DockStyle.Fill;
            Grid.BackColor = Color.Transparent;
            Grid.Region = null;

            Roads.Dock = DockStyle.Fill;
            Roads.BackColor = Color.Transparent;
            Roads.Region = null;

            Nodes.Dock = DockStyle.Fill;
            Nodes.BackColor = Color.Transparent;
            Nodes.Region = null;

            Entities.Dock = DockStyle.Fill;
            Entities.BackColor = Color.Transparent;
            Entities.Region = null;

            Information.Dock = DockStyle.Fill;
            Information.BackColor = Color.Transparent;
            Information.Region = null;

            Controls.Add(HScrollBar);
            Controls.Add(VScrollBar);
            Controls.Add(Grid);
            Grid.Controls.Add(Roads);
            Roads.Controls.Add(Nodes);
            Nodes.Controls.Add(Entities);
            Entities.Controls.Add(Information);
        }
    }
}
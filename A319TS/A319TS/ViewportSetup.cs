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
        public Layer Grid = new Layer();
        public Layer Roads = new Layer();
        public Layer Nodes = new Layer();
        public Layer Entities = new Layer();
        public Layer Information = new Layer();
        public Layer Input = new Layer();

        private void InitControls()
        {
            Grid.Dock = DockStyle.Fill;
            Grid.BackColor = Color.Transparent;
            Grid.Region = null;
            Grid.Paint += DrawGrid;

            Roads.Dock = DockStyle.Fill;
            Roads.BackColor = Color.Transparent;
            Roads.Region = null;
            Roads.Paint += DrawRoads;

            Nodes.Dock = DockStyle.Fill;
            Nodes.BackColor = Color.Transparent;
            Nodes.Region = null;
            Nodes.Paint += DrawNodes;

            Entities.Dock = DockStyle.Fill;
            Entities.BackColor = Color.Transparent;
            Entities.Region = null;
            Entities.Paint += DrawEntities;

            Information.Dock = DockStyle.Fill;
            Information.BackColor = Color.Transparent;
            Information.Region = null;
            Information.Paint += DrawInformation;

            Input.Dock = DockStyle.Fill;
            Input.BackColor = Color.Transparent;
            Input.MouseMove += OnMove;
            Input.MouseWheel += OnWheel;

            Controls.Add(Grid);
            Grid.Controls.Add(Roads);
            Roads.Controls.Add(Nodes);
            Nodes.Controls.Add(Entities);
            Entities.Controls.Add(Information);
            Information.Controls.Add(Input);
        }
    }
}
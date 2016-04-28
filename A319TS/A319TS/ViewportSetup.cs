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
        public Layer Connections = new Layer();
        public Layer Nodes = new Layer();
        public Layer Entities = new Layer();
        public Layer Information = new Layer();
        public Layer Input = new Layer();

        private void InitControls()
        {
            Grid.Dock = DockStyle.Fill;
            Grid.BackColor = Color.Transparent;
            Grid.Paint += DrawGrid;

            Connections.Dock = DockStyle.Fill;
            Connections.BackColor = Color.Transparent;
            Connections.Paint += DrawConnections;

            Nodes.Dock = DockStyle.Fill;
            Nodes.BackColor = Color.Transparent;
            Nodes.Paint += DrawNodes;

            Entities.Dock = DockStyle.Fill;
            Entities.BackColor = Color.Transparent;
            Entities.Paint += DrawEntities;

            Information.Dock = DockStyle.Fill;
            Information.BackColor = Color.Transparent;
            Information.Paint += DrawInformation;

            Input.Dock = DockStyle.Fill;
            Input.BackColor = Color.Transparent;
            Input.MouseMove += OnMove;
            Input.MouseWheel += OnWheel;

            Controls.Add(Grid);
            Grid.Controls.Add(Connections);
            Connections.Controls.Add(Nodes);
            Nodes.Controls.Add(Entities);
            Entities.Controls.Add(Information);
            Information.Controls.Add(Input);
        }
    }
}
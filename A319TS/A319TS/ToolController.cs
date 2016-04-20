using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace A319TS
{
    class ToolController
    {
        private bool FirstRoadConnection = true;
        private Node FirstRoad;

        public ToolStripItemCollection Tools;
        public Viewport Viewport;
        public Project CurrentProject;

        public ToolController(ToolStripItemCollection collection, Viewport viewport, Project currentProject)
        {
            Tools = collection;
            Viewport = viewport;
            Viewport.Input.MouseClick += ViewportClick;
            CurrentProject = currentProject;
        }

        private void ViewportClick(object sender, MouseEventArgs args)
        {
            ToolStripButton activeTool = null;
            foreach (ToolStripButton tool in Tools.OfType<ToolStripButton>())
                if (tool.Checked)
                    activeTool = tool;

            if (activeTool != null)
            {
                switch (activeTool.Name)
                {
                    case "ToolAddNode": AddNode(); break;
                    case "ToolRemoveNode": RemoveNode(); break;
                    case "ToolAddRoad": AddRoad(); break;
                    default: break;
                }
            }
        }

        private void AddNode()
        {
            if (CurrentProject.Nodes.Find(n => n.Position == Viewport.GridPos) == null)
            {
                CurrentProject.Nodes.Add(new Node(Viewport.GridPos));
                Viewport.Nodes.Refresh();
            }
        }
        private void RemoveNode()
        {
            Node targetNode = CurrentProject.Nodes.Find(n => n.Position == Viewport.GridPos);
            if (targetNode != null)
            {
                foreach (Node node in CurrentProject.Nodes)
                    foreach (Road road in node.Roads)
                        if (road.Destination == targetNode)
                            node.Roads.Remove(road);

                CurrentProject.Nodes.Remove(targetNode);
            }
        }
        private void AddRoad()
        {
            foreach (Node node in CurrentProject.Nodes)
            {
                if (Viewport.GridPos == node.Position)
                {
                    if (FirstRoadConnection)
                    {
                        FirstRoad = node;
                        FirstRoadConnection = false;
                    }
                    else
                    {
                        FirstRoadConnection = true;
                    }
                }
            }
        }
    }
}

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

        public ToolStripButton ActiveTool;
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

        public void ToggleTool(ToolStripButton clickedTool)
        {
            if (clickedTool.Checked)
            {
                clickedTool.Checked = false;
                ActiveTool = null;
            }  
            else
            {
                foreach (ToolStripButton tool in Tools.OfType<ToolStripButton>())
                {
                    if (tool == clickedTool)
                    {
                        tool.Checked = true;
                        ActiveTool = tool;
                    }
                    else
                        tool.Checked = false;
                }
            }
        }

        private void ViewportClick(object sender, MouseEventArgs args)
        {
            if (ActiveTool != null)
            {
                switch (ActiveTool.Name)
                {
                    case "ToolAddNode": AddNode(); break;
                    case "ToolRemoveNode": RemoveNode(); break;
                    case "ToolAddRoad": AddRoad(); break;
                    case "ToolPrimaryRoad": PrimaryRoad(); break;
                    case "ToolSecondaryRoad": SecondaryRoad(); break;
                    case "ToolEdit": Edit(); break;
                    default: break;
                }
            }
        }

        private void AddNode()
        {
            Node target = CurrentProject.Nodes.Find(n => n.Position == Viewport.GridPos);
            if (target == null)
                CurrentProject.Nodes.Add(new Node(Viewport.GridPos));
            else
                target.Type = Node.NodeType.None;
            Viewport.Nodes.Refresh();
        }
        private void RemoveNode()
        {
            Node target = CurrentProject.Nodes.Find(n => n.Position == Viewport.GridPos);
            if (target != null)
            {
                foreach (Node node in CurrentProject.Nodes)
                    for (int i = 0; i < node.Roads.Count; i++)
                        if (node.Roads[i].Destination == target)
                            node.Roads.Remove(node.Roads[i]);

                CurrentProject.Nodes.Remove(target);
                Viewport.Roads.Refresh();
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
        private void PrimaryRoad()
        {

        }
        private void SecondaryRoad()
        {

        }
        private void Edit()
        {

        }
    }
}

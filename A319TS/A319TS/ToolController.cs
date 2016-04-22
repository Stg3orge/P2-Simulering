using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace A319TS
{
    class ToolController
    {
        private bool FirstRoadConnection = true;
        private Node FirstRoad;

        public ToolStripButton ActiveTool;
        public ToolStripItemCollection Tools;
        public Viewport Viewport;
        public Project Project;

        public ToolController(ToolStripItemCollection collection, Viewport viewport, Project project)
        {
            Tools = collection;
            Viewport = viewport;
            Viewport.Input.MouseClick += ViewportClick;
            Project = project;
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
        private object GetObjByGridPos()
        {
            Node node = Project.Nodes.Find(n => n.Position == Viewport.GridPos);
            LightController controller = Project.LightControllers.Find(l => l.Position == Viewport.GridPos);
            Destination dest = Project.Destinations.Find(d => d.Position == Viewport.GridPos);
            if (node != null)
                return node;
            else if (controller != null)
                return controller;
            else if (dest != null)
                return dest;
            else
                return null;
        }

        private void ViewportClick(object sender, MouseEventArgs args)
        {
            if (ActiveTool != null && args.Button == MouseButtons.Left)
            {
                switch (ActiveTool.Name)
                {
                    case "ToolAddNode": AddNode(); break;
                    case "ToolRemoveNode": RemoveNode(); break;
                    case "ToolSetNodeLight": SetNodeType(Node.NodeType.Light); break;
                    case "ToolSetNodeYield": SetNodeType(Node.NodeType.Yield); break;
                    case "ToolSetNodeHome": SetNodeType(Node.NodeType.Home); break;
                    case "ToolSetNodeParking": SetNodeType(Node.NodeType.Parking); break;
                    case "ToolAddDestination": ToolAddDestination(); break;
                    case "ToolAddLightController": AddLightController(); break;
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
            object target = GetObjByGridPos();
            if (target == null)
                Project.Nodes.Add(new Node(Viewport.GridPos));
            else if (target.GetType() == typeof(Node))
                ((Node)target).Type = Node.NodeType.None;
            Viewport.Nodes.Refresh();
        }
        private void RemoveNode()
        {
            Node target = Project.Nodes.Find(n => n.Position == Viewport.GridPos);
            if (target != null)
            {
                foreach (Node node in Project.Nodes)
                    for (int i = 0; i < node.Roads.Count; i++)
                        if (node.Roads[i].Destination == target)
                            node.Roads.Remove(node.Roads[i]);

                Project.Nodes.Remove(target);
                Viewport.Roads.Refresh();
            }
        }
        private void AddRoad()
        {
            foreach (Node node in Project.Nodes)
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
                        if (Control.ModifierKeys == Keys.Shift)
                        {
                            RoadType roadtype = new RoadType("lort", 90);
                            FirstRoad.Roads.Add(new Road(FirstRoad, node, roadtype));
                            FirstRoad = node;
                            Viewport.Roads.Refresh();
                        }
                        else
                        {
                            RoadType roadtype = new RoadType("lort", 90);
                            FirstRoad.Roads.Add(new Road(FirstRoad, node, roadtype));
                            FirstRoadConnection = true;
                            Viewport.Roads.Refresh();
                        }
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
        private void SetNodeType(Node.NodeType type)
        {
            Node target = Project.Nodes.Find(n => n.Position == Viewport.GridPos);
            if(target != null)
            {
                if (type == Node.NodeType.Light && target.Type == Node.NodeType.Light)
                    target.Green = !target.Green;
                else
                    target.Type = type;
                
                Viewport.Nodes.Refresh();
            }
        }
        private void ToolAddDestination()
        {
            if (GetObjByGridPos() == null)
                Project.Destinations.Add(new Destination(Viewport.GridPos, new DestinationType("Test", Color.Green)));
            Viewport.Entities.Refresh();
        }
        private void AddLightController()
        {
            if (GetObjByGridPos() == null)
                Project.LightControllers.Add(new LightController(Viewport.GridPos));
            Viewport.Entities.Refresh();
        }
    }
}

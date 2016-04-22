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
        private bool FirstLightControllerConnection = true;
        private LightController FirstLightController;

        public ToolStripButton ActiveTool;
        public ToolStripItemCollection Tools;
        public Viewport Viewport;
        public Project Project;

        bool FirstMove = true;
        object FirstObjectMove;

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

        private void ViewportClick(object sender, MouseEventArgs args)
        {
            if (ActiveTool != null && args.Button == MouseButtons.Left)
            {
                switch (ActiveTool.Name)
                {
                    case "ToolAddNode": AddNode(); break;
                    case "ToolSetNodeLight": SetNodeType(Node.NodeType.Light); break;
                    case "ToolSetNodeYield": SetNodeType(Node.NodeType.Yield); break;
                    case "ToolSetNodeHome": SetNodeType(Node.NodeType.Home); break;
                    case "ToolSetNodeParking": SetNodeType(Node.NodeType.Parking); break;
                    case "ToolAddLightController": AddLightController(); break;
                    case "ToolLinkLight": LinkLight(); break;
                    case "ToolAddDestination": ToolAddDestination(); break;
                    case "ToolAddRoad": AddRoad(); break;
                    case "ToolPrimaryRoad": PrimaryRoad(); break;
                    case "ToolSecondaryRoad": SecondaryRoad(); break;
                    case "ToolEdit": Edit(); break;
                    case "ToolRemove": Remove(); break;
                    case "ToolMoveObject": ToolMoveObject(); break;
                    default: break;
                }
            }
        }

        private void AddNode()
        {
            object target = Viewport.GetObjByGridPos();
            if (target == null)
                Project.Nodes.Add(new Node(Viewport.GridPos));
            else if (target.GetType() == typeof(Node))
                ((Node)target).Type = Node.NodeType.None;
            Viewport.Nodes.Refresh();
        }
        private void RemoveNode(Node target)
        {
            foreach (Node node in Project.Nodes)
                for (int i = 0; i < node.Roads.Count; i++)
                    if (node.Roads[i].Destination == target)
                        node.Roads.Remove(node.Roads[i]);

            Project.Nodes.Remove(target);
            Viewport.Roads.Refresh();
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
                        Viewport.HoverConnection = node.Position;
                    }
                    else
                    {
                        FirstRoad.Roads.Add(new Road(FirstRoad, node, new RoadType("lort", 90)));
                        if (Control.ModifierKeys == Keys.Shift)
                        {
                            FirstRoad = node;
                            Viewport.HoverConnection = FirstRoad.Position;
                        } 
                        else
                        {
                            FirstRoadConnection = true;
                            Viewport.HoverConnection = new Point(-1, -1);
                        }
                            
                        Viewport.Roads.Refresh();
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
        private void Remove()
        {
            object target = Viewport.GetObjByGridPos();
            if (target != null)
            {
                if (target.GetType() == typeof(Node))
                    RemoveNode((Node)target);
                else if (target.GetType() == typeof(Destination))
                    Project.Destinations.Remove((Destination)target);
                else if (target.GetType() == typeof(LightController))
                    Project.LightControllers.Remove((LightController)target);
                Viewport.Roads.Refresh();
            }
        }
        private void ToolMoveObject()
        {
            object obj = Viewport.GetObjByGridPos();

            if (FirstMove && obj != null)
            {
                FirstObjectMove = obj;
                Viewport.HoverConnection = Viewport.GridPos;
                FirstMove = false;
            }
            else if(!FirstMove && obj == null)
            {
                if (FirstObjectMove.GetType() == typeof(Node))
                {
                    Node node = (Node)FirstObjectMove;
                    node.Position = Viewport.GridPos;
                    Viewport.HoverConnection = new Point(-1, -1);
                    FirstMove = true;
                }
                else if (FirstObjectMove.GetType() == typeof(LightController))
                {
                    LightController lightcontrol = (LightController)FirstObjectMove;
                    lightcontrol.Position = Viewport.GridPos;
                    Viewport.HoverConnection = new Point(-1, -1);
                    FirstMove = true;
                }
                else if (FirstObjectMove.GetType() == typeof(Destination))
                {
                    Destination dest = (Destination)FirstObjectMove;
                    dest.Position = Viewport.GridPos;
                    Viewport.HoverConnection = new Point(-1, -1);
                    FirstMove = true;
                }
            }
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
            if (Viewport.GetObjByGridPos() == null)
                Project.Destinations.Add(new Destination(Viewport.GridPos, new DestinationType("Test", Color.Green)));
            Viewport.Entities.Refresh();
        }
        private void AddLightController()
        {
            if (Viewport.GetObjByGridPos() == null)
                Project.LightControllers.Add(new LightController(Viewport.GridPos));
            Viewport.Entities.Refresh();
        }
        private void LinkLight()
        {
            object obj = Viewport.GetObjByGridPos();
            if (obj != null)
            {
                
                if (FirstLightControllerConnection && obj.GetType() == typeof(LightController))
                {
                    FirstLightController = (LightController)obj;
                    FirstLightControllerConnection = false;
                    Viewport.HoverConnection = FirstLightController.Position;
                    Viewport.Roads.Refresh();
                }
                else if (!FirstLightControllerConnection && obj.GetType() == typeof(Node))
                {
                    Node node = (Node)obj;
                    FirstLightController.Lights.Add(node);
                    FirstLightControllerConnection = true;
                    Viewport.HoverConnection = new Point(-1, -1);
                    Viewport.Roads.Refresh();
                }
            }
        }
        public void StopConnection(object sender, KeyEventArgs args)
        {
            if (args.KeyCode == Keys.Escape)
            {
                FirstRoad = null;
                FirstLightController = null;
                FirstRoadConnection = true;
                FirstLightControllerConnection = true;
                Viewport.HoverConnection = new Point(-1, -1);
            }
        }
    }
}

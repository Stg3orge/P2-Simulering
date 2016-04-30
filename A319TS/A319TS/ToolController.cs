using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

namespace A319TS
{
    class ToolController
    {
        private bool FirstRoadConnection = true;
        private Node FirstRoad;
        private bool FirstLightControllerConnection = true;
        private LightController FirstLightController;
        private bool FirstMove = true;
        private object FirstObjectMove;

        public DestinationType SelectedDestinationType
        {
            get { return (DestinationType)((ToolStripComboBox)Tools.Find("ToolDestinationTypeSelect", false)[0]).SelectedItem; }
        }
        public RoadType SelectedRoadType
        {
            get { return (RoadType)((ToolStripComboBox)Tools.Find("ToolRoadTypeSelect", false)[0]).SelectedItem; }
        }

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
                    case "ToolSetNodeInbound": SetNodeType(Node.NodeType.Inbound); break;
                    case "ToolSetNodeOutbound": SetNodeType(Node.NodeType.Outbound); break;
                    case "ToolAddLightController": AddLightController(); break;
                    case "ToolLinkLight": LinkLight(); break;
                    case "ToolAddDestination": ToolAddDestination(); break;
                    case "ToolAddRoad": AddRoad(Road.RoadDifferentiation.Shared); break;
                    case "ToolPrimaryRoad": AddRoad(Road.RoadDifferentiation.Primary); break;
                    case "ToolSecondaryRoad": AddRoad(Road.RoadDifferentiation.Secondary); break;
                    case "ToolEdit": Edit(); break;
                    case "ToolRemove": Remove(); break;
                    case "ToolMoveObject": ToolMoveObject(); break;
                    default: break;
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

        private void AddNode()
        {
            object target = Viewport.GetObjByGridPos();
            if (target == null)
                Project.Nodes.Add(new Node(Viewport.GridPos));
            else if (target is Node)
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
            Viewport.Connections.Refresh();
        }
        private void AddRoad(Road.RoadDifferentiation differentiation)
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
                        FirstRoad.Roads.Add(new Road(FirstRoad, node, SelectedRoadType, differentiation));
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

                        Viewport.Connections.Refresh();
                    }
                }
            }
        }
        private void Edit()
        {
            object obj = Viewport.GetObjByGridPos();
            if (obj != null)
            {
                Form EditDialog = null;
                if (obj is Node)
                    EditDialog = new GUIToolEditNode(obj as Node, Project);
                else if (obj is Destination)
                    EditDialog = new GUIToolEditDestination(obj as Destination, Project);
                else if (obj is LightController)
                    EditDialog = new GUIToolEditLightController(obj as LightController);
                EditDialog.ShowDialog();
                Viewport.Nodes.Refresh();
            }
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
                Viewport.Connections.Refresh();
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
            else if (!FirstMove && obj == null)
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
            if (target != null)
            {
                if (type == Node.NodeType.Inbound)
                {
                    if (target.Type == Node.NodeType.Inbound)
                    {
                        target.Type = Node.NodeType.None;
                        RunPathwerk();
                    }
                    if (target.Type != Node.NodeType.Inbound)
                    {
                        foreach (Node node in Project.Nodes)
                            if (node.Type == Node.NodeType.Inbound)
                                node.Type = Node.NodeType.None;
                        target.Type = Node.NodeType.Inbound;
                        Start = target;
                        RunPathwerk();
                    }
                }
                else if (type == Node.NodeType.Outbound)
                {
                    if (target.Type == Node.NodeType.Outbound)
                    {
                        target.Type = Node.NodeType.None;
                        RunPathwerk();
                    }
                    if (target.Type != Node.NodeType.Outbound)
                    {
                        foreach (Node node in Project.Nodes)
                            if (node.Type == Node.NodeType.Outbound)
                                node.Type = Node.NodeType.None;
                        target.Type = Node.NodeType.Outbound;
                        End = target;
                        RunPathwerk();
                    }
                }
                else if (type == Node.NodeType.Light && target.Type == Node.NodeType.Light)
                    target.Green = !target.Green;
                else
                    target.Type = type;

                Viewport.Nodes.Refresh();
            }
        }
        private Node Start;
        private Node End;
        private void RunPathwerk()
        {
            Pathwerk.AddProject(Project, Road.RoadDifferentiation.Primary);
            try
            {
                Project.Path = Pathwerk.FindPath(Start, End);
                Viewport.Connections.Refresh();
            }
            catch
            {
                Project.Path = null;
                Viewport.Connections.Refresh();
            }
        }

        private void ToolAddDestination()
        {
            if (Viewport.GetObjByGridPos() == null)
                Project.Destinations.Add(new Destination(Viewport.GridPos, SelectedDestinationType));
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
                    Viewport.Connections.Refresh();
                }
                else if (!FirstLightControllerConnection && obj.GetType() == typeof(Node))
                {
                    Node node = (Node)obj;
                    FirstLightController.Lights.Add(node);
                    FirstLightControllerConnection = true;
                    Viewport.HoverConnection = new Point(-1, -1);
                    Viewport.Connections.Refresh();
                }
            }
        }
    }
}

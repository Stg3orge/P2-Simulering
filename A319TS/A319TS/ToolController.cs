using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using System;

namespace A319TS
{
    class ToolController
    {
        private Node _firstNode;
        private LightController _firstController;
        private IPositionable _firstMoveObject;
        private bool _firstNodeConnection = true;
        private bool _firstControllerConnection = true;
        private bool _firstMove = true;

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
                    tool.Checked = false;
                clickedTool.Checked = true;
                ActiveTool = clickedTool;
            }
            StopConnection();
        }
        private void ViewportClick(object sender, MouseEventArgs args)
        {
            if (ActiveTool != null && args.Button == MouseButtons.Left)
            {
                switch (ActiveTool.Name)
                {
                    case "ToolAddNode": Add(typeof(Node)); break;
                    case "ToolSetNodeLight": SetNodeType(NodeTypes.Light); break;
                    case "ToolSetNodeYield": SetNodeType(NodeTypes.Yield); break;
                    case "ToolSetNodeHome": SetNodeType(NodeTypes.Home); break;
                    case "ToolSetNodeParking": SetNodeType(NodeTypes.Parking); break;
                    case "ToolSetNodeInbound": SetNodeType(NodeTypes.Inbound); break;
                    case "ToolSetNodeOutbound": SetNodeType(NodeTypes.Outbound); break;
                    case "ToolAddLightController": Add(typeof(LightController)); break;
                    case "ToolLinkLight": LinkLight(); break;
                    case "ToolAddDestination": Add(typeof(Destination)); break;
                    case "ToolAddRoad": AddRoad(Partitions.Shared); break;
                    case "ToolPrimaryRoad": AddRoad(Partitions.Primary); break;
                    case "ToolSecondaryRoad": AddRoad(Partitions.Secondary); break;
                    case "ToolEdit": Edit(); break;
                    case "ToolRemove": Remove(); break;
                    case "ToolMoveObject": Move(); break;
                    default: break;
                }
            }
        }
        public void OnKeyDown(object sender, KeyEventArgs args)
        {
            if (args.KeyCode == Keys.Escape)
                StopConnection();
        }
        private void StopConnection()
        {
            _firstNode = null;
            _firstController = null;
            _firstMoveObject = null;
            _firstNodeConnection = true;
            _firstControllerConnection = true;
            _firstMove = true;
            Viewport.HoverConnection = new Point(-1, -1);
        }

        // TOOLS
        private void Add(Type type)
        {
            object obj = Viewport.GetObjByGridPos();
            if (obj == null)
            {
                if (type == typeof(Node))
                {
                    Project.Nodes.Add(new Node(Viewport.GridPos));
                    Viewport.Nodes.Refresh();
                }
                else if (type == typeof(Destination))
                {
                    Project.Destinations.Add(new Destination(Viewport.GridPos, SelectedDestinationType));
                    Viewport.Entities.Refresh();
                }
                else if (type == typeof(LightController))
                {
                    Project.LightControllers.Add(new LightController(Viewport.GridPos));
                    Viewport.Entities.Refresh();
                }
            }
            else if (obj is Node)
            {
                ((Node)obj).Type = NodeTypes.None;
                Viewport.Nodes.Refresh();
            }
        }
        private void SetNodeType(NodeTypes type)
        {
            object obj = Viewport.GetObjByGridPos();
            if (obj is Node)
            {
                if (type == NodeTypes.Light && ((Node)obj).Type == NodeTypes.Light)
                    ((Node)obj).Green = !((Node)obj).Green;
                else
                    ((Node)obj).Type = type;

                Viewport.Nodes.Refresh();
            }
        }
        private void LinkLight()
        {
            object obj = Viewport.GetObjByGridPos();
            if (obj != null)
            {
                if (_firstControllerConnection && obj is LightController)
                {
                    _firstController = (LightController)obj;
                    _firstControllerConnection = false;
                    Viewport.HoverConnection = _firstController.Position;
                }
                else if (!_firstControllerConnection && obj is Node)
                {
                    _firstController.Lights.Add(obj as Node);
                    _firstControllerConnection = true;
                    Viewport.HoverConnection = new Point(-1, -1);
                }
                Viewport.Connections.Refresh();
            }
        }
        private void AddRoad(Partitions partition)
        {
            object obj = Viewport.GetObjByGridPos();
            if (obj != null && obj is Node)
            {
                if (_firstNodeConnection)
                {
                    _firstNode = (Node)obj;
                    _firstNodeConnection = false;
                    Viewport.HoverConnection = ((Node)obj).Position;
                }
                else
                {
                    _firstNode.Roads.Add(new Road(_firstNode, (Node)obj, SelectedRoadType, partition));
                    if (Control.ModifierKeys == Keys.Shift)
                    {
                        _firstNode = (Node)obj;
                        Viewport.HoverConnection = ((Node)obj).Position;
                    }
                    else
                    {
                        _firstNodeConnection = true;
                        Viewport.HoverConnection = new Point(-1, -1);
                    }
                    Viewport.Connections.Refresh();
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
        private void RemoveNode(Node target)
        {
            foreach (Node node in Project.Nodes)
                for (int i = 0; i < node.Roads.Count; i++)
                    if (node.Roads[i].To == target)
                        node.Roads.Remove(node.Roads[i]);

            Project.Nodes.Remove(target);
            Viewport.Connections.Refresh();
        }
        private void Move()
        {
            object obj = Viewport.GetObjByGridPos();
            if (_firstMove && obj != null && obj is IPositionable)
            {
                _firstMoveObject = obj as IPositionable;
                Viewport.HoverConnection = Viewport.GridPos;
                _firstMove = false;
            }
            else if (!_firstMove && obj == null)
            {
                _firstMoveObject.Position = Viewport.GridPos;
                Viewport.HoverConnection = new Point(-1, -1);
                _firstMove = true;
            }
        }
    }
}
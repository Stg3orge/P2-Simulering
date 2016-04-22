using System;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace A319TS
{
    partial class GUIMain : Form
    {
        public Project CurrentProject;
        public ToolController ToolController;

        public GUIMain()
        {
            CurrentProject = new Project("Unnamed Project");
            InitGUIMain();
            ToolController = new ToolController(GUIMainToolStrip.Items, GUIMainViewport, CurrentProject);

            CurrentProject.Nodes.Add(new Node(new Point(4, 4)));
            CurrentProject.Nodes.Add(new Node(new Point(10, 10), Node.NodeType.Light));
            CurrentProject.Nodes.Add(new Node(new Point(12, 12), Node.NodeType.Home));
            CurrentProject.Nodes.Add(new Node(new Point(14, 14), Node.NodeType.Parking));
            CurrentProject.Nodes.Add(new Node(new Point(16, 16), Node.NodeType.Yield));
            CurrentProject.Destinations.Add(new Destination(new Point(6, 6), new DestinationType("Work", Color.Green)));
            CurrentProject.LightControllers.Add(new LightController(new Point(8, 8)));
        }

        // MainMenuStrip Events
        private void MenuFileNewClick(object sender, EventArgs args)
        {
            Project project = FileHandler.NewProject();
            if (project != null)
            {
                CurrentProject = project;
                GUIMainViewport.Reset();
                UpdateAll();
            }
        }
        private void MenuFileOpenClick(object sender, EventArgs args)
        {
            Project project = FileHandler.OpenProject();
            if (project != null)
            {
                CurrentProject = project;
                GUIMainViewport.Reset();
            }
        }
        private void MenuFileSaveClick(object sender, EventArgs args)
        {
            FileHandler.SaveProject(CurrentProject);
        }
        private void MenuSettingsProjectClick(object sender, EventArgs e)
        {

        }
        private void MenuSettingsSimulationClick(object sender, EventArgs e)
        {

        }
        private void MenuSettingsDestinationsClick(object sender, EventArgs e)
        {

        }
        private void MenuSettingsVehiclesClick(object sender, EventArgs e)
        {

        }

        // MainToolStrip Events
        private void ToolAddNodeClick(object sender, EventArgs args) { ToolController.ToggleTool(ToolAddNode); }
        private void ToolRemoveNodeClick(object sender, EventArgs args) { ToolController.ToggleTool(ToolRemoveNode); }
        private void ToolAddRoadClick(object sender, EventArgs args) { ToolController.ToggleTool(ToolAddRoad); }
        private void ToolPrimaryRoadClick(object sender, EventArgs args) { ToolController.ToggleTool(ToolPrimaryRoad); }
        private void ToolSecondaryRoadClick(object sender, EventArgs args) { ToolController.ToggleTool(ToolSecondaryRoad); }
        private void ToolEditClick(object sender, EventArgs args) { ToolController.ToggleTool(ToolEdit); }
        private void SetNodeTrafficLightClick(object sender, EventArgs args) { ToolController.ToggleTool(ToolSetNodeLight); }
        private void SetNodeYieldClick(object sender, EventArgs args) { ToolController.ToggleTool(ToolSetNodeYield); }
        private void SetNodeHomeClick(object sender, EventArgs args) { ToolController.ToggleTool(ToolSetNodeHome); }
        private void SetNodeParkingClick(object sender, EventArgs args) { ToolController.ToggleTool(ToolSetNodeParking); }

        // Destination and Light Controller - Not finished
        private void ToolAddDestinationClick(object sender, EventArgs args) { ToolController.ToggleTool(ToolAddDestination); }
        private void ToolAddLightControllerClick(object sender, EventArgs args) { ToolController.ToggleTool(ToolAddLightController); }
        

        // Update GUI Methods
        public void UpdateTitle() { Text = "A319TS - " + CurrentProject.Name; }
        public void UpdateStatusNodes() { StatusNodes.Text = CurrentProject.Nodes.Count.ToString(); }
        public void UpdateStatusZoom() { StatusZoom.Text = GUIMainViewport.Zoom * 100 + "%"; }
        public void UpdateAll()
        {
            UpdateTitle();
            UpdateStatusNodes();
            UpdateStatusZoom();
            GUIMainViewport.Refresh();
        }
    }
}
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

        // MainToolStrip Events
        private void ToolAddNodeClick(object sender, EventArgs args) { ToolController.ToggleTool(ToolAddNode); }
        private void ToolRemoveNodeClick(object sender, EventArgs args) { ToolController.ToggleTool(ToolRemoveNode); }
        private void ToolAddRoadClick(object sender, EventArgs args) { ToolController.ToggleTool(ToolAddRoad); }
        private void ToolPrimaryRoadClick(object sender, EventArgs args) { ToolController.ToggleTool(ToolPrimaryRoad); }
        private void ToolSecondaryRoadClick(object sender, EventArgs args) { ToolController.ToggleTool(ToolSecondaryRoad); }
        private void ToolEditClick(object sender, EventArgs args) { ToolController.ToggleTool(ToolEdit); }

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

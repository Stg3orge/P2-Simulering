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
            KeyDown += ToolController.StopConnection;
        }

        // MainMenuStrip Events
        private void MenuFileNewClick(object sender, EventArgs args)
        {
            Project project = FileHandler.NewProject();
            if (project != null)
            {
                CurrentProject = project;
                ToolController.Project = project;
                GUIMainViewport.Project = project;
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
                ToolController.Project = project;
                GUIMainViewport.Project = project;
                GUIMainViewport.Reset();
            }
        }
        private void MenuFileSaveClick(object sender, EventArgs args)
        {
            FileHandler.SaveProject(CurrentProject);
        }
        private void MenuSettingsProjectClick(object sender, EventArgs args)
        {
            GUIMenuSettingsProject project = new GUIMenuSettingsProject(CurrentProject);
            project.ShowDialog();
            UpdateTitle();
        }
        private void MenuSettingsSimulationClick(object sender, EventArgs args)
        {

        }
        private void MenuSettingsDestinationsClick(object sender, EventArgs args)
        {
            GUIMenuSettingsDestination dest = new GUIMenuSettingsDestination(CurrentProject);
            dest.ShowDialog();
            ToolDestinationTypeSelect.ComboBox.Show();
        }
        private void MenuSettingsVehiclesClick(object sender, EventArgs args)
        {

        }

        // MainToolStrip Events
        private void ToolClick(object sender, EventArgs args)
        {
            ToolController.ToggleTool(sender as ToolStripButton);
        }

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
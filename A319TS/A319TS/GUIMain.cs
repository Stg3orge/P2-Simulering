using System;
using System.Windows.Forms;
using System.ComponentModel;

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
        // FILE MENU
        private void MenuFileNewClick(object sender, EventArgs args)
        {
            Project project = FileHandler.NewProject();
            if (project != null)
                UpdateProject(project);
        }
        private void MenuFileOpenClick(object sender, EventArgs args)
        {
            Project project = FileHandler.OpenProject();
            if (project != null)
                UpdateProject(project);
        }
        private void MenuFileSaveClick(object sender, EventArgs args)
        {
            FileHandler.SaveProject(CurrentProject);
        }
        // SETTINGS MENU
        private void MenuSettingsProjectClick(object sender, EventArgs args)
        {
            GUIMenuSettingsProject project = new GUIMenuSettingsProject(CurrentProject);
            project.ShowDialog();
            UpdateTitle();
        }
        private void MenuSettingsSimulationClick(object sender, EventArgs args)
        {
            GUIMenuSettingsSimulation simulation = new GUIMenuSettingsSimulation(CurrentProject);
            simulation.ShowDialog();
        }
        
        private void MenuSettingsDistributionClick(object sender, EventArgs args)
        {
            GUIMenuSettingsDistribution distribution = new GUIMenuSettingsDistribution(CurrentProject);
            distribution.ShowDialog();
        }
        private void MenuSettingsDistribution2Click(object sender, EventArgs args)
        {
            GUIMenuSettingsDistribution2 distribution = new GUIMenuSettingsDistribution2(CurrentProject);
            distribution.ShowDialog();
        }
        // TYPES MENU
        private void MenuTypesDestinationsClick(object sender, EventArgs args)
        {
            GUIMenuTypesDestinations dest = new GUIMenuTypesDestinations(CurrentProject);
            dest.ShowDialog();
            ToolDestinationTypeSelect.ComboBox.DataSource = new BindingSource(new BindingList<DestinationType>(CurrentProject.DestinationTypes), null);
        }
        private void MenuTypesVehiclesClick(object sender, EventArgs args)
        {
            GUIMenuTypesVehicles project = new GUIMenuTypesVehicles(CurrentProject);
            project.ShowDialog();
        }
        private void MenuTypesRoadsClick(object sender, EventArgs args)
        {
            GUIMenuTypesRoads road = new GUIMenuTypesRoads(CurrentProject);
            road.ShowDialog();
            ToolRoadTypeSelect.ComboBox.DataSource = new BindingSource(new BindingList<RoadType>(CurrentProject.RoadTypes), null);
        }
        // SIMULATION MENU
        private void MenuSimulationRunClick(object sender, EventArgs args)
        {

        }
        private void MenuSimulationViewClick(object sender, EventArgs args)
        {

        }

        // MainToolStrip Events
        private void ToolClick(object sender, EventArgs args)
        {
            ToolController.ToggleTool(sender as ToolStripButton);
        }

        // Update Methods
        public void UpdateTitle() { Text = "A319TS - " + CurrentProject.Name; }
        public void UpdateProject(Project project)
        {
            CurrentProject = project;
            ToolController.Project = project;
            GUIMainViewport.Project = project;
            GUIMainViewport.Reset();
            UpdateTitle();
        }
    }
}
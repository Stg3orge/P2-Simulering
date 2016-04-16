using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace A319TS
{
    partial class GUIMain : Form
    {
        private Project _currentProject;

        public GUIMain()
        {
            _currentProject = new Project("Unnamed Project");
            InitializeGUIMain();
        }

        // MainMenuStrip Events
        private void MenuFileNewClick(object sender, EventArgs args)
        {
            Project project = FileHandler.NewProject();
            if (project != null)
            {
                _currentProject = project;
                GUIMainViewport.Reset();
                UpdateTitle();
            }
        }
        private void MenuFileOpenClick(object sender, EventArgs args)
        {
            Project project = FileHandler.OpenProject();
            if (project != null)
            {
                _currentProject = project;
                GUIMainViewport.Reset();
            }
        }
        private void MenuFileSaveClick(object sender, EventArgs args)
        {
            FileHandler.SaveProject(_currentProject);
        }
        private void MenuSettingsProjectClick(object sender, EventArgs e) {}

        // MainToolStrip Events
        private void ToolAddNodeClick(object sender, EventArgs args) { ToggleTool(ToolAddNode); }
        private void ToolRemoveNodeClick(object sender, EventArgs args) { ToggleTool(ToolRemoveNode); }
        private void ToolAddRoadClick(object sender, EventArgs args) { ToggleTool(ToolAddRoad); }

        // MainToolStrip Methods
        private void ToggleTool(ToolStripButton ToggledTool)
        {
            foreach (ToolStripButton Tool in GUIMainToolStrip.Items.OfType<ToolStripButton>())
            {
                if (ToggledTool.Checked)
                    Tool.Checked = false;
                else if (Tool == ToggledTool)
                    Tool.Checked = true;
                else
                    Tool.Checked = false;
            }
        }

        // MainViewport Events
        private void GUIMainViewportMove(object sender, MouseEventArgs args) {}
        private void GUIMainViewportWheel(object sender, MouseEventArgs args) {}
        private void GUIMainViewportClick(object sender, MouseEventArgs args) {}

        // Update GUI Methods
        private void UpdateTitle() { Text = "A319Sim - " + _currentProject.Name; }
        private void UpdateStatusVertices() { StatusNodes.Text = _currentProject.Nodes.Count.ToString(); }
        private void UpdateStatusZoom() { StatusZoom.Text = GUIMainViewport.Zoom + "%"; }
        private void UpdateAll()
        {
            UpdateTitle();
            UpdateStatusVertices();
            UpdateStatusZoom();
            GUIMainViewport.Update();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace A319TS
{
    partial class GUIMain : Form
    {
        public Project CurrentProject;

        public GUIMain()
        {
            CurrentProject = new Project("Unnamed Project");
            CurrentProject.Nodes.Add(new Node(new Point(6, 6)));
            InitGUIMain();
        }

        // MainMenuStrip Events
        private void MenuFileNewClick(object sender, EventArgs args)
        {
            Project project = FileHandler.NewProject();
            if (project != null)
            {
                CurrentProject = project;
                GUIMainViewport.Reset();
                UpdateTitle();
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
        private void ToolAddNodeClick(object sender, EventArgs args) { ToggleTool(ToolAddNode); }
        private void ToolRemoveNodeClick(object sender, EventArgs args) { ToggleTool(ToolRemoveNode); }
        private void ToolAddRoadClick(object sender, EventArgs args) { ToggleTool(ToolAddRoad); }

        // MainToolStrip Methods
        private void ToggleTool(ToolStripButton clickedTool)
        {
            if (clickedTool.Checked)
                clickedTool.Checked = false;
            else
            {
                foreach (ToolStripButton tool in GUIMainToolStrip.Items.OfType<ToolStripButton>())
                {
                    if (tool == clickedTool)
                        tool.Checked = true;
                    else
                        tool.Checked = false;
                }
            }
        }

        // Viewport Input
        private void ViewportClick(object sender, MouseEventArgs args)
        {
            if (args.Button == MouseButtons.Left)
            {
                if (ToolAddNode.Checked)
                {
                    if (CurrentProject.Nodes.Find(n => n.Position == GUIMainViewport.GridPos) == null)
                    {
                        CurrentProject.Nodes.Add(new Node(GUIMainViewport.GridPos));
                        GUIMainViewport.Nodes.Refresh();
                        UpdateStatusNodes();
                    }
                }
                else if (ToolRemoveNode.Checked)
                {

                }
                else if (ToolAddRoad.Checked)
                {

                }
            }
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

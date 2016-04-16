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
        private void InitializeGUIMain()
        {
            Text = "A319Sim"; // Window Title
            Size = new Size(700, 350);
            MinimumSize = new Size(300, 150);
            Icon = Resources.P2;

            InitializeGUIMainStatusStrip();
            InitializeGUIMainViewport();
            InitializeGUIMainToolStrip();
            InitializeGUIMainMenuStrip();

            Controls.Add(GUIMainStatusStrip);
            Controls.Add(GUIMainViewport);
            Controls.Add(GUIMainToolStrip);
            Controls.Add(GUIMainMenuStrip);
        }

        // MainMenuStrip
        private void InitializeGUIMainMenuStrip()
        {
            GUIMainMenuStrip = new MenuStrip();
            GUIMainMenuStrip.BackColor = SystemColors.Control;

            MenuFile = new ToolStripMenuItem();
            MenuFile.Text = "File";

                MenuFileNew = new ToolStripMenuItem();
                MenuFileNew.Text = "New";
                MenuFileNew.Click += MenuFileNewClick;
                MenuFileOpen = new ToolStripMenuItem();
                MenuFileOpen.Text = "Open";
                MenuFileOpen.Click += MenuFileOpenClick;
                MenuFileSave = new ToolStripMenuItem();
                MenuFileSave.Text = "Save";
                MenuFileSave.Click += MenuFileSaveClick;

            MenuSettings = new ToolStripMenuItem();
            MenuSettings.Text = "Settings";

                MenuSettingsProject = new ToolStripMenuItem();
                MenuSettingsProject.Text = "Project";
                MenuSettingsProject.Click += MenuSettingsProjectClick;

            MenuFile.DropDownItems.Add(MenuFileNew);
            MenuFile.DropDownItems.Add(MenuFileOpen);
            MenuFile.DropDownItems.Add(MenuFileSave);
            MenuSettings.DropDownItems.Add(MenuSettingsProject);
            GUIMainMenuStrip.Items.Add(MenuFile);
            GUIMainMenuStrip.Items.Add(MenuSettings);
        }
        private MenuStrip GUIMainMenuStrip;
        private ToolStripMenuItem MenuFile;
        private ToolStripMenuItem MenuFileNew;
        private ToolStripMenuItem MenuFileOpen;
        private ToolStripMenuItem MenuFileSave;
        private ToolStripMenuItem MenuSettings;
        private ToolStripMenuItem MenuSettingsProject;

        // MainToolStrip
        private void InitializeGUIMainToolStrip()
        {
            GUIMainToolStrip = new ToolStrip();
            GUIMainToolStrip.BackColor = SystemColors.ControlLight;
            GUIMainToolStrip.GripStyle = ToolStripGripStyle.Hidden;
            GUIMainToolStrip.Padding = new Padding(12, 2, 0, 0);

            ToolAddNode = new ToolStripButton();
            ToolAddNode.ToolTipText = "Add Node";
            ToolAddNode.Image = Resources.ToolAddNode;
            ToolAddNode.Click += ToolAddNodeClick;

            ToolRemoveNode = new ToolStripButton();
            ToolRemoveNode.ToolTipText = "Remove Node";
            ToolRemoveNode.Image = Resources.ToolRemoveNode;
            ToolRemoveNode.Click += ToolRemoveNodeClick;

            ToolAddRoad = new ToolStripButton();
            ToolAddRoad.ToolTipText = "Add Edge";
            ToolAddRoad.Image = Resources.ToolAddRoad;
            ToolAddRoad.Click += ToolAddRoadClick;

            GUIMainToolStrip.Items.Add(ToolAddNode);
            GUIMainToolStrip.Items.Add(ToolRemoveNode);
            GUIMainToolStrip.Items.Add(ToolAddRoad);
        }
        private ToolStrip GUIMainToolStrip;
        private ToolStripButton ToolAddNode;
        private ToolStripButton ToolRemoveNode;
        private ToolStripButton ToolAddRoad;


        // MainPanel
        private void InitializeGUIMainViewport()
        {
            GUIMainViewport = new Viewport(_currentProject);
            GUIMainViewport.Dock = DockStyle.Fill;
            GUIMainViewport.BorderStyle = BorderStyle.Fixed3D;
            GUIMainViewport.MouseClick += GUIMainViewportClick;
            GUIMainViewport.MouseMove += GUIMainViewportMove;
            GUIMainViewport.MouseWheel += GUIMainViewportWheel;
        }
        private Viewport GUIMainViewport;

        // MainStatusStrip
        private void InitializeGUIMainStatusStrip()
        {
            GUIMainStatusStrip = new StatusStrip();
            GUIMainStatusStrip.BackColor = SystemColors.ControlLight;

            StatusZoomLabel = new ToolStripStatusLabel();
            StatusZoomLabel.Text = "Zoom:";
            StatusZoom = new ToolStripStatusLabel();
            StatusZoom.Text = "100%";

            StatusNodesLabel = new ToolStripStatusLabel();
            StatusNodesLabel.Text = "Nodes:";
            StatusNodes = new ToolStripStatusLabel();
            StatusNodes.Text = "0";

            GUIMainStatusStrip.Items.Add(StatusZoomLabel);
            GUIMainStatusStrip.Items.Add(StatusZoom);
            GUIMainStatusStrip.Items.Add(StatusNodesLabel);
            GUIMainStatusStrip.Items.Add(StatusNodes);
        }
        private StatusStrip GUIMainStatusStrip;
        private ToolStripStatusLabel StatusZoomLabel;
        private ToolStripStatusLabel StatusZoom;
        private ToolStripStatusLabel StatusNodesLabel;
        private ToolStripStatusLabel StatusNodes;
    }
}

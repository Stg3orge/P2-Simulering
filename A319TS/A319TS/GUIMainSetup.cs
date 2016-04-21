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
        private void InitGUIMain()
        {
            Text = "A319TS - " + CurrentProject.Name; // Window Title
            Size = new Size(700, 350);
            MinimumSize = new Size(300, 150);
            Icon = Resources.P2;

            InitGUIMainStatusStrip();
            InitGUIMainViewport();
            InitGUIMainToolStrip();
            InitGUIMainMenuStrip();

            Controls.Add(GUIMainStatusStrip);
            Controls.Add(GUIMainViewport);
            Controls.Add(GUIMainToolStrip);
            Controls.Add(GUIMainMenuStrip);
        }

        // MainMenuStrip
        private void InitGUIMainMenuStrip()
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
        private void InitGUIMainToolStrip()
        {
            GUIMainToolStrip = new ToolStrip();
            GUIMainToolStrip.BackColor = SystemColors.ControlLight;
            GUIMainToolStrip.GripStyle = ToolStripGripStyle.Hidden;
            GUIMainToolStrip.Padding = new Padding(12, 2, 0, 0);

            ToolAddNode = new ToolStripButton();
            ToolAddNode.Name = "ToolAddNode";
            ToolAddNode.ToolTipText = "Add Node";
            ToolAddNode.Image = Resources.ToolAddNode;
            ToolAddNode.Click += ToolAddNodeClick;

            ToolRemoveNode = new ToolStripButton();
            ToolRemoveNode.Name = "ToolRemoveNode";
            ToolRemoveNode.ToolTipText = "Remove Node";
            ToolRemoveNode.Image = Resources.ToolRemoveNode;
            ToolRemoveNode.Click += ToolRemoveNodeClick;

            ToolAddRoad = new ToolStripButton();
            ToolAddRoad.Name = "ToolAddRoad";
            ToolAddRoad.ToolTipText = "Add Road";
            ToolAddRoad.Image = Resources.ToolAddRoad;
            ToolAddRoad.Click += ToolAddRoadClick;

            ToolPrimaryRoad = new ToolStripButton();
            ToolPrimaryRoad.Name = "ToolPrimaryRoad";
            ToolPrimaryRoad.ToolTipText = "Add Primary Road";
            ToolPrimaryRoad.Image = Resources.ToolAddRoad;
            ToolPrimaryRoad.Click += ToolPrimaryRoadClick;

            ToolSecondaryRoad = new ToolStripButton();
            ToolSecondaryRoad.Name = "ToolSecondaryRoad";
            ToolSecondaryRoad.ToolTipText = "Add Secondary Road";
            ToolSecondaryRoad.Image = Resources.ToolAddRoad;
            ToolSecondaryRoad.Click += ToolSecondaryRoadClick;

            ToolRoadTypeSelect = new ToolStripDropDownButton();
            ToolRoadTypeSelect.Name = "ToolRoadTypeSelect";

            ToolEdit = new ToolStripButton();
            ToolEdit.Name = "Edit";
            ToolEdit.ToolTipText = "Edit";
            ToolEdit.Image = Resources.ToolToggleRoad;
            ToolEdit.Click += ToolEditClick;

            SetNodeTrafficLight = new ToolStripButton();
            SetNodeTrafficLight.Name = "SetNodeTrafficLight";
            SetNodeTrafficLight.ToolTipText = "Set node = Traffic Light";
            SetNodeTrafficLight.Image = Resources.ToolAddNode;
            SetNodeTrafficLight.Click += SetNodeTrafficLightClick;

            SetNodeYield = new ToolStripButton();
            SetNodeYield.Name = "SetNodeYield";
            SetNodeYield.ToolTipText = "Set node = Yield";
            SetNodeYield.Image = Resources.ToolAddNode;
            SetNodeYield.Click += SetNodeYieldClick;

            SetNodeHome = new ToolStripButton();
            SetNodeHome.Name = "SetNodeHome";
            SetNodeHome.ToolTipText = "Set node = Home";
            SetNodeHome.Image = Resources.ToolAddNode;
            SetNodeHome.Click += SetNodeHomeClick;

            SetNodeParking = new ToolStripButton();
            SetNodeParking.Name = "SetNodeParking";
            SetNodeParking.ToolTipText = "Set node = Parking";
            SetNodeParking.Image = Resources.ToolAddNode;
            SetNodeParking.Click += SetNodeParkingClick;

            GUIMainToolStrip.Items.Add(ToolAddNode);
            GUIMainToolStrip.Items.Add(ToolRemoveNode);
            GUIMainToolStrip.Items.Add(new ToolStripSeparator());
            GUIMainToolStrip.Items.Add(ToolAddRoad);
            GUIMainToolStrip.Items.Add(ToolPrimaryRoad);
            GUIMainToolStrip.Items.Add(ToolSecondaryRoad);
            GUIMainToolStrip.Items.Add(ToolRoadTypeSelect);
            GUIMainToolStrip.Items.Add(new ToolStripSeparator());
            GUIMainToolStrip.Items.Add(ToolEdit);
            GUIMainToolStrip.Items.Add(SetNodeTrafficLight);
            GUIMainToolStrip.Items.Add(SetNodeYield);
            GUIMainToolStrip.Items.Add(SetNodeHome);
            GUIMainToolStrip.Items.Add(SetNodeParking);

        }
        public ToolStrip GUIMainToolStrip;
        public ToolStripButton ToolAddNode;
        public ToolStripButton ToolRemoveNode;
        public ToolStripButton ToolAddRoad;
        public ToolStripButton ToolPrimaryRoad;
        public ToolStripButton ToolSecondaryRoad;
        public ToolStripDropDownButton ToolRoadTypeSelect;
        public ToolStripButton ToolEdit;
        public ToolStripButton SetNodeTrafficLight;
        public ToolStripButton SetNodeYield;
        public ToolStripButton SetNodeHome;
        public ToolStripButton SetNodeParking;

        // MainPanel
        private void InitGUIMainViewport()
        {
            GUIMainViewport = new Viewport(CurrentProject);
            GUIMainViewport.Dock = DockStyle.Fill;
            GUIMainViewport.BorderStyle = BorderStyle.Fixed3D;
            GUIMainViewport.Padding = new Padding(0, 0, 0, 20);
        }
        private Viewport GUIMainViewport;

        // MainStatusStrip
        private void InitGUIMainStatusStrip()
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
        public ToolStripStatusLabel StatusZoom;
        private ToolStripStatusLabel StatusNodesLabel;
        public ToolStripStatusLabel StatusNodes;
    }
}

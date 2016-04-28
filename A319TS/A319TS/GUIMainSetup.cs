using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

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

                MenuSettingsSimulation = new ToolStripMenuItem();
                MenuSettingsSimulation.Text = "Simulation";
                MenuSettingsSimulation.Click += MenuSettingsSimulationClick;

                MenuSettingsDestinations = new ToolStripMenuItem();
                MenuSettingsDestinations.Text = "Destinations";
                MenuSettingsDestinations.Click += MenuSettingsDestinationsClick;

                MenuSettingsVehicles = new ToolStripMenuItem();
                MenuSettingsVehicles.Text = "Vehicles";
                MenuSettingsVehicles.Click += MenuSettingsVehiclesClick;

                MenuSettingsRoads = new ToolStripMenuItem();
                MenuSettingsRoads.Text = "Roads";
                MenuSettingsRoads.Click += MenuSettingsRoadsClick;

            MenuFile.DropDownItems.Add(MenuFileNew);
            MenuFile.DropDownItems.Add(MenuFileOpen);
            MenuFile.DropDownItems.Add(MenuFileSave);
            MenuSettings.DropDownItems.Add(MenuSettingsProject);
            MenuSettings.DropDownItems.Add(MenuSettingsSimulation);   // addet
            MenuSettings.DropDownItems.Add(MenuSettingsDestinations);   // addet
            MenuSettings.DropDownItems.Add(MenuSettingsVehicles);   // addet
            MenuSettings.DropDownItems.Add(MenuSettingsRoads);   // addet
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
        private ToolStripMenuItem MenuSettingsSimulation;
        private ToolStripMenuItem MenuSettingsDestinations;
        private ToolStripMenuItem MenuSettingsVehicles;
        private ToolStripMenuItem MenuSettingsRoads;

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
            ToolAddNode.Click += ToolClick;

            ToolSetNodeLight = new ToolStripButton();
            ToolSetNodeLight.Name = "ToolSetNodeLight";
            ToolSetNodeLight.ToolTipText = "Set Node Light";
            ToolSetNodeLight.Image = Resources.ToolSetNodeLight;
            ToolSetNodeLight.Click += ToolClick;

            ToolSetNodeYield = new ToolStripButton();
            ToolSetNodeYield.Name = "ToolSetNodeYield";
            ToolSetNodeYield.ToolTipText = "Set Node Yield";
            ToolSetNodeYield.Image = Resources.ToolSetNodeYield;
            ToolSetNodeYield.Click += ToolClick;

            ToolSetNodeHome = new ToolStripButton();
            ToolSetNodeHome.Name = "ToolSetNodeHome";
            ToolSetNodeHome.ToolTipText = "Set Node Home";
            ToolSetNodeHome.Image = Resources.ToolSetNodeHome;
            ToolSetNodeHome.Click += ToolClick;

            ToolSetNodeParking = new ToolStripButton();
            ToolSetNodeParking.Name = "ToolSetNodeParking";
            ToolSetNodeParking.ToolTipText = "Set Node Parking";
            ToolSetNodeParking.Image = Resources.ToolSetNodeParking;
            ToolSetNodeParking.Click += ToolClick;

            ToolSetNodeInbound = new ToolStripButton();
            ToolSetNodeInbound.Name = "ToolSetNodeInbound";
            ToolSetNodeInbound.ToolTipText = "Set Node Inbound";
            ToolSetNodeInbound.Image = Resources.ToolSetNodeInbound;
            ToolSetNodeInbound.Click += ToolClick;

            ToolSetNodeOutbound = new ToolStripButton();
            ToolSetNodeOutbound.Name = "ToolSetNodeOutbound";
            ToolSetNodeOutbound.ToolTipText = "Set Node Outbound";
            ToolSetNodeOutbound.Image = Resources.ToolSetNodeOutbound;
            ToolSetNodeOutbound.Click += ToolClick;

            ToolAddLightController = new ToolStripButton();
            ToolAddLightController.Name = "ToolAddLightController";
            ToolAddLightController.ToolTipText = "Add Light Controller";
            ToolAddLightController.Image = Resources.ToolAddLightController;
            ToolAddLightController.Click += ToolClick;

            ToolLinkLight = new ToolStripButton();
            ToolLinkLight.Name = "ToolLinkLight";
            ToolLinkLight.ToolTipText = "Link Lights to Controller";
            ToolLinkLight.Image = Resources.ToolLinkLight;
            ToolLinkLight.Click += ToolClick;

            ToolAddDestination = new ToolStripButton();
            ToolAddDestination.Name = "ToolAddDestination";
            ToolAddDestination.ToolTipText = "Add Destination";
            ToolAddDestination.Image = Resources.ToolAddDestination;
            ToolAddDestination.Click += ToolClick;

            ToolDestinationTypeSelect = new ToolStripComboBox();
            ToolDestinationTypeSelect.Name = "ToolDestinationTypeSelect";
            ToolDestinationTypeSelect.FlatStyle = FlatStyle.Standard;
            ToolDestinationTypeSelect.DropDownStyle = ComboBoxStyle.DropDownList;
            ToolDestinationTypeSelect.ComboBox.DataSource = new BindingSource(new BindingList<DestinationType>(CurrentProject.DestinationTypes), null);

            ToolAddRoad = new ToolStripButton();
            ToolAddRoad.Name = "ToolAddRoad";
            ToolAddRoad.ToolTipText = "Add Road";
            ToolAddRoad.Image = Resources.ToolAddRoad;
            ToolAddRoad.Click += ToolClick;

            ToolPrimaryRoad = new ToolStripButton();
            ToolPrimaryRoad.Name = "ToolPrimaryRoad";
            ToolPrimaryRoad.ToolTipText = "Add Primary Road";
            ToolPrimaryRoad.Image = Resources.ToolAddPrimary;
            ToolPrimaryRoad.Click += ToolClick;

            ToolSecondaryRoad = new ToolStripButton();
            ToolSecondaryRoad.Name = "ToolSecondaryRoad";
            ToolSecondaryRoad.ToolTipText = "Add Secondary Road";
            ToolSecondaryRoad.Image = Resources.ToolAddSecondary;
            ToolSecondaryRoad.Click += ToolClick;

            ToolRoadTypeSelect = new ToolStripComboBox();
            ToolRoadTypeSelect.Name = "ToolRoadTypeSelect";
            ToolRoadTypeSelect.FlatStyle = FlatStyle.Standard;
            ToolRoadTypeSelect.DropDownStyle = ComboBoxStyle.DropDownList;
            ToolRoadTypeSelect.ComboBox.DataSource = CurrentProject.RoadTypes;

            ToolEdit = new ToolStripButton();
            ToolEdit.Name = "ToolEdit";
            ToolEdit.ToolTipText = "Edit";
            ToolEdit.Image = Resources.ToolEdit;
            ToolEdit.Click += ToolClick;

            ToolRemove = new ToolStripButton();
            ToolRemove.Name = "ToolRemove";
            ToolRemove.ToolTipText = "Remove";
            ToolRemove.Image = Resources.ToolRemove;
            ToolRemove.Click += ToolClick;

            ToolMove = new ToolStripButton();
            ToolMove.Name = "ToolMoveObject";
            ToolMove.ToolTipText = "Move Object";
            ToolMove.Image = Resources.ToolMove;
            ToolMove.Click += ToolClick;
            
            GUIMainToolStrip.Items.Add(ToolAddNode);
            GUIMainToolStrip.Items.Add(ToolSetNodeLight);
            GUIMainToolStrip.Items.Add(ToolSetNodeYield);
            GUIMainToolStrip.Items.Add(ToolSetNodeHome);
            GUIMainToolStrip.Items.Add(ToolSetNodeParking);
            GUIMainToolStrip.Items.Add(ToolSetNodeInbound);
            GUIMainToolStrip.Items.Add(ToolSetNodeOutbound);
            GUIMainToolStrip.Items.Add(new ToolStripSeparator());
            GUIMainToolStrip.Items.Add(ToolAddLightController);
            GUIMainToolStrip.Items.Add(ToolLinkLight);
            GUIMainToolStrip.Items.Add(ToolAddDestination);
            GUIMainToolStrip.Items.Add(ToolDestinationTypeSelect);
            GUIMainToolStrip.Items.Add(new ToolStripSeparator());
            GUIMainToolStrip.Items.Add(ToolAddRoad);
            GUIMainToolStrip.Items.Add(ToolPrimaryRoad);
            GUIMainToolStrip.Items.Add(ToolSecondaryRoad);
            GUIMainToolStrip.Items.Add(ToolRoadTypeSelect);
            GUIMainToolStrip.Items.Add(new ToolStripSeparator());
            GUIMainToolStrip.Items.Add(ToolEdit);
            GUIMainToolStrip.Items.Add(ToolRemove);
            GUIMainToolStrip.Items.Add(ToolMove);
        }
        public ToolStrip GUIMainToolStrip;
        public ToolStripButton ToolAddNode;
        public ToolStripButton ToolSetNodeLight;
        public ToolStripButton ToolSetNodeYield;
        public ToolStripButton ToolSetNodeHome;
        public ToolStripButton ToolSetNodeParking;
        public ToolStripButton ToolSetNodeInbound;
        public ToolStripButton ToolSetNodeOutbound;
        public ToolStripButton ToolAddLightController;
        public ToolStripButton ToolLinkLight;
        public ToolStripButton ToolAddDestination;
        public ToolStripComboBox ToolDestinationTypeSelect;
        public ToolStripButton ToolAddRoad;
        public ToolStripButton ToolPrimaryRoad;
        public ToolStripButton ToolSecondaryRoad;
        public ToolStripComboBox ToolRoadTypeSelect;
        public ToolStripButton ToolEdit;
        public ToolStripButton ToolRemove;
        public ToolStripButton ToolMove;

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

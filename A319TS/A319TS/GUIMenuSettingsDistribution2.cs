using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace A319TS
{
    class GUIMenuSettingsDistribution2 : Form
    {
        private Project Project;
        private TabControl TabContainer;
        private TabPage TabDestinations;
        private TabPage TabVehicles;
        private DataGridView Destinations;
        private DataGridView Vehicles;
        private Label DestinationsLabel;
        private Label VehiclesLabel;
        private TextBox DestinationsPercent;
        private TextBox VehiclesPercent;
        private Button Save;

        public GUIMenuSettingsDistribution2(Project project)
        {
            Project = project;
            Setup();
        }

        private void Setup()
        {
            Text = "Distribution";
            Size = new Size(368, 400);
            MinimumSize = new Size(368, 400);
            MaximumSize = new Size(368, 400);
            ShowIcon = false;
            MinimizeBox = false;
            MaximizeBox = false;
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterParent;

            TabContainer = new TabControl();
            TabContainer.Location = new Point(13, 13);
            TabContainer.Size = new Size(323, 301);
            Controls.Add(TabContainer);

            TabDestinations = new TabPage();
            TabDestinations.Text = "Destinations";
            TabContainer.TabPages.Add(TabDestinations);

            TabVehicles = new TabPage();
            TabVehicles.Text = "Vehicles";
            TabContainer.TabPages.Add(TabVehicles);

            Destinations = new DataGridView();
            Destinations.Dock = DockStyle.Fill;
            TabDestinations.Controls.Add(Destinations);

            Vehicles = new DataGridView();
            Vehicles.Dock = DockStyle.Fill;
            TabVehicles.Controls.Add(Vehicles);
            
            DestinationsPercent = new TextBox();
            DestinationsPercent.Text = "100%";
            DestinationsPercent.Location = new Point(102, 321);
            DestinationsPercent.Size = new Size(40, 22);
            DestinationsPercent.Enabled = false;
            DestinationsPercent.ReadOnly = true;
            Controls.Add(DestinationsPercent);

            VehiclesPercent = new TextBox();
            VehiclesPercent.Text = "100%";
            VehiclesPercent.Location = new Point(215, 321);
            VehiclesPercent.Size = new Size(40, 22);
            VehiclesPercent.Enabled = false;
            VehiclesPercent.ReadOnly = true;
            Controls.Add(VehiclesPercent);

            Save = new Button();
            Save.Text = "Save";
            Save.Location = new Point(261, 320);
            Save.Size = new Size(75, 23);
            Save.Click += SaveData;
            Controls.Add(Save);

            DestinationsLabel = new Label();
            DestinationsLabel.Text = "Destinations";
            DestinationsLabel.Location = new Point(10, 324);
            Controls.Add(DestinationsLabel);

            VehiclesLabel = new Label();
            VehiclesLabel.Text = "Vehicles";
            VehiclesLabel.Location = new Point(148, 324);
            Controls.Add(VehiclesLabel);
        }
        private void SaveData(object sender, EventArgs args)
        {
            Close();
        }
    }
}

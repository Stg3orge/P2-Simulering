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
    class GUIMenuSettingsDistribution : Form
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

        public GUIMenuSettingsDistribution(Project project)
        {
            Project = project;
            Setup();
            Load += ReadData;
            FormClosing += OnClosing;
        }

        private void Setup()
        {
            Text = "Distribution";
            Size = new Size(378, 350);
            MinimumSize = new Size(378, 350);
            MaximumSize = new Size(378, 350);
            ShowIcon = false;
            MinimizeBox = false;
            MaximizeBox = false;
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterParent;

            TabContainer = new TabControl();
            TabContainer.Location = new Point(12, 12);
            TabContainer.Size = new Size(336, 252);
            TabContainer.SelectedIndexChanged += OnSelectedChanged;
            Controls.Add(TabContainer);

            TabDestinations = new TabPage();
            TabDestinations.Text = "Destinations";
            TabContainer.TabPages.Add(TabDestinations);

            TabVehicles = new TabPage();
            TabVehicles.Text = "Vehicles";
            TabContainer.TabPages.Add(TabVehicles);

            Destinations = new DataGridView();
            Destinations.Dock = DockStyle.Fill;
            Destinations.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            Destinations.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Destinations.AllowUserToResizeColumns = false;
            Destinations.AllowUserToResizeRows = false;
            Destinations.RowHeadersVisible = false;
            Destinations.EditingControlShowing += OnEditingControlShowing;
            Destinations.CellEndEdit += UpdatePercentage;
            TabDestinations.Controls.Add(Destinations);
            Destinations.Show();

            Vehicles = new DataGridView();
            Vehicles.Dock = DockStyle.Fill;
            Vehicles.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            Vehicles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Vehicles.AllowUserToResizeColumns = false;
            Vehicles.AllowUserToResizeRows = false;
            Vehicles.RowHeadersVisible = false;
            Vehicles.EditingControlShowing += OnEditingControlShowing;
            Vehicles.CellEndEdit += UpdatePercentage;
            TabVehicles.Controls.Add(Vehicles);
            Vehicles.Show();

            DestinationsPercent = new TextBox();
            DestinationsPercent.Text = "0%";
            DestinationsPercent.Location = new Point(104, 270);
            DestinationsPercent.Size = new Size(45, 22);
            DestinationsPercent.Enabled = false;
            DestinationsPercent.ReadOnly = true;
            Controls.Add(DestinationsPercent);

            VehiclesPercent = new TextBox();
            VehiclesPercent.Text = "0%";
            VehiclesPercent.Location = new Point(222, 270);
            VehiclesPercent.Size = new Size(45, 22);
            VehiclesPercent.Enabled = false;
            VehiclesPercent.ReadOnly = true;
            Controls.Add(VehiclesPercent);

            Save = new Button();
            Save.Text = "Save";
            Save.Location = new Point(273, 270);
            Save.Size = new Size(75, 23);
            Save.Click += SaveData;
            Controls.Add(Save);

            DestinationsLabel = new Label();
            DestinationsLabel.Text = "Destinations";
            DestinationsLabel.Location = new Point(12, 273);
            Controls.Add(DestinationsLabel);

            VehiclesLabel = new Label();
            VehiclesLabel.Text = "Vehicles";
            VehiclesLabel.Location = new Point(155, 273);
            Controls.Add(VehiclesLabel);
        }
        private void OnSelectedChanged(object sender, EventArgs args)
        {
            SetColors(TabContainer.SelectedTab.Controls[0] as DataGridView);
            // A TabControl doesn't set the CellStyle of a DataGridView, 
            // except for the currently selected tab, so we need to do it 
            // everytime the selected tab has changed.
        }
        private void OnEditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs args)
        {
            args.Control.KeyPress += InputHandler;
            // The TextBox Editing Control only exists while it is being edited, 
            // which is why we need to connect the InputHandler each time.
        }
        private void InputHandler(object sender, KeyPressEventArgs args)
        {
            // Ensure that only numeric and control input is handled.
            if (!char.IsDigit(args.KeyChar) && !char.IsControl(args.KeyChar))
                args.Handled = true;
        }
        private void ReadData(object sender, EventArgs args)
        {
            Destinations.DataSource = new BindingSource(new BindingList<DestinationType>(Project.DestinationTypes), null);
            Destinations.Columns[0].ReadOnly = true;
            Destinations.Columns[1].ReadOnly = true;
            SetColors(Destinations);

            Vehicles.DataSource = new BindingSource(new BindingList<VehicleType>(Project.VehicleTypes), null);
            Vehicles.Columns[0].ReadOnly = true;
            Vehicles.Columns[1].Visible = false;
            Vehicles.Columns[2].Visible = false;
            Vehicles.Columns[3].Visible = false;
            Vehicles.Columns[4].ReadOnly = true;

            UpdatePercentage();
        }
        private void SetColors(DataGridView data)
        {
            foreach (DataGridViewRow row in data.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.ValueType == typeof(Color))
                    {
                        cell.Style.SelectionBackColor = ((IColorable)row.DataBoundItem).Color;
                        cell.Style.BackColor = ((IColorable)row.DataBoundItem).Color;
                        cell.Style.SelectionForeColor = Color.Transparent;
                        cell.Style.ForeColor = Color.Transparent;
                    }
                }
            }
        }
        private void SaveData(object sender, EventArgs args)
        {
            if (Sum(Vehicles) == 100 && Sum(Destinations) == 100) Close();
            else MessageBox.Show("The types are not fully distributed, please ensure 100% distribution.");
        }
        private void OnClosing(object sender, FormClosingEventArgs args)
        {
            if (Sum(Vehicles) != 100 || Sum(Destinations) != 100)
            {
                MessageBox.Show("The types are not fully distributed, please ensure 100% distribution.");
                args.Cancel = true;
            }
        }
        private double Sum(DataGridView data)
        {
            double sum = 0;
            foreach (DataGridViewRow row in data.Rows)
                sum += ((IDistributable)row.DataBoundItem).Distribution;
            return sum;
        }
        private void UpdatePercentage() { UpdatePercentage(null, null); }
        private void UpdatePercentage(object sender, EventArgs args)
        {
            DestinationsPercent.Text = Sum(Destinations) + "%";
            VehiclesPercent.Text = Sum(Vehicles) + "%";
        }
    }
}

using System;
using System.Drawing;
using System.Windows.Forms;

namespace A319TS
{
    class GUIMenuSettingsDistribution : Form
    {
        private Label Dest;
        private Label Vehicle;
        private Label Procent;
        private Label ProcentOf100;
        private Button OK;
        private Button Set;
        private TextBox Procentset;
        private ComboBox Destinations;
        private ComboBox Vehicles;
        Project Project;

        public GUIMenuSettingsDistribution(Project project)
        {
            Project = project;
            Text = "Distribution";
            Size = new Size(166, 253);
            MinimumSize = new Size(166, 253);
            MaximumSize = new Size(166, 253);
            ShowIcon = false;
            MinimizeBox = false;
            MaximizeBox = false;
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterParent;
            InitializeGUISettingsDistribution();

            Controls.Add(Dest);
            Controls.Add(Vehicle);
            Controls.Add(Procent);
            Controls.Add(ProcentOf100);
            Controls.Add(OK);
            Controls.Add(Procentset);
            Controls.Add(Destinations);
            Controls.Add(Vehicles);
            Controls.Add(Set);
        }
        private void InitializeGUISettingsDistribution()
        {
            Destinations = new ComboBox();
            Destinations.Location = new Point(12, 33);
            Destinations.Size = new Size(121, 24);


            Vehicles = new ComboBox();
            Vehicles.Location = new Point(12, 73);
            Vehicles.Size = new Size(121, 24);

            Procentset = new TextBox();
            Procentset.Location = new Point(12, 120);
            Procentset.Size = new Size(121, 24);

            Vehicle = new Label();
            Vehicle.Text = "Vehicle";
            Vehicle.Location = new Point(9, 53);
            Vehicle.Size = new Size(54, 17);

            Procent = new Label();
            Procent.Text = "%";
            Procent.Location = new Point(9, 100);
            Procent.Size = new Size(79, 17);

            Dest = new Label();
            Dest.Text = "Destination";
            Dest.Location = new Point(9, 6);
            Dest.Size = new Size(79, 17);

            ProcentOf100 = new Label();
            ProcentOf100.Text = "0%";
            ProcentOf100.Location = new Point(105, 100);
            ProcentOf100.Size = new Size(79, 17);

            OK = new Button();
            OK.Text = "OK";
            OK.Location = new Point(12, 177);
            OK.Size = new Size(121, 23);
            OK.Click += OKClick;

            Set = new Button();
            Set.Text = "Set";
            Set.Location = new Point(12, 148);
            Set.Size = new Size(121, 23);
            Set.Click += SetClick;
        }
        private void SetClick(object sender, EventArgs e)
        {

        }
        private void OKClick(object sender, EventArgs e)
        {

        }
    }
}

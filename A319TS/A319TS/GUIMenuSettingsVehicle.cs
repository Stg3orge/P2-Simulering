using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace A319TS
{
    class GUIMenuSettingsVehicle : Form
    {
        private TextBox NameOfVehicle;
        private NumericUpDown MaxSpeedOfVehicle;
        private NumericUpDown AccelerationOfVehicle;
        private NumericUpDown DecelerationOfVehicle;
        private Label NameVehicle;
        private Label MaxSpeed;
        private Label Acceleration;
        private Label Deceleration;
        private Button Add;
        private Button Remove;
        private DataGridView VehicleData;
        Project Project;

        public GUIMenuSettingsVehicle(Project project)
        {
            Project = project;
            Text = "Vehicles";
            Size = new Size(687, 205);
            MinimumSize = new Size(687, 205);
            MaximumSize = new Size(687, 205);
            ShowIcon = false;
            MinimizeBox = false;
            MaximizeBox = false;
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterParent;
            InitializeGUISettingsVehicle();

            Controls.Add(NameOfVehicle);
            Controls.Add(MaxSpeedOfVehicle);
            Controls.Add(AccelerationOfVehicle);
            Controls.Add(DecelerationOfVehicle);
            Controls.Add(NameVehicle);
            Controls.Add(MaxSpeed);
            Controls.Add(Acceleration);
            Controls.Add(Deceleration);
            Controls.Add(Add);
            Controls.Add(Remove);
            Controls.Add(VehicleData);
        }
        private void InitializeGUISettingsVehicle()
        {
            NameOfVehicle = new TextBox();
            NameOfVehicle.Location = new Point(104, 6);
            NameOfVehicle.Size = new Size(100, 22);

            MaxSpeedOfVehicle = new NumericUpDown();
            MaxSpeedOfVehicle.Location = new Point(104, 34);
            MaxSpeedOfVehicle.Size = new Size(100, 22);
            MaxSpeedOfVehicle.Maximum = 1228;
            MaxSpeedOfVehicle.Minimum = 0;

            AccelerationOfVehicle = new NumericUpDown();
            AccelerationOfVehicle.Location = new Point(104, 62);
            AccelerationOfVehicle.Size = new Size(100, 22);
            AccelerationOfVehicle.Maximum = 10;
            AccelerationOfVehicle.Minimum = 0;

            DecelerationOfVehicle = new NumericUpDown();
            DecelerationOfVehicle.Location = new Point(104, 91);
            DecelerationOfVehicle.Size = new Size(100, 22);
            DecelerationOfVehicle.Maximum = 0;
            DecelerationOfVehicle.Minimum = -10;

            NameVehicle = new Label();
            NameVehicle.Text = "NameVehicle";
            NameVehicle.Location = new Point(12, 9);

            MaxSpeed = new Label();
            MaxSpeed.Text = "MaxSpeed";
            MaxSpeed.Location = new Point(12, 37);

            Acceleration = new Label();
            Acceleration.Text = "Acceleration";
            Acceleration.Location = new Point(12, 65);

            Deceleration = new Label();
            Deceleration.Text = "Deceleration";
            Deceleration.Location = new Point(12, 94);

            Add = new Button();
            Add.Text = "Add";
            Add.Location = new Point(12, 119);
            Add.Size = new Size(88, 35);
            Add.Click += AddClick;

            Remove = new Button();
            Remove.Text = "Remove";
            Remove.Location = new Point(104, 119);
            Remove.Size = new Size(88, 35);
            Remove.Click += RemoveClick;

            VehicleData = new DataGridView();
            VehicleData.Text = "SetOfVehicles";
            VehicleData.Location = new Point(210, 6);
            VehicleData.Size = new Size(451, 148);
            VehicleData.DataSource = Project.VehicleTypes;
            VehicleData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            VehicleData.ReadOnly = true;
            VehicleData.Show();
        }

        private void RemoveClick(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in VehicleData.SelectedRows)
                VehicleData.Rows.Remove(row);
        }

        private void AddClick(object sender, EventArgs e)
        {
            if (NameOfVehicle.Text.Length > 0 && Project.VehicleTypes.Find(d => d.Name == NameOfVehicle.Text) == null)
            {
                Project.VehicleTypes.Add(new VehicleType(NameOfVehicle.Text, Convert.ToInt32(MaxSpeedOfVehicle.Text), Convert.ToDouble(AccelerationOfVehicle.Text), Convert.ToDouble(DecelerationOfVehicle.Text)));
                NameVehicle.ForeColor = Color.Black;
            }
            else
            {
                NameVehicle.ForeColor = Color.Red;
            }
            VehicleData.DataSource = new BindingSource(new BindingList<VehicleType>(Project.VehicleTypes), null);
        }
    }
}

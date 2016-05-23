using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace A319TS
{
    class GUIMenuTypesVehicles : Form
    {
        private Project Project;
        private TextBox SetName;
        private NumericUpDown MaxSpeed;
        private NumericUpDown Acceleration;
        private NumericUpDown Deceleration;
        private Label NameLabel;
        private Label MaxSpeedLabel;
        private Label AccelerationLabel;
        private Label DecelerationLabel;
        private Button Add;
        private Button Remove;
        private Button SetColor;
        private DataGridView Vehicles;
        private ColorDialog ColorPicker;

        public GUIMenuTypesVehicles(Project project)
        {
            Project = project;
            Setup();
            Load += ReadData;
        }
        private void Setup()
        {
            Text = "Vehicle Types";
            Size = new Size(700, 233);
            MinimumSize = new Size(700, 233);
            MaximumSize = new Size(700, 233);
            ShowIcon = false;
            MinimizeBox = false;
            MaximizeBox = false;
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterParent;

            ColorPicker = new ColorDialog();

            SetName = new TextBox();
            SetName.Location = new Point(106, 12);
            SetName.Size = new Size(90, 22);
            Controls.Add(SetName);

            MaxSpeed = new NumericUpDown();
            MaxSpeed.Location = new Point(106, 40);
            MaxSpeed.Size = new Size(90, 22);
            MaxSpeed.Maximum = 1228;
            MaxSpeed.Minimum = 0;
            Controls.Add(MaxSpeed);

            Acceleration = new NumericUpDown();
            Acceleration.Location = new Point(106, 68);
            Acceleration.Size = new Size(90, 22);
            Acceleration.Maximum = 10;
            Acceleration.Minimum = 0;
            Controls.Add(Acceleration);

            Deceleration = new NumericUpDown();
            Deceleration.Location = new Point(106, 96);
            Deceleration.Size = new Size(90, 22);
            Deceleration.Maximum = 0;
            Deceleration.Minimum = -10;
            Controls.Add(Deceleration);

            NameLabel = new Label();
            NameLabel.Text = "Name";
            NameLabel.Location = new Point(55, 15);
            Controls.Add(NameLabel);

            MaxSpeedLabel = new Label();
            MaxSpeedLabel.Text = "MaxSpeed";
            MaxSpeedLabel.Location = new Point(22, 42);
            Controls.Add(MaxSpeedLabel);

            AccelerationLabel = new Label();
            AccelerationLabel.Text = "Acceleration";
            AccelerationLabel.Location = new Point(14, 70);
            Controls.Add(AccelerationLabel);

            DecelerationLabel = new Label();
            DecelerationLabel.Text = "Deceleration";
            DecelerationLabel.Location = new Point(12, 98);
            Controls.Add(DecelerationLabel);

            Add = new Button();
            Add.Text = "Add";
            Add.Location = new Point(146, 124);
            Add.Size = new Size(50, 23);
            Add.Click += AddClick;
            Controls.Add(Add);

            SetColor = new Button();
            SetColor.BackColor = ColorPicker.Color;
            SetColor.Location = new Point(117, 124);
            SetColor.Size = new Size(23, 23);
            SetColor.Click += ClickColor;
            Controls.Add(SetColor);

            Remove = new Button();
            Remove.Text = "Remove";
            Remove.Location = new Point(595, 153);
            Remove.Size = new Size(75, 23);
            Remove.Click += RemoveClick;
            Controls.Add(Remove);

            Vehicles = new DataGridView();
            Vehicles.Location = new Point(202, 12);
            Vehicles.Size = new Size(468, 135);
            Vehicles.DataSource = Project.VehicleTypes;
            Vehicles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Vehicles.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            Vehicles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Vehicles.AllowUserToResizeColumns = false;
            Vehicles.AllowUserToResizeRows = false;
            Vehicles.RowHeadersVisible = false;
            Vehicles.ReadOnly = true;
            Controls.Add(Vehicles);
            Vehicles.Show();
        }
        private void ReadData(object sender, EventArgs args)
        {
            Vehicles.DataSource = new BindingSource(new BindingList<VehicleType>(Project.VehicleTypes), null);
            Vehicles.Columns[5].Visible = false;
            UpdateGridColors();
        }
        private void RemoveClick(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in Vehicles.SelectedRows)
                if (((VehicleType)row.DataBoundItem).Name != "Default")
                    Vehicles.Rows.Remove(row);
        }
        private void AddClick(object sender, EventArgs e)
        {
            AddVehicleType();
            UpdateGridColors();
        }
        private void AddVehicleType()
        {
            if (SetName.Text.Length > 0 && Project.VehicleTypes.Find(d => d.Name == SetName.Text) == null)
            {
                Project.VehicleTypes.Add(new VehicleType(SetName.Text, 
                                                         Convert.ToInt32(MaxSpeed.Text), 
                                                         Convert.ToDouble(Acceleration.Text), 
                                                         Convert.ToDouble(Deceleration.Text), 
                                                         ColorPicker.Color));
                Vehicles.DataSource = new BindingSource(new BindingList<VehicleType>(Project.VehicleTypes), null);
                NameLabel.ForeColor = Color.Black;
            }
            else
            {
                NameLabel.ForeColor = Color.Red;
            }
        }
        private void ClickColor(object sender, EventArgs e)
        {
            ColorPicker.ShowDialog();
            SetColor.BackColor = ColorPicker.Color;
        }
        private void UpdateGridColors()
        {
            foreach (DataGridViewRow row in Vehicles.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.ColumnIndex == 4)
                    {
                        cell.Style.SelectionBackColor = ((VehicleType)row.DataBoundItem).Color;
                        cell.Style.BackColor = ((VehicleType)row.DataBoundItem).Color;
                        cell.Style.SelectionForeColor = Color.Transparent;
                        cell.Style.ForeColor = Color.Transparent;
                    }
                }
            }
        }
    }
}
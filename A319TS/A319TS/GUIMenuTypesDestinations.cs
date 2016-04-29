using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace A319TS
{
    class GUIMenuTypesDestinations : Form
    {
        private Project Project;
        private TextBox SetName;
        private Label NameLabel;
        private Button SetColor;
        private Button Add;
        private Button Remove;
        private DataGridView Destinations;
        private ColorDialog ColorPicker;

        public GUIMenuTypesDestinations(Project project)
        {
            Project = project;
            Setup();
            Load += ReadData;
        }

        private void Setup()
        {
            Text = "Destination Types";
            Size = new Size(281, 400);
            MinimumSize = new Size(281, 400);
            MaximumSize = new Size(281, 400);
            ShowIcon = false;
            MinimizeBox = false;
            MaximizeBox = false;
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterParent;

            ColorPicker = new ColorDialog();

            SetName = new TextBox();
            SetName.Location = new Point(63, 12);
            SetName.Size = new Size(100, 22);
            Controls.Add(SetName);

            NameLabel = new Label();
            NameLabel.Text = "Name";
            NameLabel.Location = new Point(12, 15);
            Controls.Add(NameLabel);
            
            SetColor = new Button();
            SetColor.BackColor = ColorPicker.Color;
            SetColor.Location = new Point(169, 12);
            SetColor.Size = new Size(23, 23);
            SetColor.Click += ClickColor;
            Controls.Add(SetColor);

            Add = new Button();
            Add.Text = "Add";
            Add.Location = new Point(198, 12);
            Add.Size = new Size(50, 23);
            Add.Click += AddClick;
            Controls.Add(Add);

            Remove = new Button();
            Remove.Text = "Remove";
            Remove.Location = new Point(176, 320);
            Remove.Size = new Size(75, 23);
            Remove.Click += RemoveClick;
            Controls.Add(Remove);

            Destinations = new DataGridView();
            Destinations.Location = new Point(15, 41);
            Destinations.Size = new Size(233, 273);
            Destinations.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Destinations.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            Destinations.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Destinations.AllowUserToResizeColumns = false;
            Destinations.AllowUserToResizeRows = false;
            Destinations.RowHeadersVisible = false;
            Destinations.ReadOnly = true;
            Controls.Add(Destinations);
            Destinations.Show();
        }
        private void ReadData(object sender, EventArgs args)
        {
            Destinations.DataSource = new BindingSource(new BindingList<DestinationType>(Project.DestinationTypes), null);
            Destinations.Columns[2].Visible = false;
            UpdateGridColors();
        }
        private void RemoveClick(object sender, EventArgs e)
        {
            // Set Destination Types to Default if the type is to be removed.
            foreach (Destination dest in Project.Destinations)
                foreach (DataGridViewRow row in Destinations.SelectedRows)
                    if (dest.Type == (DestinationType)row.DataBoundItem)
                        dest.Type = Project.DestinationTypes[0]; // Default

            // Remove Selected Rows
            foreach (DataGridViewRow row in Destinations.SelectedRows)
                if (((DestinationType)row.DataBoundItem).Name != "Default")
                    Destinations.Rows.Remove(row);
        }
        private void AddClick(object sender, EventArgs e)
        {
            AddDestinationType();
            UpdateGridColors();
        }
        private void AddDestinationType()
        {
            if (SetName.Text.Length > 0 && Project.DestinationTypes.Find(d => d.Name == SetName.Text) == null)
            {
                Project.DestinationTypes.Add(new DestinationType(SetName.Text, ColorPicker.Color));
                Destinations.DataSource = new BindingSource(new BindingList<DestinationType>(Project.DestinationTypes), null);
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
            foreach (DataGridViewRow row in Destinations.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.ColumnIndex == 1)
                    {
                        cell.Style.SelectionBackColor = ((DestinationType)row.DataBoundItem).Color;
                        cell.Style.BackColor = ((DestinationType)row.DataBoundItem).Color;
                        cell.Style.SelectionForeColor = Color.Transparent;
                        cell.Style.ForeColor = Color.Transparent;
                    }
                }
            }
        }
    }
}

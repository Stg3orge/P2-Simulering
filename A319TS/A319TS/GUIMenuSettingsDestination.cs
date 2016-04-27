using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace A319TS
{
    class GUIMenuSettingsDestination : Form
    {
        private TextBox NameSet;
        private Button ColorSet;
        private Button Add;
        private Label NameLabel;
        private Label ColorLabel;
        private DataGridView DestData;
        private ColorDialog SetColorForDest;
        Project Project;
        public GUIMenuSettingsDestination(Project project)
        {
            Project = project;
            Text = "Destination";
            Size = new Size(582, 269);
            MinimumSize = new Size(582, 269);
            MaximumSize = new Size(1000, 1000);
            ShowIcon = false;
            MinimizeBox = false;
            MaximizeBox = false;
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterParent;
            InitializeGUISettingsDestination();

            Controls.Add(NameSet);
            Controls.Add(NameLabel);
            Controls.Add(ColorSet);
            Controls.Add(Add);
            Controls.Add(ColorLabel);
            Controls.Add(DestData);
        }
        private void InitializeGUISettingsDestination()
        {
            NameSet = new TextBox();
            NameSet.Location = new Point(64, 12);
            NameSet.Size = new Size(131, 22);

            NameLabel = new Label();
            NameLabel.Text = "Name";
            NameLabel.Location = new Point(13, 12);

            ColorLabel = new Label();
            ColorLabel.Text = "Color";
            ColorLabel.Location = new Point(13, 46);

            ColorSet = new Button();
            ColorSet.Text = "SetColor";
            ColorSet.Location = new Point(64, 43);
            ColorSet.Size = new Size(131, 24);
            ColorSet.Click += ClickColor;

            SetColorForDest = new ColorDialog();
            SetColorForDest.AllowFullOpen = false;

            Add = new Button();
            Add.Text = "Add";
            Add.Location = new Point(12, 159);
            Add.Size = new Size(183, 46);
            Add.Click += AddClick;

            DestData = new DataGridView();
            DestData.Text = "SetOfDestinations";
            DestData.Location = new Point(218, 12);
            DestData.Size = new Size(329, 193);
            DestData.DataSource = Project.DestinationTypes;
            DestData.Show();
        }

        private void AddClick(object sender, EventArgs e)
        {
            if (NameSet.Text.Length > 0 && Project.DestinationTypes.Find(d => d.Name == NameSet.Text) == null)
            {
                Project.DestinationTypes.Add(new DestinationType(NameSet.Text, SetColorForDest.Color));
                NameLabel.ForeColor = Color.Black;
            }
            else
            {
                NameLabel.ForeColor = Color.Red;
            }
            DestData.DataSource = new BindingSource(new BindingList<DestinationType>(Project.DestinationTypes), null);
            
        }

        private void ClickColor(object sender, EventArgs e)
        {
            SetColorForDest.ShowDialog();
        }
    }
}

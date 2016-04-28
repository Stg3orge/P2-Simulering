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
    class GUIToolEditDestination : Form
    {
        public Destination Destination;
        public Project Project;
        private Label TypeLabel;
        private ComboBox Type;

        public GUIToolEditDestination(Destination dest, Project project)
        {
            Destination = dest;
            Project = project;
            Setup();
            Load += ReadData;
            FormClosing += SaveData;
        }
        private void ReadData(object sender, EventArgs args)
        {
            Type.DataSource = new BindingSource(new BindingList<DestinationType>(Project.DestinationTypes), null);
            Type.SelectedItem = Destination.Type;
        }
        private void SaveData(object sender, EventArgs args)
        {
            Destination.Type = Type.SelectedItem as DestinationType;
        }
        private void SetSize(int width, int height)
        {
            Size = new Size(width, height);
            MinimumSize = new Size(width, height);
            MaximumSize = new Size(width, height);
        }
        private void Setup()
        {
            Text = "Edit Destination";
            ShowIcon = false;
            MinimizeBox = false;
            MaximizeBox = false;
            SizeGripStyle = SizeGripStyle.Hide;
            SetSize(201, 84);

            TypeLabel = new Label();
            TypeLabel.Text = "Type";
            TypeLabel.Location = new Point(12, 15);
            TypeLabel.AutoSize = true;
            Controls.Add(TypeLabel);

            Type = new ComboBox();
            Type.Location = new Point(49, 12);
            Type.Size = new Size(121, 21);
            Type.DropDownStyle = ComboBoxStyle.DropDownList;
            Controls.Add(Type);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.ComponentModel;

namespace A319TS
{
    class GUIToolEditNode : Form
    {
        public Node Node;

        private Label PositionLabel;
        private Label TypeLabel;
        private TextBox Position;
        private ComboBox Type;
        private CheckBox GreenCheck;
        private Label RoadsLabel;
        private DataGridView RoadData;

        public GUIToolEditNode(Node node)
        {
            Node = node;
            Setup();
            FormClosing += Save;
        }
        
        private void Save(object sender, EventArgs args)
        {
            Node.Green = GreenCheck.Checked;
        }
        private void SetSize(int width, int height)
        {
            Size = new Size(width, height);
            MinimumSize = new Size(width, height);
            MaximumSize = new Size(width, height);
        }
        private void Setup()
        {
            Text = "Edit Node";
            ShowIcon = false;
            MinimizeBox = false;
            MaximizeBox = false;
            SizeGripStyle = SizeGripStyle.Hide;
            SetSize(230, 150);

            PositionLabel = new Label();
            PositionLabel.Text = "Position";
            PositionLabel.Location = new Point(12, 16);
            PositionLabel.AutoSize = true;
            Controls.Add(PositionLabel);

            Position = new TextBox();
            Position.Text = Node.Position.X + ", " + Node.Position.Y;
            Position.Location = new Point(77, 13);
            Position.Size = new Size(121, 22);
            Position.ReadOnly = true;
            Controls.Add(Position);

            TypeLabel = new Label();
            TypeLabel.Text = "Type";
            TypeLabel.Location = new Point(12, 44);
            TypeLabel.AutoSize = true;
            Controls.Add(TypeLabel);

            Type = new ComboBox();
            Type.Location = new Point(77, 41);
            Type.Size = new Size(121, 24);
            Type.DataSource = Enum.GetValues(typeof(Node.NodeType));
            Type.SelectedItem = Node.Type;
            Controls.Add(Type);

            GreenCheck = new CheckBox();
            GreenCheck.Text = "Green (Light)";
            GreenCheck.Location = new Point(77, 72);
            GreenCheck.Size = new Size(115, 21);
            GreenCheck.Checked = Node.Green;
            Controls.Add(GreenCheck);
            
            if (Node.Roads.Count > 0)
            {
                SetSize(480, 245);

                RoadsLabel = new Label();
                RoadsLabel.Text = "Roads";
                RoadsLabel.Location = new Point(204, 16);
                RoadsLabel.AutoSize = true;
                Controls.Add(RoadsLabel);

                RoadData = new DataGridView();
                RoadData.Location = new Point(207, 36);
                RoadData.Size = new Size(240, 150);
                RoadData.DataSource = new BindingSource(new BindingList<Road>(Node.Roads), null);
                Controls.Add(RoadData);
            }
        }

        private DataTable ReadRoadData()
        {
            DataTable data = new DataTable();
            data.Columns.Add(new DataColumn("Position"));
            data.Columns.Add(new DataColumn("Type"));
            data.Columns.Add(new DataColumn("Delete"));

            foreach (Road road in Node.Roads)
            {
                Button delete = new Button();
                delete.Text = "Delete";
                data.Rows.Add(road, road.Type, "Delete");
            }

            return data;
        }
    }
}

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

        private Label NodePosition;
        private Label NodeType;
        private Label NodeLight;
        private TextBox NodePositionField;
        private ComboBox NodeTypeField;
        private CheckBox NodeLightCheck;
        private Label NodeRoads;
        private DataGridView RoadData;

        public GUIToolEditNode(Node node)
        {
            Node = node;
            Setup();
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
            SetSize(200, 140);

            NodePosition = new Label();
            NodePosition.Text = "Position";
            NodePosition.Location = new Point(12, 16);
            NodePosition.AutoSize = true;
            Controls.Add(NodePosition);

            NodePositionField = new TextBox();
            NodePositionField.Text = Node.Position.X + ", " + Node.Position.Y;
            NodePositionField.Location = new Point(66, 13);
            NodePositionField.Size = new Size(100, 20);
            NodePositionField.ReadOnly = true;
            Controls.Add(NodePositionField);

            NodeType = new Label();
            NodeType.Text = "Type";
            NodeType.Location = new Point(12, 43);
            NodeType.AutoSize = true;
            Controls.Add(NodeType);

            NodeTypeField = new ComboBox();
            NodeTypeField.Location = new Point(66, 40);
            NodeTypeField.Size = new Size(100, 21);
            NodeTypeField.DataSource = Enum.GetValues(typeof(Node.NodeType));
            NodeTypeField.SelectedItem = Node.Type;
            Controls.Add(NodeTypeField);

            NodeLight = new Label();
            NodeLight.Text = "Light";
            NodeLight.Location = new Point(12, 71);
            NodeLight.AutoSize = true;
            Controls.Add(NodeLight);

            NodeLightCheck = new CheckBox();
            NodeLightCheck.Location = new Point(67, 68);
            NodeLightCheck.Size = new Size(100, 21);
            NodeLightCheck.DataBindings.Add("Checked", Node, "Green");
            Controls.Add(NodeLightCheck);
            
            if (Node.Roads.Count > 0)
            {
                SetSize(210, 315);
                AutoScroll = true;
                SetAutoScrollMargin(0, 10);

                NodeRoads = new Label();
                NodeRoads.Text = "Roads";
                NodeRoads.Location = new Point(12, 100);
                NodeRoads.AutoSize = true;
                Controls.Add(NodeRoads);

                RoadData = new DataGridView();
                RoadData.Location = new Point(12, 129);
                RoadData.Size = new Size(100, 100);
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

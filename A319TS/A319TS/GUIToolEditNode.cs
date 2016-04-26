using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Data;

namespace A319TS
{
    partial class GUIToolEdit : Form
    {
        private Label NodePosition;
        private Label NodeType;
        private Label NodeLight;
        private TextBox NodePositionField;
        private ComboBox NodeTypeField;
        private CheckBox NodeLightCheck;
        private Label NodeRoads;
        private DataGrid RoadData;

        private void InitEditNode(Node node)
        {
            switch (node.Roads.Count)
            {
                case 0:
                    SetSize(200, 140);
                    break;
                default:
                    SetSize(210, 315);
                    AutoScroll = true;
                    SetAutoScrollMargin(0, 10);
                    break;
            }

            NodePosition = new Label();
            NodePosition.Text = "Position";
            NodePosition.Location = new Point(12, 16);
            NodePosition.AutoSize = true;
            Controls.Add(NodePosition);

            NodePositionField = new TextBox();
            NodePositionField.Text = node.Position.X + ", " + node.Position.Y;
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
            NodeTypeField.SelectedItem = node.Type;
            Controls.Add(NodeTypeField);

            NodeLight = new Label();
            NodeLight.Text = "Light";
            NodeLight.Location = new Point(12, 71);
            NodeLight.AutoSize = true;
            Controls.Add(NodeLight);

            NodeLightCheck = new CheckBox();
            NodeLightCheck.Location = new Point(67, 68);
            NodeLightCheck.Size = new Size(100, 21);
            NodeLightCheck.DataBindings.Add("Checked", node, "Green");
            Controls.Add(NodeLightCheck);
            
            /*
            if (node.Roads.Count > 0)
            {
                NodeRoads = new Label();
                NodeRoads.Text = "Roads";
                NodeRoads.Location = new Point(12, 100);
                NodeRoads.AutoSize = true;
                Controls.Add(NodeRoads);

                RoadData = new DataGrid();
                RoadData.DataSource = ReadRoadData(node);

            }
            */
        }

        private DataTable ReadRoadData(Node node)
        {
            DataTable data = new DataTable();
            data.Columns[0].ColumnName = "Position";
            data.Columns[1].ColumnName = "Type";
            data.Columns[2].ColumnName = "Delete";

            foreach (Road road in node.Roads)
            {
                data.Rows.Add(road, road.Type, new Button());
            }

            return data;
        }
    }
}

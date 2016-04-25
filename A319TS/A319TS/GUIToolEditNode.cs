using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

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
        private List<Label> NodeRoadLabels;
        private List<ComboBox> NodeRoadTypes;
        private List<Button> NodeRoadDeleteButtons;
        private void InitEditNode(Node node)
        {
            switch (node.Roads.Count)
            {
                case 0:
                    SetSize(200, 140);
                    break;
                case 1:
                    SetSize(200, 210);
                    break;
                case 2:
                    SetSize(200, 260);
                    break;
                case 3:
                    SetSize(200, 315);
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

            NodeRoads = new Label();
            NodeRoads.Text = "Roads";
            NodeRoads.Location = new Point(12, 100);
            NodeRoads.AutoSize = true;
            Controls.Add(NodeRoads);

            NodeRoadLabels = new List<Label>();
            NodeRoadTypes = new List<ComboBox>();
            NodeRoadDeleteButtons = new List<Button>();

            for (int i = 0; i < node.Roads.Count; i++)
            {
                Label label = new Label();
                label.Text = "(" + node.Roads[i].From.Position.X + ", " + 
                                   node.Roads[i].From.Position.Y + ") - (" + 
                                   node.Roads[i].Destination.Position.X + ", " + 
                                   node.Roads[i].Destination.Position.Y + ")";
                label.Location = new Point(21, 123 + (i * 51));
                label.AutoSize = true;
                NodeRoadLabels.Add(label);

                ComboBox combo = new ComboBox();
                combo.Size = new Size(80, 21);
                combo.Location = new Point(24, 140 + (i * 51));
                NodeRoadTypes.Add(combo);

                Button button = new Button();
                button.Text = "Delete";
                button.Size = new Size(50, 23);
                button.Location = new Point(110, 139 + (i * 51));
                NodeRoadDeleteButtons.Add(button);
            }

            foreach (Label label in NodeRoadLabels)
                Controls.Add(label);
            foreach (ComboBox combo in NodeRoadTypes)
                Controls.Add(combo);
            foreach (Button button in NodeRoadDeleteButtons)
                Controls.Add(button);
        }
    }
}

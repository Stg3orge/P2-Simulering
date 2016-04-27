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
        public Project Project;

        private Label PositionLabel;
        private Label TypeLabel;
        private TextBox Position;
        private ComboBox Type;
        private CheckBox GreenCheck;
        private Label RoadsLabel;
        private DataGridView RoadData;
        private Button RemoveRoad;

        public GUIToolEditNode(Node node, Project project)
        {
            Node = node;
            Project = project;
            Setup();
            Load += ReadData;
            FormClosing += Save;
        }
        
        private void ReadData(object sender, EventArgs args)
        {
            Position.BringToFront();
            Type.BringToFront();

            Type.DataSource = Enum.GetValues(typeof(Node.NodeType));
            Type.SelectedItem = Node.Type;

            if (Node.Roads.Count > 0)
            {
                RoadData.BringToFront();
                RoadData.DataSource = new BindingSource(new BindingList<Road>(Node.Roads), null);
                foreach (DataGridViewColumn column in RoadData.Columns)
                    column.Resizable = DataGridViewTriState.False;
                foreach (DataGridViewRow row in RoadData.Rows)
                    row.Resizable = DataGridViewTriState.False;
                RoadData.Columns[0].ReadOnly = true;
                RoadData.Columns[1].ReadOnly = true;
                RoadData.Columns[2].Visible = false;
                RoadData.Columns[3].Visible = false;
                RoadData.Columns[4].Visible = false;
                var RoadTypeColumn = new DataGridViewComboBoxColumn() { HeaderText = "Type" };
                RoadData.Columns.Insert(3, RoadTypeColumn);
                RoadTypeColumn.DataSource = new BindingSource(new BindingList<RoadType>(Project.RoadTypes), null);
                foreach (DataGridViewRow row in RoadData.Rows)
                    foreach (DataGridViewComboBoxCell cb in row.Cells.OfType<DataGridViewComboBoxCell>())
                        cb.Value = ((Road)RoadData.Rows[cb.RowIndex].DataBoundItem).Type;
                RoadData.Show();
            }
        }
        private void Save(object sender, EventArgs args)
        {
            Node.Green = GreenCheck.Checked;
            Node.Type = (Node.NodeType)Type.SelectedItem;
        }
        private void SetSize(int width, int height)
        {
            Size = new Size(width, height);
            MinimumSize = new Size(width, height);
            MaximumSize = new Size(width, height);
        }
        private void RemoveRoadClick(object sender, EventArgs args)
        {
            foreach (DataGridViewRow row in RoadData.SelectedRows)
                RoadData.Rows.Remove(row);
        }
        private void Setup()
        {
            Text = "Edit Node";
            ShowIcon = false;
            MinimizeBox = false;
            MaximizeBox = false;
            SizeGripStyle = SizeGripStyle.Hide;
            SetSize(193, 130);

            PositionLabel = new Label();
            PositionLabel.Text = "Position";
            PositionLabel.Location = new Point(12, 15);
            PositionLabel.AutoSize = true;
            Controls.Add(PositionLabel);

            Position = new TextBox();
            Position.Text = Node.Position.X + ", " + Node.Position.Y;
            Position.Location = new Point(62, 12);
            Position.Size = new Size(100, 20);
            Position.Enabled = false;
            Controls.Add(Position);

            TypeLabel = new Label();
            TypeLabel.Text = "Type";
            TypeLabel.Location = new Point(12, 41);
            TypeLabel.AutoSize = true;
            Controls.Add(TypeLabel);

            Type = new ComboBox();
            Type.Location = new Point(62, 38);
            Type.Size = new Size(100, 21);
            Type.DropDownStyle = ComboBoxStyle.DropDownList;
            Controls.Add(Type);

            GreenCheck = new CheckBox();
            GreenCheck.Text = "Green (Light)";
            GreenCheck.Location = new Point(62, 65);
            GreenCheck.AutoSize = true;
            GreenCheck.Checked = Node.Green;
            Controls.Add(GreenCheck);
            
            if (Node.Roads.Count > 0)
            {
                Size GridViewSize = new Size(303, 140);
                Point RemoveLocation = new Point(398, 179);
                if (Node.Roads.Count > 5)
                {
                    GridViewSize.Width += 17;
                    RemoveLocation.X += 17;
                    SetSize(522, 252);
                }
                else
                {
                    SetSize(505, 252);
                }

                RoadsLabel = new Label();
                RoadsLabel.Text = "Roads";
                RoadsLabel.Location = new Point(168, 15);
                RoadsLabel.AutoSize = true;
                Controls.Add(RoadsLabel);

                RoadData = new DataGridView();
                RoadData.Location = new Point(171, 32);
                RoadData.Size = GridViewSize;
                RoadData.RowHeadersVisible = false;
                RoadData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                Controls.Add(RoadData);

                RemoveRoad = new Button();
                RemoveRoad.Location = RemoveLocation;
                RemoveRoad.Size = new Size(75, 23);
                RemoveRoad.Text = "Remove";
                RemoveRoad.Click += RemoveRoadClick;
                Controls.Add(RemoveRoad);
            }
        }
    }
}

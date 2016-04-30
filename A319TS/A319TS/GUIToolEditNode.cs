using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

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
        private DataGridView Roads;
        private Button Remove;

        public GUIToolEditNode(Node node, Project project)
        {
            Node = node;
            Project = project;
            Setup();
            Load += ReadData;
            FormClosing += SaveData;
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
            SetSize(229, 145);

            PositionLabel = new Label();
            PositionLabel.Text = "Position";
            PositionLabel.Location = new Point(12, 15);
            PositionLabel.AutoSize = true;
            Controls.Add(PositionLabel);

            Position = new TextBox();
            Position.Text = Node.Position.X + ", " + Node.Position.Y;
            Position.Location = new Point(76, 12);
            Position.Size = new Size(120, 22);
            Position.Enabled = false;
            Controls.Add(Position);

            TypeLabel = new Label();
            TypeLabel.Text = "Type";
            TypeLabel.Location = new Point(12, 43);
            TypeLabel.AutoSize = true;
            Controls.Add(TypeLabel);

            Type = new ComboBox();
            Type.Location = new Point(76, 40);
            Type.Size = new Size(120, 24);
            Type.DropDownStyle = ComboBoxStyle.DropDownList;
            Controls.Add(Type);

            GreenCheck = new CheckBox();
            GreenCheck.Text = "Green (Light)";
            GreenCheck.Location = new Point(76, 70);
            GreenCheck.AutoSize = true;
            GreenCheck.Checked = Node.Green;
            Controls.Add(GreenCheck);

            if (Node.Roads.Count > 0)
            {
                SetSize(547, 280);

                RoadsLabel = new Label();
                RoadsLabel.Text = "Roads";
                RoadsLabel.Location = new Point(208, 15);
                RoadsLabel.AutoSize = true;
                Controls.Add(RoadsLabel);

                Roads = new DataGridView();
                Roads.Location = new Point(221, 36);
                Roads.Size = new Size(303, 158);
                Roads.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                Roads.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                Roads.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                Roads.AllowUserToResizeColumns = false;
                Roads.AllowUserToResizeRows = false;
                Roads.RowHeadersVisible = false;
                Roads.ReadOnly = true;
                Controls.Add(Roads);
                Roads.Show();

                Remove = new Button();
                Remove.Location = new Point(439, 200);
                Remove.Size = new Size(75, 23);
                Remove.Text = "Remove";
                Remove.Click += RemoveRoadClick;
                Controls.Add(Remove);
            }
        }
        private void ReadData(object sender, EventArgs args)
        {
            Type.DataSource = Enum.GetValues(typeof(NodeTypes));
            Type.SelectedItem = Node.Type;

            if (Node.Roads.Count > 0)
            {
                Roads.DataSource = new BindingSource(new BindingList<Road>(Node.Roads), null);
                foreach (DataGridViewColumn column in Roads.Columns)
                    column.Visible = false;
                Roads.Columns[0].Visible = true;
                Roads.Columns[1].Visible = true;
                Roads.Columns[2].Visible = true;
            }
        }
        private void SaveData(object sender, EventArgs args)
        {
            Node.Green = GreenCheck.Checked;
            Node.Type = (NodeTypes)Type.SelectedItem;
        }
        private void RemoveRoadClick(object sender, EventArgs args)
        {
            foreach (DataGridViewRow row in Roads.SelectedRows)
                Roads.Rows.Remove(row);
        }
    }
}

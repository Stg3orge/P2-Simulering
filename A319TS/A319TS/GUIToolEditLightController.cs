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
    class GUIToolEditLightController : Form
    {
        public LightController Controller;
        private Label FirstTimeLabel;
        private Label SecondTimeLabel;
        private NumericUpDown FirstTime;
        private NumericUpDown SecondTime;
        private Label LinksLabel;
        private DataGridView Links;
        private Button Remove;

        public GUIToolEditLightController(LightController controller)
        {
            Controller = controller;
            Setup();
            Load += ReadData;
            FormClosing += SaveData;
        }
        private void ReadData(object sender, EventArgs args)
        {
            Links.DataSource = new BindingSource(new BindingList<Node>(Controller.Lights), null);
            Links.Columns[0].Visible = false;
            Links.Columns[3].Visible = false;
            Links.Columns[1].ReadOnly = true;
        }
        private void SaveData(object sender, EventArgs args)
        {
            Controller.FirstTime = Convert.ToInt32(FirstTime.Value);
            Controller.SecondTime = Convert.ToInt32(SecondTime.Value);
        }
        private void Setup()
        {
            Text = "Edit Light Controller";
            ShowIcon = false;
            MinimizeBox = false;
            MaximizeBox = false;
            SizeGripStyle = SizeGripStyle.Hide;
            SetSize(219, 109);

            FirstTimeLabel = new Label();
            FirstTimeLabel.Text = "First Time";
            FirstTimeLabel.Location = new Point(12, 15);
            FirstTimeLabel.AutoSize = true;
            Controls.Add(FirstTimeLabel);

            FirstTime = new NumericUpDown();
            FirstTime.Text = Controller.FirstTime.ToString();
            FirstTime.Location = new Point(88, 12);
            FirstTime.Size = new Size(100, 20);
            Controls.Add(FirstTime);

            SecondTimeLabel = new Label();
            SecondTimeLabel.Text = "Second Time";
            SecondTimeLabel.Location = new Point(12, 41);
            SecondTimeLabel.AutoSize = true;
            Controls.Add(SecondTimeLabel);

            SecondTime = new NumericUpDown();
            SecondTime.Text = Controller.SecondTime.ToString();
            SecondTime.Location = new Point(88, 38);
            SecondTime.Size = new Size(100, 20);
            Controls.Add(SecondTime);

            if (Controller.Lights.Count > 0)
            {
                Size GridViewSize = new Size(203, 140);
                Point RemoveLocation = new Point(143, 229);
                if (Controller.Lights.Count > 5)
                {
                    GridViewSize.Width += 17;
                    RemoveLocation.X += 17;
                    SetSize(249 + 17, 302);
                }
                else
                {
                    SetSize(249, 302);
                }

                LinksLabel = new Label();
                LinksLabel.Text = "Links";
                LinksLabel.Location = new Point(12, 67);
                LinksLabel.AutoSize = true;
                Controls.Add(LinksLabel);

                Links = new DataGridView();
                Links.Location = new Point(15, 84);
                Links.Size = GridViewSize;
                Links.RowHeadersVisible = false;
                Links.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                Links.AllowUserToResizeColumns = false;
                Links.AllowUserToResizeRows = false;
                Links.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                Links.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                Controls.Add(Links);

                Remove = new Button();
                Remove.Location = RemoveLocation;
                Remove.Size = new Size(75, 23);
                Remove.Text = "Remove";
                Remove.Click += RemoveClick;
                Controls.Add(Remove);
            }
        }
        private void SetSize(int width, int height)
        {
            Size = new Size(width, height);
            MinimumSize = new Size(width, height);
            MaximumSize = new Size(width, height);
        }
        private void RemoveClick(object sender, EventArgs args)
        {
            foreach (DataGridViewRow row in Links.SelectedRows)
                Links.Rows.Remove(row);
        }
    }
}

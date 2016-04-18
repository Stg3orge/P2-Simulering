using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace A319TS
{
    class ViewportController
    {
        private Viewport _viewport;
        private GUIMain _main;

        public ViewportController(Viewport viewport, GUIMain main)
        {
            _viewport = viewport;
            _main = main;

            _viewport.MouseMove += OnMove;
            _viewport.MouseWheel += OnWheel;
            _viewport.MouseClick += OnClick;
        }
        
        private void OnMove(object sender, MouseEventArgs args)
        {
            if (args.Button == MouseButtons.Right)
            {
                _viewport.HScrollBar.Value += args.Location.X - _viewport.MousePos.X;
                _viewport.VScrollBar.Value += args.Location.Y - _viewport.MousePos.Y;
                _viewport.Refresh();
            }
            _viewport.MousePos = args.Location;
        }
        private void OnWheel(object sender, MouseEventArgs args)
        {
            if (args.Delta > 0)
                _viewport.Zoom += 0.25F;
            else
                _viewport.Zoom -= 0.25F;
            _viewport.Refresh();
            _main.UpdateStatusZoom();
        }
        private void OnClick(object sender, MouseEventArgs args)
        {
            MessageBox.Show("Hello!");
            if (args.Button == MouseButtons.Left)
            {
                switch (_main.CheckedTool.Name)
                {
                    case "ToolAddNode": AddNode(); break;
                    case "ToolRemoveNode": RemoveNode(); break;
                    case "ToolAddRoad": AddRoad(); break;
                    default: break;
                }
            }
        }
        private void AddNode()
        {
            _main.CurrentProject.Nodes.Add(new Node(_viewport.GridPos));
            _main.UpdateStatusNodes();
        }
        private void RemoveNode()
        {

        }
        private void AddRoad()
        {

        }
    }
}

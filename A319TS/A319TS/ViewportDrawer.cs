using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace A319TS
{
    class ViewportDrawer
    {
        private Viewport v;
        public ViewportDrawer(Viewport viewport)
        {
            v = viewport;
            v.Grid.Paint += DrawGrid;
            v.Roads.Paint += DrawRoads;
            v.Nodes.Paint += DrawNodes;
            v.Entities.Paint += DrawEntities;
            v.Information.Paint += DrawInformation;
        }
        private Point GetDrawPosition(Point position)
        {
            return new Point(position.X * v.GridSize, position.Y * v.GridSize);
        }
        private void DrawGrid(object sender, PaintEventArgs args)
        {
            args.Graphics.SmoothingMode = SmoothingMode.HighSpeed;
            args.Graphics.TranslateTransform(v.HScrollBar.Value * -1, v.VScrollBar.Value * -1);
            args.Graphics.ScaleTransform(v.Zoom, v.Zoom);
            for (int i = 0; i < v.GridLength; i += v.GridSize)
            {
                args.Graphics.DrawLine(Pens.LightGray, i, 0, i, v.GridLength);
                args.Graphics.DrawLine(Pens.LightGray, 0, i, v.GridLength, i);
            }
        }
        private void DrawRoads(object sender, PaintEventArgs args)
        {
            args.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            args.Graphics.TranslateTransform(v.HScrollBar.Value * -1, v.VScrollBar.Value * -1);
            args.Graphics.ScaleTransform(v.Zoom, v.Zoom);
            Pen pen = new Pen(Color.Black);
            pen.CustomEndCap = new AdjustableArrowCap(4, 4);

            foreach (Node node in v.Project.Nodes)
                foreach (Road road in node.Roads)
                    args.Graphics.DrawLine(pen, GetDrawPosition(node.Position),
                        GetDrawPosition(road.Destination.Position));
        }
        private void DrawNodes(object sender, PaintEventArgs args)
        {
            args.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            args.Graphics.TranslateTransform(v.HScrollBar.Value * -1, v.VScrollBar.Value * -1);
            args.Graphics.ScaleTransform(v.Zoom, v.Zoom);
            foreach (Node node in v.Project.Nodes)
            {
                Point position = GetDrawPosition(node.Position);
                position.X -= v.NodeSize / 2;
                position.Y -= v.NodeSize / 2;
                args.Graphics.FillEllipse(Brushes.White, position.X, position.Y, v.NodeSize, v.NodeSize);
                args.Graphics.DrawEllipse(Pens.Black, position.X, position.Y, v.NodeSize, v.NodeSize);
            }
        }
        private void DrawEntities(object sender, PaintEventArgs args)
        {
            args.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            args.Graphics.TranslateTransform(v.HScrollBar.Value * -1, v.VScrollBar.Value * -1);
            args.Graphics.ScaleTransform(v.Zoom, v.Zoom);
        }
        private void DrawInformation(object sender, PaintEventArgs args)
        {
            args.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            args.Graphics.TranslateTransform(v.HScrollBar.Value * -1, v.VScrollBar.Value * -1);
            args.Graphics.ScaleTransform(v.Zoom, v.Zoom);
        }
    }
}
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
    partial class Viewport
    {
        private Point GetDrawPosition(Point position)
        {
            return new Point(position.X * GridSize, position.Y * GridSize);
        }
        private void DrawGrid(object sender, PaintEventArgs args)
        {
            args.Graphics.SmoothingMode = SmoothingMode.HighSpeed;
            args.Graphics.TranslateTransform(ViewPos.X, ViewPos.Y);
            args.Graphics.ScaleTransform(Zoom, Zoom);
            for (int i = 0; i < GridLength; i += GridSize)
            {
                args.Graphics.DrawLine(Pens.LightGray, i, 0, i, GridLength);
                args.Graphics.DrawLine(Pens.LightGray, 0, i, GridLength, i);
            }
        }
        private void DrawRoads(object sender, PaintEventArgs args)
        {
            args.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            args.Graphics.TranslateTransform(ViewPos.X, ViewPos.Y);
            args.Graphics.ScaleTransform(Zoom, Zoom);
            Pen pen = new Pen(Color.Black, 4);
            pen.CustomEndCap = new AdjustableArrowCap(2, 2);

            foreach (Node node in Project.Nodes)
                foreach (Road road in node.Roads)
                    args.Graphics.DrawLine(pen, GetDrawPosition(node.Position),
                        GetDrawPosition(road.Destination.Position));
        }
        private void DrawNodes(object sender, PaintEventArgs args)
        {
            args.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            args.Graphics.TranslateTransform(ViewPos.X, ViewPos.Y);
            args.Graphics.ScaleTransform(Zoom, Zoom);
            foreach (Node node in Project.Nodes)
            {
                Point position = GetDrawPosition(node.Position);
                position.X -= NodeSize / 2;
                position.Y -= NodeSize / 2;
                args.Graphics.FillEllipse(Brushes.White, position.X, position.Y, NodeSize, NodeSize);
                args.Graphics.DrawEllipse(Pens.Black, position.X, position.Y, NodeSize, NodeSize);
            }
        }
        private void DrawEntities(object sender, PaintEventArgs args)
        {
            args.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            args.Graphics.TranslateTransform(ViewPos.X, ViewPos.Y);
            args.Graphics.ScaleTransform(Zoom, Zoom);
        }
        private void DrawInformation(object sender, PaintEventArgs args)
        {
            args.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            args.Graphics.TranslateTransform(ViewPos.X, ViewPos.Y);
            args.Graphics.ScaleTransform(Zoom, Zoom);
        }
    }
}
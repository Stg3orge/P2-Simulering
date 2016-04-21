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
            Pen pen = new Pen(Color.Black);
            pen.CustomEndCap = new AdjustableArrowCap(4, 4);

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

                Brush fill = Brushes.White;
                switch (node.Type)
                {
                    case Node.NodeType.Yield: fill = Brushes.Yellow; break;
                    case Node.NodeType.Home: fill = Brushes.Magenta; break;
                    case Node.NodeType.Parking: fill = Brushes.Blue; break;
                    case Node.NodeType.Light: if (node.Green) fill = Brushes.Green; else fill = Brushes.Red; break;
                    default: break;
                }

                args.Graphics.FillEllipse(fill, position.X, position.Y, NodeSize, NodeSize);
                args.Graphics.DrawEllipse(Pens.Black, position.X, position.Y, NodeSize, NodeSize);
            }
        }
        private void DrawEntities(object sender, PaintEventArgs args)
        {
            args.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            args.Graphics.TranslateTransform(ViewPos.X, ViewPos.Y);
            args.Graphics.ScaleTransform(Zoom, Zoom);
            foreach (LightController controller in Project.LightControllers)
            {
                Point position = GetDrawPosition(controller.Position);
                position.X -= EntitySize / 2;
                position.Y -= EntitySize / 2;
                Rectangle rect = new Rectangle(position.X, position.Y, EntitySize, EntitySize);
                Rectangle pieRect = new Rectangle(position.X + 1, position.Y + 1, EntitySize - 2, EntitySize - 2);
                args.Graphics.FillRectangle(Brushes.White, rect);
                args.Graphics.DrawRectangle(Pens.Black, rect);
                args.Graphics.FillPie(Brushes.Black, pieRect, 0, 270);
            }
            foreach (Destination dest in Project.Destinations)
            {
                Point position = GetDrawPosition(dest.Position);
                position.X -= EntitySize / 2;
                position.Y -= EntitySize / 2;
                Rectangle rect = new Rectangle(position.X, position.Y, EntitySize, EntitySize);
                Rectangle innerRect = new Rectangle(position.X + 2, position.Y + 2, EntitySize - 4, EntitySize - 4);
                args.Graphics.FillRectangle(new SolidBrush(dest.Type.Color), rect);
                args.Graphics.DrawRectangle(Pens.Black, rect);
                args.Graphics.DrawRectangle(Pens.Black, innerRect);
            }
        }
        private void DrawInformation(object sender, PaintEventArgs args)
        {
            args.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            args.Graphics.TranslateTransform(ViewPos.X, ViewPos.Y);
            args.Graphics.ScaleTransform(Zoom, Zoom);
        }
    }
}
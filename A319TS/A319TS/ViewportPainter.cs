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
        private void ScaleTranslateSmooth(SmoothingMode mode, PaintEventArgs args)
        {
            args.Graphics.SmoothingMode = mode;
            args.Graphics.TranslateTransform(ViewPos.X, ViewPos.Y);
            args.Graphics.ScaleTransform(Zoom, Zoom);
        }
        private void DrawGrid(object sender, PaintEventArgs args)
        {
            ScaleTranslateSmooth(SmoothingMode.HighSpeed, args);
            for (int i = 0; i < GridLength; i += GridSize)
            {
                args.Graphics.DrawLine(Pens.LightGray, i, 0, i, GridLength);
                args.Graphics.DrawLine(Pens.LightGray, 0, i, GridLength, i);
            }
        }
        private void DrawRoads(object sender, PaintEventArgs args)
        {
            ScaleTranslateSmooth(SmoothingMode.HighQuality, args);

            // Draw Light Controller Links
            Pen linkPen = new Pen(Color.Magenta);
            linkPen.DashPattern = new float[] { 4F, 4F };
            foreach (LightController controller in Project.LightControllers)
                foreach (Node light in controller.Lights)
                    args.Graphics.DrawLine(linkPen, GetDrawPosition(controller.Position), GetDrawPosition(light.Position));

            // Draw Roads
            Pen roadPen = new Pen(Color.Black, 2);
            roadPen.CustomEndCap = new AdjustableArrowCap(4, 4);
            foreach (Node node in Project.Nodes)
                foreach (Road road in node.Roads)
                    args.Graphics.DrawLine(roadPen, GetDrawPosition(node.Position),
                        GetDrawPosition(road.Destination.Position));
        }
        private void DrawNodes(object sender, PaintEventArgs args)
        {
            ScaleTranslateSmooth(SmoothingMode.HighQuality, args);
            foreach (Node node in Project.Nodes)
            {
                Point position = GetDrawPosition(node.Position);
                position.X -= NodeSize / 2;
                position.Y -= NodeSize / 2;

                switch (node.Type)
                {
                    case Node.NodeType.Yield:
                        DrawNode(Brushes.Yellow, position, args);
                        break;
                    case Node.NodeType.Home:
                        DrawNode(Brushes.Magenta, position, args);
                        break;
                    case Node.NodeType.Parking:
                        DrawNode(Brushes.Blue, position, args);
                        break;
                    case Node.NodeType.Light:
                        if (node.Green) DrawNode(Brushes.LimeGreen, position, args);
                        else DrawNode(Brushes.Red, position, args);
                        break;

                    case Node.NodeType.Inbound:
                        
                        DrawNode(Brushes.Black, position, args);
                        DrawArrow(node, true, args);
                      
                        break;
                    case Node.NodeType.Outbound:
                        DrawNode(Brushes.Black, position, args);
                        DrawArrow(node, false, args);
                        break;
                    default:
                        DrawNode(Brushes.White, position, args);
                        break;
                }
            }
        }
        private void DrawNode(Brush fill, Point position, PaintEventArgs args)
        {
            args.Graphics.FillEllipse(fill, position.X, position.Y, NodeSize, NodeSize);
            args.Graphics.DrawEllipse(Pens.Black, position.X, position.Y, NodeSize, NodeSize);
        }
        private void DrawArrow(Node node, bool left, PaintEventArgs args)
        {
            Point PosNode = GetDrawPosition(node.Position);
            Point offset;
            Pen arrow = new Pen(Color.White, 2);
            arrow.CustomEndCap = new AdjustableArrowCap(3, 3);
            if (left)
            {
                offset = new Point(PosNode.X - 4, PosNode.Y);
                args.Graphics.DrawLine(arrow, PosNode, offset);
            }
            else
            {
                offset = new Point(PosNode.X + 4, PosNode.Y);
                args.Graphics.DrawLine(arrow, PosNode, offset);
            }
        }
        private void DrawEntities(object sender, PaintEventArgs args)
        {
            ScaleTranslateSmooth(SmoothingMode.HighQuality, args);
            foreach (LightController controller in Project.LightControllers)
            {
                Point position = GetDrawPosition(controller.Position);
                position.X -= EntitySize / 2;
                position.Y -= EntitySize / 2;
                DrawLightController(position, args);
            }
            foreach (Destination dest in Project.Destinations)
            {
                Point position = GetDrawPosition(dest.Position);
                position.X -= EntitySize / 2;
                position.Y -= EntitySize / 2;
                DrawDestination(dest.Type.Color, position, args);
            }
        }
        private void DrawLightController(Point position, PaintEventArgs args)
        {
            Rectangle rect = new Rectangle(position.X, position.Y, EntitySize, EntitySize);
            Rectangle pieRect = new Rectangle(position.X + 1, position.Y + 1, EntitySize - 2, EntitySize - 2);
            args.Graphics.FillRectangle(Brushes.White, rect);
            args.Graphics.DrawRectangle(Pens.Black, rect);
            args.Graphics.FillPie(Brushes.Black, pieRect, 0, 270);
        }
        private void DrawDestination(Color color, Point position, PaintEventArgs args)
        {
            Rectangle rect = new Rectangle(position.X, position.Y, EntitySize, EntitySize);
            Rectangle innerRect = new Rectangle(position.X + 2, position.Y + 2, EntitySize - 4, EntitySize - 4);
            args.Graphics.FillRectangle(new SolidBrush(color), rect);
            args.Graphics.DrawRectangle(Pens.Black, rect);
            args.Graphics.DrawRectangle(Pens.Black, innerRect);
        }
        private void DrawInformation(object sender, PaintEventArgs args)
        {
            ScaleTranslateSmooth(SmoothingMode.HighQuality, args);
            object obj = GetObjByGridPos();
            Point ellipsePosition = GetDrawPosition(GridPos);
            Point textPosition = GetDrawPosition(GridPos);
            ellipsePosition.X -= EntitySize / 2;
            ellipsePosition.Y -= EntitySize / 2;
            textPosition.X += EntitySize;
            textPosition.Y -= EntitySize / 2;
            args.Graphics.DrawEllipse(Pens.Black, new Rectangle(ellipsePosition, new Size(EntitySize, EntitySize)));
            if (obj != null) DrawOutlinedString(obj.ToString(), textPosition, args);
            if (HoverConnection != new Point(-1, -1)) args.Graphics.DrawLine(Pens.Black, GetDrawPosition(HoverConnection), GetDrawPosition(GridPos));
        }
        private void DrawOutlinedString(string text, Point position, PaintEventArgs args)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddString(text, FontFamily.GenericMonospace, (int)FontStyle.Regular, args.Graphics.DpiY * 8 / 72, position, new StringFormat());
            args.Graphics.DrawPath(Pens.DarkSlateGray, path);
        }
    }
}
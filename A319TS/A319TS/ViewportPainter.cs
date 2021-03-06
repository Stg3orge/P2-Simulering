﻿using System;
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
        protected Point GetDrawPosition(Point position)
        {
            return new Point(position.X * GridSize, position.Y * GridSize);
        }
        protected void ScaleTranslateSmooth(SmoothingMode mode, PaintEventArgs args)
        {
            args.Graphics.SmoothingMode = mode;
            args.Graphics.TranslateTransform(ViewPos.X, ViewPos.Y);
            args.Graphics.ScaleTransform(Zoom, Zoom);
        }
        protected void DrawGrid(object sender, PaintEventArgs args)
        {
            ScaleTranslateSmooth(SmoothingMode.HighSpeed, args);
            for (int i = 0; i < GridLength * GridSize; i += GridSize)
            {
                args.Graphics.DrawLine(Pens.LightGray, i, 0, i, GridLength * GridSize);
                args.Graphics.DrawLine(Pens.LightGray, 0, i, GridLength * GridSize, i);
            }
        }
        protected virtual void DrawConnections(object sender, PaintEventArgs args)
        {
            ScaleTranslateSmooth(SmoothingMode.HighQuality, args);

            // Draw Light Controller Links
            Pen linkPen = new Pen(Color.Magenta);
            linkPen.DashPattern = new float[] { 4F, 4F };
            foreach (LightController controller in Project.LightControllers)
                foreach (Node light in controller.Lights)
                    args.Graphics.DrawLine(linkPen, GetDrawPosition(controller.Position), GetDrawPosition(light.Position));

            // Draw Roads
            foreach (Node node in Project.Nodes)
                foreach (Road road in node.Roads)
                    DrawRoad(road, args);
        }
        protected void DrawRoad(Road road, PaintEventArgs args)
        {
            Pen roadPen = new Pen(Color.Black, 2);
            roadPen.CustomEndCap = new AdjustableArrowCap(4, 4);

            if(road.Partition == Partitions.Primary)
                roadPen.Color = Color.Blue;
            if (road.Partition == Partitions.Secondary)
                roadPen.Color = Color.Red;

            args.Graphics.DrawLine(roadPen, GetDrawPosition(road.From.Position), GetDrawPosition(road.To.Position));
        }


        protected void DrawNodes(object sender, PaintEventArgs args)
        {
            ScaleTranslateSmooth(SmoothingMode.HighQuality, args);
            foreach (Node node in Project.Nodes)
            {
                Point position = GetDrawPosition(node.Position);
                position.X -= NodeSize / 2;
                position.Y -= NodeSize / 2;

                switch (node.Type)
                {
                    case NodeTypes.Yield:
                        DrawNode(Brushes.Yellow, position, args);
                        break;
                    case NodeTypes.Home:
                        DrawNode(Brushes.Magenta, position, args);
                        break;
                    case NodeTypes.Parking:
                        DrawNode(Brushes.Blue, position, args);
                        break;
                    case NodeTypes.Light:
                        if (node.Green) DrawNode(Brushes.LimeGreen, position, args);
                        else DrawNode(Brushes.Red, position, args);
                        break;
                    case NodeTypes.Inbound:
                        DrawNode(Brushes.Black, position, args);
                        DrawArrow(node, true, args);
                        break;
                    case NodeTypes.Outbound:
                        DrawNode(Brushes.Black, position, args);
                        DrawArrow(node, false, args);
                        break;
                    default:
                        DrawNode(Brushes.White, position, args);
                        break;
                }
            }
        }
        protected void DrawNode(Brush fill, Point position, PaintEventArgs args)
        {
            args.Graphics.FillEllipse(fill, position.X, position.Y, NodeSize, NodeSize);
            args.Graphics.DrawEllipse(Pens.Black, position.X, position.Y, NodeSize, NodeSize);
        }
        protected void DrawArrow(Node node, bool left, PaintEventArgs args)
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
        protected virtual void DrawEntities(object sender, PaintEventArgs args)
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
        protected void DrawLightController(Point position, PaintEventArgs args)
        {
            Rectangle rect = new Rectangle(position.X, position.Y, EntitySize, EntitySize);
            Rectangle pieRect = new Rectangle(position.X + 1, position.Y + 1, EntitySize - 2, EntitySize - 2);
            args.Graphics.FillRectangle(Brushes.White, rect);
            args.Graphics.DrawRectangle(Pens.Black, rect);
            args.Graphics.FillPie(Brushes.Black, pieRect, 0, 270);
        }
        protected void DrawDestination(Color color, Point position, PaintEventArgs args)
        {
            Rectangle rect = new Rectangle(position.X, position.Y, EntitySize, EntitySize);
            Rectangle innerRect = new Rectangle(position.X + 2, position.Y + 2, EntitySize - 4, EntitySize - 4);
            args.Graphics.FillRectangle(new SolidBrush(color), rect);
            args.Graphics.DrawRectangle(Pens.Black, rect);
            args.Graphics.DrawRectangle(Pens.Black, innerRect);
        }
        protected void DrawInformation(object sender, PaintEventArgs args)
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
            if (obj != null) args.Graphics.DrawString(obj.ToString(), SystemFonts.DialogFont, Brushes.Black, textPosition);
            if (HoverConnection != new Point(-1, -1)) args.Graphics.DrawLine(Pens.Black, GetDrawPosition(HoverConnection), GetDrawPosition(GridPos));
        }
    }
}
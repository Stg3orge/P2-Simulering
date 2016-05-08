using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace A319TS
{
    class Vector2D
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Length
        {
            get { return MathExtension.Distance(new PointD(0, 0), new PointD(X, Y)); }
        }

        public Vector2D()
        {
            X = 0;
            Y = 0;
        }
        public Vector2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        public void Scale(double scalar)
        {
            X *= scalar;
            Y *= scalar;
        }
        public void ToUnit()
        {
            double magnitude = Length;
            X /= magnitude;
            Y /= magnitude;
        }
        public static Vector2D FromRoad(Road road)
        {
            Vector2D vector = new Vector2D();
            vector.X = road.To.Position.X - road.From.Position.X;
            vector.Y = road.To.Position.Y - road.From.Position.Y;
            return vector;
        }
    }
}

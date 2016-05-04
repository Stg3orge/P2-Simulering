using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace A319TS
{
    static class MathExtension
    {
        static public double Distance(Point from, Point to)
        {
            return (Math.Sqrt(Math.Pow(Math.Abs(from.X - to.X), 2)
                            + Math.Pow(Math.Abs(from.Y - to.Y), 2)));
        }
        static public double Distance(PointF from, PointF to)
        {
            return (Math.Sqrt(Math.Pow(Math.Abs(from.X - to.X), 2)
                            + Math.Pow(Math.Abs(from.Y - to.Y), 2)));
        }
        static public double KmhToMetersPerMs(double kmh)
        {
            return kmh * 0.000277778;
        }
    }
}
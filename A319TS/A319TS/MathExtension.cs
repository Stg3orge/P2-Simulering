using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace A319TS
{
    public static class MathExtension
    {
        static public double Distance(Point from, Point to)
        {
            return Math.Sqrt((to.X - from.X) * (to.X - from.X) + (to.Y - from.Y) * (to.Y - from.Y));
        }
        static public double Distance(PointD from, PointD to)
        {
            return Math.Sqrt((to.X - from.X) * (to.X - from.X) + (to.Y - from.Y) * (to.Y - from.Y));
        }
        static public double KmhToMms(double kmh)
        {
            return kmh / 3600;
        }
    }
}
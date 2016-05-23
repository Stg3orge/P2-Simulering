using System;
using System.Drawing;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace A319TS.Tests
{

    [TestClass]
    public class MathExtensionTests
    {
        [TestMethod]
        public void Distance_TestToSeeIfCalulationIsCorrectWhenInputIsTwoRandomCoords()
        {
            Point pointA = new Point(6, 10);
            Point pointB = new Point(2, 15);


            double result = MathExtension.Distance(pointA, pointB);

            Assert.AreEqual(6.40312, result, 0.00001);
        }
        [TestMethod]
        public void Distance_Test2ToSeeIfCalculationIsCorrectWhenInputIsTwoRandomCoords()
        {
            Point pointA = new Point(11, 10);
            Point pointB = new Point(4, 15);


            double dingus = MathExtension.Distance(pointA, pointB);

            Assert.AreEqual(8.60233, dingus, 0.00001);
        }

        [TestMethod]
        public void KmhToMms_TestToSeeIfCalculationIsCorrectParsedWithDouble50000Point5()
        {
            double Pingus = 50000.5;

            double Dingus = MathExtension.KmhToMms(Pingus);

            Assert.AreEqual(13.889027, Dingus, 0.000001);
        }

        [TestMethod]
        public void KmhToMms_TestToSeeIfCalculationIsCorrectParsedWithDouble100Point00001()
        {
            double Pingus = 100.00001;

            double Dingus = MathExtension.KmhToMms(Pingus);

            Assert.AreEqual(0.0277, Dingus, 0.0001);
        }
    }
}

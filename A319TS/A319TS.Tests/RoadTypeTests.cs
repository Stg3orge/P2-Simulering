using Microsoft.VisualStudio.TestTools.UnitTesting;
using A319TS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A319TS.Tests
{
    [TestClass]
    public class RoadTypeTests
    {
        [TestMethod]
        public void CompareTo_TestOfInputAndResultWhenParsedWithTwoDifferentSpeedValues()
        {
            // ARRANGE
            var rt = new RoadType("Motorvej", 100);
           
            var rt1 = new RoadType("MiscVej", 60);

            // ACT
            var result = rt1.CompareTo(rt1);
            var result2 = rt.CompareTo(rt1);

            // ASSERT
            
            Assert.AreEqual(0, result);
            Assert.AreEqual(1, result2);
        }

        [TestMethod]
        public void CompareTo_TestWithListOfMinAndMax()
        {
            List<RoadType> MinMaxList = new List<RoadType>();

            MinMaxList.Add(new RoadType("Motorvej", 100));
            MinMaxList.Add(new RoadType("MiscVej", 60));
            MinMaxList.Add(new RoadType("VillaVej", 20));
            MinMaxList.Add(new RoadType("MotorTrafikVej", 90));
            MinMaxList.Add(new RoadType("Racerbane", 250));

            RoadType min = MinMaxList.Min();
            RoadType max = MinMaxList.Max();

            Assert.AreSame(min, MinMaxList[2]);
            Assert.AreSame(max, MinMaxList[4]);
            
        }
    }
}
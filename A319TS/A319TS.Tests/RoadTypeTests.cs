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
        public void CompareTo_TestOfInputAndResultWhenParsedWithValueSixty()
        {
            // ARRANGE
            var rt = new RoadType("-", 0);
            
            rt.Name = "MiscVej";
            rt.Speed = 60;

            var rt1 = new RoadType(rt.Name, rt.Speed);

            //rt.CompareTo(rt);

            // ACT
            var result = rt1.CompareTo(rt1);

            // ASSERT
            
            Assert.Fail();
        }
    }
}
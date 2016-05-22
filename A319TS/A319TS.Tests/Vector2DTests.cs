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
    public class Vector2DTests
    {
        [TestMethod]
        public void ToUnit_TestIfLengthIsOneOrCloseTo()
        {
            Vector2D vec = new Vector2D(13, 24);
            
            vec.ToUnit();
            
            Assert.AreEqual(1, vec.Length, 0.0000000001);
        }

        [TestMethod]
        public void Scale_Test()
        {
            Vector2D vec2 = new Vector2D(10, 0);

            vec2.Scale(6.5);

            Assert.AreEqual(65, vec2.Length);
        }
    }
}
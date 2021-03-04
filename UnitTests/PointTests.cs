using Microsoft.VisualStudio.TestTools.UnitTesting;
using Squid_Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Squid_Math.Tests
{
    [TestClass()]
    public class PointTests
    {
        [TestMethod()]
        public void PointTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ToStringTest()
        {
            Point p1 = new Point(7, 5);
            Assert.AreEqual("[7, 5]", p1.ToString());
        }

        [TestMethod()]
        public void EqualsTest()
        {
            Point p1 = new Point(4, 9);
            var p2 = new Point(4, 9);
            Point p3 = new Point(4, 9);
            bool test = p1 == p3;
            Assert.IsTrue(test);
        }

        [TestMethod()]
        public void GetHashCodeTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CompareToTest()
        {
            Assert.Fail();
        }
    }
}
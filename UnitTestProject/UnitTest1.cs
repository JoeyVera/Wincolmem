using System;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wincolmem;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestChangeColour()
        {
            Color testColour = Color.FromName("Red");
            testColour = Form1.ChangeColour(testColour);
            Assert.AreEqual("Green", testColour.Name);
        }
    }
}

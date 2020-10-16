using System;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wincolmem;

namespace UnitTestMenu
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestChangeColour()
        {
            Color testColour = Color.Red;
            testColour = Menu.ChangeColour(testColour);
            Assert.AreEqual(Color.Blue, testColour);
        }

        [TestMethod]
        public void TestInsertHighScore()
        {
            HighScores hss = new HighScores();
            hss.AddHighScore(new HighScore { points = 3, name = "TEST1" });
            Assert.AreEqual(hss[4].points, 2);
        }

        [TestMethod]
        public void TestHighScoreSaving()
        {
            HighScores hss = new HighScores();
            hss.AddHighScore(new HighScore { points = 100, name = "TEST2" });
            hss.SaveHighScores();
            hss.LoadHighScores();
            Assert.AreEqual(hss[0].points, 100);
        }

    }
}

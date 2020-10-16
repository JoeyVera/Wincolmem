using System;
using System.Drawing;
using Game;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestGame
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestGetLevelMap0()
        {
            ColMem colmem = new ColMem();
            Color[,] map = colmem.GetLevelMap(0);
            Assert.AreEqual(4,map.Length);
            bool rightColour = true;

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if ((map[i, j] == Color.Red || map[i, j] == Color.Green) && rightColour)
                        rightColour = true;
                    else
                        rightColour = false;

                    Assert.AreEqual(true, rightColour);
                }
            }
        }

        [TestMethod]
        public void TestGetLevelMap1()
        {
            ColMem colmem = new ColMem();
            Color[,] map = colmem.GetLevelMap(1);
            Assert.AreEqual(16, map.Length);

            bool rightColour = true;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if ((map[i, j] == Color.Red || map[i, j] == Color.Green) && rightColour)
                        rightColour = true;
                    else
                        rightColour = false;

                    Assert.AreEqual(true, rightColour);
                }
            }

        }

        [TestMethod]
        public void TestGetLevelMap2()
        {
            ColMem colmem = new ColMem();
            Color[,] map = colmem.GetLevelMap(2);
            Assert.AreEqual(16, map.Length);
            

            bool gotRed = false;
            bool gotGreen = false;
            bool gotOrange = false;
            bool gotBlue = false;

            bool rightColour = true;

            string colourName;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    colourName = map[i, j].Name;
                    switch (colourName)
                    {
                        case "Red":
                            gotRed = true;
                            break;
                        case "Green":
                            gotGreen = true;
                            break;
                        case "Orange":
                            gotOrange = true;
                            break;
                        case "Blue":
                            gotBlue = true;
                            break;
                        default:
                            rightColour = false;
                            break;
                    }
                }
            }

            Assert.AreEqual(true, rightColour);
            Assert.AreEqual(true, gotRed);
            Assert.AreEqual(true, gotGreen);
            Assert.AreEqual(true, gotOrange);
            Assert.AreEqual(true, gotBlue);

        }
    }
}

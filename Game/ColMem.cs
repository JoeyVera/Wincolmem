using System;
using System.Collections.Generic;
using System.Drawing;

namespace Game
{
    public struct Level
    {
        public int colours;
        public int dimension;
        public int points;
        public int timeInSecs;
    }

    public class ColMem
    {
        private static Level[] levels;
        private static Color[] arrayOfColours;
        private static Color[,] map;        

        public ColMem()
        {
            InitializeArrayOfColours();
            InitializeLevels();
        }

        private static void InitializeArrayOfColours()
        {
            arrayOfColours = new Color[8];
            arrayOfColours[0] = Color.Red;
            arrayOfColours[1] = Color.Green;
            arrayOfColours[2] = Color.Orange;
            arrayOfColours[3] = Color.Blue;
            arrayOfColours[4] = Color.Brown;
            arrayOfColours[5] = Color.Cyan;
            arrayOfColours[6] = Color.Black;
            arrayOfColours[7] = Color.White;
        }

        private static void InitializeLevels()
        {
            levels = new Level[4];

            levels[0] = new Level() { colours = 2, dimension = 2, points = 10, timeInSecs = 10 };
            levels[1] = new Level() { colours = 2, dimension = 4, points = 20, timeInSecs = 28 };
            levels[2] = new Level() { colours = 4, dimension = 4, points = 30, timeInSecs = 50 };
            levels[3] = new Level() { colours = 8, dimension = 4, points = 40, timeInSecs = 75 };
        }

        public Level GetLevel(int level)
        {
            return levels[level];
        }

        public Color[,] GetLevelMap(int level)
        {
            int dimension = levels[level].dimension;
            int colours = levels[level].colours;

            List<Color> cards = GetListOfCards(dimension * dimension, colours);
            cards.Shuffle();

            map = new Color[dimension, dimension];

            for(int i = 0; i < dimension; i++)
            {
                for (int j = 0; j < dimension; j++)
                {
                    map[i, j] = cards[0];
                    cards.RemoveAt(0);
                }                
            }

            return map;
        }

        private static List<Color> GetListOfCards(int numberOfCards, int numberOfColours)
        {
            List<Color> listOfCards = new List<Color>();

            for (int i = 0; i < numberOfCards; i++)
            {
                listOfCards.Add(arrayOfColours[i* numberOfColours / numberOfCards]);
            }

            return listOfCards;
        }
    }
    
    static class MyExtensions
    {
        private static Random rng = new Random();

        // I borrowed this method from stack overflow
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }

}

using _3GenAlg.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace _3GenAlg.Operators
{
    internal class RandomMapGenerator : IMapGenerator
    {
        private Random rnd;

        private int CityCount;
        private int Min;
        private int Max;

        public RandomMapGenerator(int min, int max, int cityCount, Random rnd)
        {
            this.rnd = rnd;
            CityCount = cityCount;
            Min = min;
            Max = max;
        }

        public List<(double x, double y)> Generate()
        {

            List<(double x, double y)> coordinates = new List<(double x, double y)>();

            for (int i = 0; i < CityCount; i++)
            {
                double x = Min + rnd.NextDouble() * (Max - Min);
                double y = Min + rnd.NextDouble() * (Max - Min);

                coordinates.Add((x, y));
            }
            return coordinates;
        }
    }
}

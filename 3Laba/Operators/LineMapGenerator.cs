using _3GenAlg.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace _3GenAlg.Operators
{
    internal class LineMapGenerator : IMapGenerator
    {
        private readonly int _cityCount;

        public LineMapGenerator(int cityCount)
        {
            _cityCount = cityCount;
        }

        public List<(double x, double y)> Generate()
        {
            List<(double x, double y)> coordinates = new List<(double x, double y)>();

            for (int i = 0; i < _cityCount; i++)
            {
                coordinates.Add((i, 0.0));
            }

            return coordinates;
        }
    }
}

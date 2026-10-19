using _3GenAlg.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace _3GenAlg.Core
{
    internal class Map
    {
        public List<(double x, double y)> Coordinates {  get; private set; }
        public double[,] DistanceMatrix { get; private set; }

        public IReadOnlyList<(double x, double y)> Cities => Coordinates;

        // конструктор создания матрицы расстояний или линии
        public Map(IMapGenerator mapGenerator)
        {

            Coordinates = mapGenerator.Generate();

            int n = Coordinates.Count();
            
            DistanceMatrix = new double[n,n];

            for(int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    double x1 = Coordinates[i].x - Coordinates[j].x;
                    double y1 = Coordinates[i].y - Coordinates[j].y;
                    DistanceMatrix[i,j] = Math.Sqrt(Math.Pow(x1, 2) + Math.Pow(y1, 2));
                }
            }
        }
        public double GetDistance(int i, int j)
        {
            return DistanceMatrix[i,j];
        }
    }
}

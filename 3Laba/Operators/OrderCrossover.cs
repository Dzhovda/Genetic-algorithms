using _3GenAlg.Core;
using _3GenAlg.Interfaces;
using System;

namespace _3GenAlg.Operators
{
    internal class OrderCrossover : ICrossover
    {
        private readonly Random _rnd;

        public OrderCrossover(Random rnd)
        {
            _rnd = rnd;
        }

        public (Individual, Individual) Crossover(Individual parent1, Individual parent2)
        {
            int length = parent1.Route.Length;
            int crossStart = _rnd.Next(0, length - 1);
            int crossFin = _rnd.Next(crossStart + 1, length);

            int[] childRoute1 = new int[length];
            int[] childRoute2 = new int[length];

            childRoute1 = SingleCrossover(crossStart, crossFin, childRoute1,
                                          parent1.Route, parent2.Route);
            childRoute2 = SingleCrossover(crossStart, crossFin, childRoute2,
                                          parent2.Route, parent1.Route);

            Individual child1 = new Individual(childRoute1);
            Individual child2 = new Individual(childRoute2);

            return (child1, child2);
        }

        private int[] SingleCrossover(int crossStart, int crossFin, int[] childArray,
                                      int[] parentArray1, int[] parentArray2)
        {
            int length = childArray.Length;
            bool[] usedCities = new bool[length];

            for (int i = crossStart; i <= crossFin; i++)
            {
                childArray[i] = parentArray1[i];
                usedCities[parentArray1[i]] = true;
            }

            // Заполнить свободные позиции по кругу, начиная с crossFin+1
            int parentCounter = (crossFin + 1) % length;   // откуда читать в parent2
            int childCounter = (crossFin + 1) % length;    // куда писать в childArray
            int addedCount = crossFin - crossStart + 1;    // сколько городов уже добавлено
            int targetCount = length;                       // сколько всего нужно

            while (addedCount < targetCount)
            {
                int city = parentArray2[parentCounter];

                if (!usedCities[city])
                {
                    childArray[childCounter] = city;
                    usedCities[city] = true;

                    childCounter = (childCounter + 1) % length;
                    addedCount++;
                }

                parentCounter = (parentCounter + 1) % length;
            }

            return childArray;
        }
    }
}
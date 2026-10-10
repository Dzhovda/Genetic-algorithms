using _2GenAlgEx2.Core;
using _2GenAlgEx2.Interfaces;
using System;
using System.Collections.Generic;

namespace _2GenAlgEx2.Operators
{
    internal class CycleCrossover : ICrossover
    {
        public CycleCrossover() { }

        public (Individual, Individual) Crossover(Individual parent1, Individual parent2)
        {
            int length = parent1.Route.Length;

            int[] childRoute1 = new int[length];
            int[] childRoute2 = new int[length];

            bool[] visited = new bool[length];

            // 1. Строим позицию каждого города в parent1 для быстрого поиска
            int[] positionInParent1 = new int[length];
            for (int i = 0; i < length; i++)
                positionInParent1[parent1.Route[i]] = i;

            // 2. Проходим по всем позициям, для каждой непосещённой строим цикл
            for (int start = 0; start < length; start++)
            {
                if (visited[start])
                    continue;

                // Строим цикл
                List<int> cycle = new List<int>();
                int current = start;

                while (!visited[current])
                {
                    visited[current] = true;
                    cycle.Add(current);

                    // Берём город из parent2 на текущей позиции
                    int cityFromParent2 = parent2.Route[current];

                    // Находим, где этот город в parent1
                    current = positionInParent1[cityFromParent2];
                }

                // 3. Распределяем позиции цикла между детьми
                //    Индекс цикла чередуется: 0 → child1 из P1, 1 → child1 из P2, и т.д.
                for (int k = 0; k < cycle.Count; k++)
                {
                    int pos = cycle[k];
                    bool takeFromParent1 = (k % 2 == 0);

                    if (takeFromParent1)
                    {
                        childRoute1[pos] = parent1.Route[pos];
                        childRoute2[pos] = parent2.Route[pos];
                    }
                    else
                    {
                        childRoute1[pos] = parent2.Route[pos];
                        childRoute2[pos] = parent1.Route[pos];
                    }
                }
            }

            Individual child1 = new Individual(childRoute1);
            Individual child2 = new Individual(childRoute2);

            return (child1, child2);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace _2GenAlgEx2.Core
{
    internal class Population
    {
        public List<Individual> Individuals { get; private set; }

        public Map map { get; private set; }

        public Population(Map map, int populationCount, Random rnd)
        {
            this.map = map;
            Individuals = new List<Individual>();
            //создание индивида популяции
            for (int i = 0; i < populationCount; i++)
            {
                //создать кортеж с путем для individual
                int[] route = new int[map.Coordinates.Count()];

                route = RouteCreate(map.Coordinates.Count(), rnd);

                // высчитать фитнес
                Individual individual = new Individual(route);
                Individuals.Add(individual);
            }
            EvaluateAll();
        }
        private int[] RouteCreate(int cityCount, Random rnd)
        {
            int[] route = new int[cityCount];
            for(int i = 0; i < route.Length; i++)
            {
                route[i] = i;
            }
            for(int i = route.Length - 1; i > 0; i--)
            {
                int box = route[i];
                int j = rnd.Next(0, i + 1);
                route[i] = route[j];
                route[j] = box;
            }
            return route;
        }
        public void EvaluateAll()
        {
            for (int i = 0; i < Individuals.Count; i++)// счетчик индивидов
            {
                double Distance = 0.0;
                for (int j = 0; j < Individuals[i].Route.Length; j++)// счетчик массива внутри индивида
                { 
                    int x = Individuals[i].Route[j];
                    int y = 0;
                    if (j + 1 >= Individuals[i].Route.Length)
                    {
                        y = Individuals[i].Route[0];
                    }
                    else
                    {
                        y = Individuals[i].Route[j + 1];
                    }

                    Distance += map.GetDistance(x, y);
                }
                Individuals[i].Fitness = Distance;
            }
        }
        public void Replace(List<Individual> newIndividuals)
        {
            Individuals.Clear();
            Individuals.AddRange(newIndividuals);
        }

        public Individual GetBest()
        {
            Individual best = Individuals[0];
            for (int i = 1; i < Individuals.Count; i++)
            {
                if (Individuals[i].Fitness < best.Fitness)
                    best = Individuals[i];
            }
            return best;
        }
        public void Sort()
        {
            Individuals.Sort((a, b) => a.Fitness.CompareTo(b.Fitness));
        }
    }
}
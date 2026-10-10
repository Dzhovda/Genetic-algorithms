using _2GenAlgEx2.Core;
using _2GenAlgEx2.Interfaces;
using _2GenAlgEx2.Operators;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2GenAlgEx2.Algorithm
{
    internal class GeneticAlgorithm
    {
        private readonly Population _population;
        private readonly IElitism _elitism;
        private readonly ICrossover _crossover;
        private readonly IMutation _mutation;
        private readonly ISelector _selector;
        public event Action<int, Population> GenerationCompleted;

        public GeneticAlgorithm(Population population, IElitism elitism,ISelector selector, IMutation mutation, ICrossover crossover)
        {
            _population = population;
            _elitism = elitism;
            _selector = selector;
            _mutation = mutation;
            _crossover = crossover;
        }
        public Individual Run(int evolutionCount)
        {
            for (int i = 0; i < evolutionCount; i++)
            {
                // поколения созданы, карта создана
                // вычисление фитнеса
                _population.EvaluateAll();
                // сортировка по возрастанию
                _population.Sort();

                GenerationCompleted?.Invoke(i, _population);

                // элита
                List<Individual> newPopulation = _elitism.SelectElite(_population.Individuals);
                while (newPopulation.Count < _population.Individuals.Count)
                {
                    Individual parent1 = _selector.Select(_population.Individuals);
                    Individual parent2 = _selector.Select(_population.Individuals);

                    (Individual child1, Individual child2) = _crossover.Crossover(parent1, parent2);

                    _mutation.Mutate(child1);
                    _mutation.Mutate(child2);

                    newPopulation.Add(child1);

                    if(newPopulation.Count < _population.Individuals.Count)
                    {
                        newPopulation.Add(child2);
                    }
                }

                _population.Replace(newPopulation);
            }
            _population.EvaluateAll();
            return _population.GetBest();
        }
    }
}

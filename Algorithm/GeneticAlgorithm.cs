using _1GenAlg2ex.Core;
using _1GenAlg2ex.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace _1GenAlg2ex.Algorithm
{
    internal class GeneticAlgorithm
    {
        public List<int> GenerationHistory { get; } = new List<int>();

        public event Action<int, Population> GenerationCompleted;

        public List<double> BestFitnessHistory { get; } = new List<double>();
        private readonly Population _population;
        private readonly ICrossover _crossover;
        private readonly IMutation _mutation;
        private readonly ISelector _selector;
        private readonly Random _rnd;
        private readonly int _eliteCount;

        public GeneticAlgorithm(Population population, ICrossover crossover,
                                IMutation mutation, ISelector selector,
                                Random rnd, int eliteCount = 2)
        {
            _population = population;
            _crossover = crossover;
            _mutation = mutation;
            _selector = selector;
            _rnd = rnd;
            _eliteCount = eliteCount;
        }

        public Individual Run(int generations, int reportEvery = 10)
        {
            for (int gen = 0; gen < generations; gen++)
            {
                // 1. Оценить популяцию
                _population.EvaluateAll();
                // 2. Отсортировать
                _population.Sort();

                GenerationHistory.Add(gen);
                BestFitnessHistory.Add(_population.Individuals[0].Fitness);

                if (gen % reportEvery == 0)
                    GenerationCompleted?.Invoke(gen, _population);

                // 3. Взять элиту
                List<Individual> newGeneration = new List<Individual>();

                for (int i = 0; i < _eliteCount; i++)
                    newGeneration.Add(_population.Individuals[i]);
                // 4. Создать потомков через селекцию, кроссовер, мутацию
                while (newGeneration.Count < _population.Size)
                {
                    // 1. Выбрать двух родителей через _selector.Select(...)
                    Individual parent1 = _selector.Select(_population.Individuals);
                    Individual parent2 = _selector.Select(_population.Individuals);

                    // 2. Скрестить их через _crossover.Crossover(...)
                    var (childGenotype1, childGenotype2) = _crossover.Crossover(parent1.Genotype, parent2.Genotype, _rnd);

                    // 3. Мутировать обоих потомков через _mutation.Mutate(...)
                    childGenotype1 = _mutation.Mutate(childGenotype1, _rnd);
                    childGenotype2 = _mutation.Mutate(childGenotype2, _rnd);

                    Individual child1 = new Individual(childGenotype1);
                    Individual child2 = new Individual(childGenotype2);

                    GenerationHistory.Add(gen);
                    BestFitnessHistory.Add(_population.Individuals[0].Fitness);

                    // 4. Добавить потомков в newGeneration
                    newGeneration.Add(child1);
                    if (newGeneration.Count < _population.Size)
                        newGeneration.Add(child2);
                }
                _population.Replace(newGeneration);
            }
            _population.EvaluateAll();
            return _population.GetBest();
        }

        public List<Population> GetFinalPopulationX()
        {
            List<Population> pop = new List<Population>();
            return pop;
        }
        public List<Population> GetFinalPopulationY()
        {
            List<Population> pop = new List<Population>();
            return pop;
        }
    }
}

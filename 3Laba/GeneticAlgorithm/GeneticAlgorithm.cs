using _3GenAlg.Core;
using _3GenAlg.Interfaces;
using _3GenAlg.Operators;
using System;
using System.Collections.Generic;
using System.Text;

namespace _3GenAlg.Algorithm
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
		public void RunFor(int generations)
		{
			for (int gen = 0; gen < generations; gen++)
			{
				_population.EvaluateAll();
				_population.Sort();

				GenerationCompleted?.Invoke(gen, _population);

				List<Individual> newPopulation = _elitism.SelectElite(_population.Individuals);

				while (newPopulation.Count < _population.Individuals.Count)
				{
					Individual parent1 = _selector.Select(_population.Individuals);
					Individual parent2 = _selector.Select(_population.Individuals);

					(Individual child1, Individual child2) = _crossover.Crossover(parent1, parent2);

					_mutation.Mutate(child1);
					_mutation.Mutate(child2);

					newPopulation.Add(child1);
					if (newPopulation.Count < _population.Individuals.Count)
						newPopulation.Add(child2);
				}

				_population.Replace(newPopulation);
			}
		}

		public Individual Run(int evolutionCount)
		{
			RunFor(evolutionCount);
			_population.EvaluateAll();
			return _population.GetBest();
		}

	}
}

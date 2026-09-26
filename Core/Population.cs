using _1GenAlg2ex.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace _1GenAlg2ex.Core
{
    internal class Population
    {
        private readonly int _bitsPerGene;
        private readonly List<(double Min, double Max)> _bounds;
        private readonly Random _rnd;
        public int Size { get; }

        private readonly IFitnessFunction _fitnessFunction;

        public List<Individual> Individuals { get; private set; } = new List<Individual>();


        public Population(int populationCount, int bitsPerGene, List<(double Min, double Max)> bounds, IFitnessFunction fitnessFunction, Random rnd)
        {
            _bounds = bounds;
            _rnd = rnd;
            _bitsPerGene = bitsPerGene;
            Size = populationCount;
            _fitnessFunction = fitnessFunction;
            
            for(int i = 0; i < populationCount; i++)
            {
                Genotype genotype = new Genotype(bitsPerGene, bounds, rnd);
                Individual individual = new Individual(genotype);
                individual.Fitness = _fitnessFunction.Evaluate(individual.Phenotype);

                Individuals.Add(individual);
            }
        }
        public void EvaluateAll()// считает Fitness
        {
            for(int i = 0; i < Individuals.Count; i++)
            {
                Individuals[i].Fitness = _fitnessFunction.Evaluate(Individuals[i].Phenotype);
            }
        }
        public Individual GetBest()
        {
            double best = Individuals[0].Fitness;
            int iBest = 0;
            for (int i = 0; i < Individuals.Count; i++)
            {
                if (Individuals[i].Fitness < best)
                {
                    best = Individuals[i].Fitness;
                    iBest = i;
                }
            }
            return Individuals[iBest];
        }
        public void Sort()
        {
            Individuals.Sort((a, b) => a.Fitness.CompareTo(b.Fitness));
        }
        public void AddIndividual(Genotype genotype)
        {
            Individual individual = new Individual(genotype);
            individual.Fitness = _fitnessFunction.Evaluate(individual.Phenotype);
            Individuals.Add(individual);
        }
        public void Replace(List<Individual> newIndividuals)
        {
            Individuals.Clear();
            Individuals.AddRange(newIndividuals);
        }
    }
}

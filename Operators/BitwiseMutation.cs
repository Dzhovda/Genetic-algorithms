using _1GenAlg2ex.Core;
using _1GenAlg2ex.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace _1GenAlg2ex.Operators
{
    internal class BitwiseMutation : IMutation
    {
        private readonly double _mutationRate;

        public BitwiseMutation(double mutationRate = 0.01)
        {
            _mutationRate = mutationRate;
        }
        public Genotype Mutate(Genotype child, Random rnd)
        {
            for (int i = 0; i < child.Chromosome.Count; i++) 
            {
                if (rnd.NextDouble() < _mutationRate)
                {
                    child.Chromosome[i] = !child.Chromosome[i];
                }
            }
            return child;
        }
    }
}

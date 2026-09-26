using _1GenAlg2ex.Core;
using _1GenAlg2ex.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace _1GenAlg2ex.Operators
{
    internal class OneBitMutation : IMutation
    {
        public Genotype Mutate(Genotype child, Random rnd)
        {
            int point = rnd.Next(child.Chromosome.Count);
            if (rnd.NextDouble() < 0.05)   // сработает примерно в 5% случаев
            {
                child.Chromosome[point] = !child.Chromosome[point];
            }
            return child;
        }
    }
}

using _1GenAlg2ex.Core;
using _1GenAlg2ex.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace _1GenAlg2ex.Operators
{
    internal class TwoPointCrossover : ICrossover
    {
        public (Genotype, Genotype) Crossover(Genotype parent1, Genotype parent2, Random rnd)
        {
            int point1 = rnd.Next(1, parent1.Chromosome.Count);
            int point2 = rnd.Next(point1, parent1.Chromosome.Count);
            List<bool> childList1 = new List<bool>();
            List<bool> childList2 = new List<bool>();

            for(int i = 0; i < point1; i++)
            {
                childList1.Add(parent1.Chromosome[i]);
                childList2.Add(parent2.Chromosome[i]);
            }
            for(int i = point1;i < point2; i++)
            {
                childList1.Add(parent2.Chromosome[i]);
                childList2.Add(parent1.Chromosome[i]);
            }
            for(int i = point2; i < parent1.Chromosome.Count; i++)
            {
                childList1.Add(parent1.Chromosome[i]);
                childList2.Add(parent2.Chromosome[i]);
            }
            Genotype child1 = new Genotype(childList1, parent1._bitsPerGene, parent1.getBounds());
            Genotype child2 = new Genotype(childList2, parent1._bitsPerGene, parent1.getBounds());
            return (child1, child2);
        }
    }
}

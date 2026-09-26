using _1GenAlg2ex.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace _1GenAlg2ex.Interfaces
{
    internal interface ICrossover
    {

        (Genotype, Genotype) Crossover(Genotype parent1, Genotype parent2, Random rnd);

    }
}

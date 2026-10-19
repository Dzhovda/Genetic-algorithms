using _3GenAlg.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace _3GenAlg.Interfaces
{
    internal interface ICrossover
    {
        public (Individual, Individual) Crossover(Individual parent1, Individual parent2);
    }
}

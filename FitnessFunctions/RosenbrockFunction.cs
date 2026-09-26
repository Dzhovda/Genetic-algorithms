using _1GenAlg2ex.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace _1GenAlg2ex.FitnessFunctions
{
    internal class RosenbrockFunction : IFitnessFunction
    {
        public double Evaluate(IReadOnlyList<double> phenotype)
        {
            double x = phenotype[0];
            double y = phenotype[1];
            return Math.Pow(1 - x, 2) + 100 * Math.Pow(y - x * x, 2);
        }
    }
}

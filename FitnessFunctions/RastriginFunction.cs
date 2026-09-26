using _1GenAlg2ex.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace _1GenAlg2ex.FitnessFunctions
{
    internal class RastriginFunction : IFitnessFunction
    {
        public double Evaluate(IReadOnlyList<double> phenotype)
        {
            double x = phenotype[0];
            double y = phenotype[1];
            return 20 + (Math.Pow(x, 2) - 10 * Math.Cos(2 * 3.14 * x)) + (Math.Pow(y, 2) - 10 * Math.Cos(2 * 3.14 * y));
        }
    }
}

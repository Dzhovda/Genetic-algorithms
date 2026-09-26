using _1GenAlg2ex.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace _1GenAlg2ex.FitnessFunctions
{
    internal class AckleyFunction : IFitnessFunction
    {
        public double Evaluate(IReadOnlyList<double> phenotype)
        {
            double x = phenotype[0];
            double y = phenotype[1];

            // Первая часть: -20 * exp(-0.2 * sqrt(0.5 * (x² + y²)))
            double term1 = -20 * Math.Exp(-0.2 * Math.Sqrt(0.5 * (x * x + y * y)));

            // Вторая часть: -exp(0.5 * (cos(2πx) + cos(2πy)))
            double term2 = -Math.Exp(0.5 * (Math.Cos(2 * Math.PI * x) + Math.Cos(2 * Math.PI * y)));

            // Третья часть: +20 + e
            return term1 + term2 + 20 + Math.E;
        }
    }
}

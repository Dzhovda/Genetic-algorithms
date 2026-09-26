using System;
using System.Collections.Generic;
using System.Text;

namespace _1GenAlg2ex.Interfaces
{
    internal interface IFitnessFunction
    {
        double Evaluate(IReadOnlyList<double> phenotype);
    }
}

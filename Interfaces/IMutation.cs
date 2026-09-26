using _1GenAlg2ex.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace _1GenAlg2ex.Interfaces
{
    internal interface IMutation
    {
        Genotype Mutate(Genotype child, Random rnd);
    }
}

using _1GenAlg2ex.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace _1GenAlg2ex.Interfaces
{
    internal interface ISelector
    {
        Individual Select(List<Individual> individuals);
    }
}

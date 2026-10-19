using _3GenAlg.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace _3GenAlg.Interfaces
{
    internal interface ISelector
    {
        Individual Select(List<Individual> individuals);
    }
}

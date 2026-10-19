using _3GenAlg.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace _3GenAlg.Interfaces
{
    internal interface IElitism
    {
        public List<Individual> SelectElite(List<Individual> sortedIndividuals);
    }
}

using _3GenAlg.Core;
using _3GenAlg.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace _3GenAlg.Operators
{
    internal class NoElitism : IElitism
    {
        public List<Individual> SelectElite(List<Individual> sortedIndividuals)
        {
            return new List<Individual>();
        }
    }
}

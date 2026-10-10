using _2GenAlgEx2.Core;
using _2GenAlgEx2.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2GenAlgEx2.Operators
{
    internal class NoElitism : IElitism
    {
        public List<Individual> SelectElite(List<Individual> sortedIndividuals)
        {
            return new List<Individual>();
        }
    }
}

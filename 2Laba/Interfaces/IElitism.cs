using _2GenAlgEx2.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2GenAlgEx2.Interfaces
{
    internal interface IElitism
    {
        public List<Individual> SelectElite(List<Individual> sortedIndividuals);
    }
}

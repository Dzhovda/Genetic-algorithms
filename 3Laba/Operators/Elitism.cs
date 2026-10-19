using _3GenAlg.Core;
using _3GenAlg.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace _3GenAlg.Operators
{
    internal class Elitism : IElitism
    {
        private int _count = 0;
        public Elitism(int count)
        {
            _count = count;
        }
        public List<Individual> SelectElite(List<Individual> sortedIndividuals)
        {
            int take = Math.Min(_count, sortedIndividuals.Count);
            List<Individual> list = new List<Individual>();
            for (int i = 0; i < take; i++)
            {
                list.Add(sortedIndividuals[i]);
            }
            return list;
        }
    }
}

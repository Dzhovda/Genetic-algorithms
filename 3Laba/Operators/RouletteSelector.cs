using _3GenAlg.Core;
using _3GenAlg.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace _3GenAlg.Operators
{
    internal class RouletteSelector : ISelector
    {
        private Random _rnd;

        public RouletteSelector(Random rnd)
        {
            _rnd = rnd;
        }

        public Individual Select(List<Individual> individuals)
        {
            List<double> Chances = new List<double>();
            double Weight = 0;
            double TotalWeight = 0;
            double ChanceCount = 0;
            int WinnerId = 0;
            for (int i = 0; i < individuals.Count; i++) 
            {
                Weight = 1 / individuals[i].Fitness;
                Chances.Add(Weight);
                TotalWeight += Weight;
            }

            for(int i = 0;i < Chances.Count; i++)
            {
                Chances[i] = Chances[i] / TotalWeight;
            }

            double Chance = _rnd.NextDouble();
            for (int i = 0; i < Chances.Count; i++)
            {
                ChanceCount += Chances[i];
                if (ChanceCount >= Chance)
                {
                    WinnerId = i;
                    break;
                }
            }
            return individuals[WinnerId];
        }
    }
}

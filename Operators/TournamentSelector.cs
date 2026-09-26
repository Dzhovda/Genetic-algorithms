using _1GenAlg2ex.Core;
using _1GenAlg2ex.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace _1GenAlg2ex.Operators
{
    internal class TournamentSelector : ISelector
    {
        private Random rnd;

        private int tournamentSize;
        public TournamentSelector(Random rnd, int tournamentSize)
        {
            this.rnd = rnd;
            this.tournamentSize = tournamentSize;
        }

        public Individual Select(List<Individual> individuals)
        {
            List<Individual> filtered = new List<Individual>();
            int iBest = 0;

            for (int i = 0; i < tournamentSize; i++)
            {
                int iRnd = rnd.Next(individuals.Count);
                filtered.Add(individuals[iRnd]);
            }

            double best = filtered[0].Fitness;

            for (int i = 0; i < filtered.Count; i++)
            {
                if (filtered[i].Fitness < best)
                {
                    best = filtered[i].Fitness;
                    iBest = i;
                }
            }

            return filtered[iBest];
        }
    }
}

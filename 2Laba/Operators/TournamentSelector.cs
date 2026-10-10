using _2GenAlgEx2.Core;
using _2GenAlgEx2.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2GenAlgEx2.Operators
{
    internal class TournamentSelector : ISelector
    {
        private Random _rnd;
        private int TournamentSize;

        public TournamentSelector(int tournamentSize, Random rnd)
        {
            _rnd = rnd;
            TournamentSize = tournamentSize;
        }

        public Individual Select(List<Individual> individuals)
        {
            
            double BestFitness = double.MaxValue;
            int BestId = 0;
            for (int i = 0; i < TournamentSize; i++)
            {
                int id = _rnd.Next(individuals.Count);

                if(BestFitness > individuals[id].Fitness)
                {
                    BestFitness = individuals[id].Fitness;
                    BestId = id;
                }
            }
            return individuals[BestId];
        }
    }
}

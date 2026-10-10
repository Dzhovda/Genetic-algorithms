using _2GenAlgEx2.Core;
using _2GenAlgEx2.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2GenAlgEx2.Operators
{
    internal class SwapMutation : IMutation
    {
        private Random _rnd;
        private double _mutationRate = 0.01;

        public SwapMutation(double mutationRate, Random rnd)
        {
            _mutationRate = mutationRate;
            _rnd = rnd;
        }

        public void Mutate(Individual individual)
        {
            double chanceForMutation = _rnd.NextDouble();
            if(chanceForMutation < _mutationRate)
            {
                int cityId1 = _rnd.Next(0, individual.Route.Length);
                int cityId2 = _rnd.Next(0, individual.Route.Length);
                int tmpCity = 0;

                tmpCity = individual.Route[cityId1];
                individual.Route[cityId1] = individual.Route[cityId2];
                individual.Route[cityId2] = tmpCity;
            }
        }
    }
}

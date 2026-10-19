using _3GenAlg.Core;
using _3GenAlg.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace _3GenAlg.Operators
{
    internal class InversionMutation : IMutation
    {
        private Random _rnd;
        private double _mutationRate = 0.01;

        public InversionMutation(double mutationRate, Random rnd)
        {
            _mutationRate = mutationRate;
            _rnd = rnd;
        }

        public void Mutate(Individual individual)
        {
            if (_rnd.NextDouble() >= _mutationRate)
                return;

            int length = individual.Route.Length;

            int i = _rnd.Next(0, length - 1);
            int j = _rnd.Next(i + 1, length);

            int left = i;
            int right = j;

            while (left < right)
            {
                int tmp = individual.Route[left];
                individual.Route[left] = individual.Route[right];
                individual.Route[right] = tmp;

                left++;
                right--;
            }
        }
    }
}

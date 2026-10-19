using System;
using System.Collections.Generic;
using System.Text;

namespace _3GenAlg.Core
{
    internal class Individual
    {
        public int[] Route {  get; private set; }
        public double Fitness {get; set;}

        public Individual(int[] route, double fitness = 0.0)
        {
            Route = route;
            Fitness = fitness;
        }
    }
}

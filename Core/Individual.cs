using System;
using System.Collections.Generic;
using System.Text;
////summery
/// Представляет особь генетического алгоритма: хранит генотип, фенотип и значение фитнеса.
////summery
namespace _1GenAlg2ex.Core
{
    internal class Individual
    {
        public Genotype Genotype { get; private set; }
        public List<double> Phenotype { get; private set; } 
        public double Fitness { get; set; }              

        public Individual(Genotype genotype)
        {
            Genotype = genotype;
            Phenotype = genotype.Decode(); 
            Fitness = 0.0;                  
        }
    }
}

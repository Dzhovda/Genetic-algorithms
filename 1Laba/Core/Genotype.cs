using System;
using System.Collections.Generic;
using System.Text;

namespace _1GenAlg2ex.Core
{
    internal class Genotype
    {
        private List<(double Min, double Max)> _bounds = new List<(double Min, double Max)>();

        public int BitsPerGene { get; private set; }

        public List<bool> Chromosome { get; private set; }

        public int GeneCount => _bounds.Count;

        public int TotalBits => GeneCount * BitsPerGene;

        public List<(double min, double max)> getBounds()
        {
            return _bounds;
        }
        public Genotype(List<bool> chromosome, int bitsPerGene, List<(double min, double max)> bounds)
        {
            Chromosome = chromosome;
            BitsPerGene = bitsPerGene;
            _bounds = bounds; 
        }
        public Genotype(int bitsPerGene, List<(double Min, double Max)> bounds, Random rnd)
        {
            BitsPerGene = bitsPerGene;
            _bounds = bounds;

            Chromosome = new List<bool>(GeneCount * bitsPerGene);
            for (int i = 0; i < TotalBits; i++)
                Chromosome.Add(rnd.NextDouble() < 0.5);
        }
        public List<double> Decode()
        {
            int counter = 0;
            int pow = BitsPerGene -1;
            double total = 0;
            double Masshtab = (Math.Pow(2, BitsPerGene) - 1); // масштабирование максимальное значение битовой строки - 1(0 до 2^n - 1) / 100(один процент) 
            List<double> phenotype = new List<double>();
            for (int i = 0; i < Chromosome.Count(); i++) {
                if (Chromosome[i] == true)
                {
                    total += Math.Pow(2, pow);
                    pow--;
                }
                else
                {
                    pow--;
                }
                if(pow < 0)
                {
                    total /= Masshtab;
                    total *= (_bounds[counter].Max - _bounds[counter].Min);
                    total += _bounds[counter].Min;
                    phenotype.Add(total);
                    total = 0;
                    pow = BitsPerGene - 1;
                    counter++;
                }
            }

            return phenotype;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace _1GenAlg2ex.Core // параметры с маленькой буквы, атрибуты _имяатрибута (исправить поля, атрибуты и тд в соответствующей стилистике)
{
    internal class Genotype
    {
        private List<(double Min, double Max)> bounds = new List<(double Min, double Max)> ();

        public int _bitsPerGene { get; private set; }

        public List<bool> Chromosome { get; private set; }

        public int GeneCount => bounds.Count;

        public int TotalBits => GeneCount * _bitsPerGene;

        public List<(double Min, double Max)> getBounds()
        {
            return bounds;
        }
        public Genotype(List<bool> Chromosome, int _bitsPerGene, List<(double Min, double Max)> bounds)
        {
            this.Chromosome = Chromosome;
            this._bitsPerGene = _bitsPerGene;
            this.bounds = bounds; 
        }
        public Genotype(int _bitsPerGene, List<(double Min, double Max)> bounds, Random rnd)
        {
            this._bitsPerGene = _bitsPerGene;
            this.bounds = bounds;

            Chromosome = new List<bool>(GeneCount * _bitsPerGene);
            for (int i = 0; i < TotalBits; i++)
                Chromosome.Add(rnd.NextDouble() < 0.5);
        }
        public List<double> Decode()
        {
            int counter = 0;
            int pow = _bitsPerGene -1;
            double total = 0;
            double Masshtab = (Math.Pow(2, _bitsPerGene) - 1); // масштабирование максимальное значение битовой строки - 1(0 до 2^n - 1) / 100(один процент) 
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
                    total *= (bounds[counter].Max - bounds[counter].Min);
                    total += bounds[counter].Min;
                    phenotype.Add(total);
                    total = 0;
                    pow = _bitsPerGene - 1;
                    counter++;
                }
            }

            return phenotype;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace _3GenAlg.Interfaces
{
    internal interface IMapGenerator
    {
        public List<(double x, double y)> Generate();
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace _2GenAlgEx2.Interfaces
{
    internal interface IMapGenerator
    {
        public List<(double x, double y)> Generate();
    }
}

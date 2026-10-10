using _2GenAlgEx2.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2GenAlgEx2.Interfaces
{
    internal interface ISelector
    {
        Individual Select(List<Individual> individuals);
    }
}

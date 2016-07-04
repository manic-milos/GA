using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA
{
    abstract class GenomeCrossBase
    {
        public abstract List<Genome> crossGenomes(List<Genome> parents,
            GeneticAlgorithm ga);
    }
}

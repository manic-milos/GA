using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA
{
    class Random1FlipMutator:MutatorBase
    {
        protected override Genome mutate(Genome g)
        {
            Genome copy = g.Clone();
            int position = copy.geneticAlgorithm.settings.getRandomFromRange(
                0, copy.genes.Length - 1);
            copy[position] = 1 - copy[position];
            return copy.adjust();
        }
    }
}

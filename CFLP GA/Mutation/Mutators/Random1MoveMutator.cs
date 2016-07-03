using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA
{
    class Random1MoveMutator: MutatorBase
    {
        protected override Genome mutate(Genome g)
        {
            Genome copy = g.Clone();
            List<int> ones = new List<int>();
            List<int> zeros = new List<int>();
            for (int i = 0; i < copy.genes.Length; i++)
            {
                if (copy[i] == 1)
                    ones.Add(i);
                else
                    zeros.Add(i);
            }
            int ind1 = copy.geneticAlgorithm.settings.getRandomFromRange(
                0, ones.Count);
            int ind0 = copy.geneticAlgorithm.settings.getRandomFromRange(
                0, zeros.Count);
            copy[ind1] = 0;
            copy[ind0] = 1;
            return copy.adjust();
        }
    }
}

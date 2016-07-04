using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA
{
    class RandomWithPreferenceMutator:MutatorBase
    {
        protected double onePreference = 0.1;
        protected double zeroPreference = 0.1;
        protected override Genome mutate(Genome g)
        {
            Genome g1 = g.Clone();
            for(int i=0;i<g1.genes.Length;i++)
            {
                double p = ControlledRandom.getRandomDouble();
                if (g1[i] == 1)
                {
                    if (p < onePreference)
                    {
                        g1[i] = 1-g1[i];
                    }
                }
                else
                {
                    if (p < zeroPreference)
                    {
                        g1[i] = 1-g1[i];
                    }
                }

            }
            return g1;
        }
    }
}

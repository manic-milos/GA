using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA
{
    class SureRandomMutation:MutationBase
    {
        int howMany;
        protected override void mutate(GenePopulation genePool,GeneticAlgorithm ga)
        {
            for (int i = 0; i < howMany; i++)
            {
                int ind = ga.settings.getRandomFromRange(0, genePool.Count);
                Genome g = genePool.ElementAt(ind);
                Genome g1 = mutator.Mutate(g);
                genePool.Include(g1);
            }
        }
        public SureRandomMutation(MutatorBase mutator,int howMany=1)
            :base(mutator)
        {
            this.howMany = howMany;
        }
    }
}

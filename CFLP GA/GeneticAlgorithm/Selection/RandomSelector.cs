using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA
{
    class RandomSelector:SelectorBase
    {
        public int crossPool = 20;
        public RandomSelector(int crossPool)
        {
            this.crossPool = crossPool;
        }
        protected override GenePopulation select(GenePopulation genePool,
            GeneticAlgorithm ga)
        {
            GenePopulation crossing = new GenePopulation(ga);
            for (int i = 0; i < crossPool; i++)
            {
                crossing.Include(
                    genePool.ElementAt(
                    ControlledRandom.getRandomFromRange(0, genePool.Count)));
            }
            return crossing;
        }
    }
}

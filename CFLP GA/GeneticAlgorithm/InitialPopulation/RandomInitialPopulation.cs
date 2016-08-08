using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA
{
    class RandomInitialPopulation:InitialPopulationBase
    {
        public int size=150;
        public UnfeasableDeciderBase decider;
        public RandomInitialPopulation(int size)
        {
            this.size = size;
            decider = new FactorUnfeasableDecider(size);
        }
        protected override GenePopulation create(GeneticAlgorithm ga)
        {
            GenePopulation Genes = new GenePopulation(ga);
            int i = 0;
            while (i++ < size)
            {
                Genome a;
                a = Genome.generateRandom(ga);
                Genes.Include(a);
                decider.DecideUnfeasable(i, Genes);
            }
            return Genes;
        }
    }
}

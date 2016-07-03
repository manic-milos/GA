using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA
{
    class TournamentSelector:SelectorBase
    {
        public int crossPool = 20;
        public int tournamentSize = 3;
        public bool replacing = true;
        public TournamentSelector(int crossPool,int tournamentSize, bool replacing=false)
        {
            this.crossPool = crossPool;
            this.tournamentSize = tournamentSize;
            this.replacing = replacing;
        }
        protected override GenePopulation select(GenePopulation population,
            GeneticAlgorithm ga)
        {
            Dictionary<int, Genome> candidates = new Dictionary<int, Genome>();
            GenePopulation parents = new GenePopulation();
            int n=0;
            foreach(Genome g in population)
            {
                candidates.Add(n++,g);
            }
            for(int i=0;i<crossPool;i++)
            {
                double max = 0;
                int maxind = 0;
                for(int j=0;j<tournamentSize;j++)
                {
                    int ind = ga.settings.getRandomFromRange(0, n);
                    double fit = candidates[ind].fitness();
                    if(fit>max)
                    {
                        max = fit;
                        maxind = ind;
                    }
                }
                parents.Include(candidates[maxind]);
            }
            return parents;
        }
    }
}

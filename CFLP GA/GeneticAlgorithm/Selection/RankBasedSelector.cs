using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA
{
    class RankBasedSelector:SelectorBase
    {
        public double selectionPreasure = 1;//TODO;
        public int crossPool=20;
        public RankBasedSelector(int crossPool,double selectionPreasure=1.0)
        {
            this.crossPool = crossPool;
            this.selectionPreasure = selectionPreasure;
        }
        protected override GenePopulation select(GenePopulation population, GeneticAlgorithm ga)
        {
            List<Genome> candidates = new List<Genome>();
            foreach(Genome g in population)
            {
                candidates.Add(g);
            }
            candidates.Reverse();
            GenePopulation selected = new GenePopulation(ga);
            for(int i=0;i<crossPool;i++)
            {
                double pick=ControlledRandom.getRandomDouble();
                int ind = 0;
                double current = getRankBasedProbability(ind, candidates.Count);
                while(current<pick)
                {
                    ind++;
                    current += getRankBasedProbability(ind, candidates.Count);
                }
                selected.Include(candidates[ind]);
            }
            return selected;

        }
        public double getRankBasedProbability(int i,int n)
        {
            i++;
            double pi=(2-selectionPreasure)/n+(2*i*(selectionPreasure-1))/(n*n-1);
            return pi;
        }
    }
}

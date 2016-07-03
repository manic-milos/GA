using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA
{
    class RandomAdjuster : GenomeAdjusterBase
    {

        protected override Genome adjust(Genome g, GeneticAlgorithm ga)
        {
            Problem problem = ga.problem;
            if (g.checkGenome())
            {
                return g;
            }

            if (!problem.checkCardinalityContraint(g.genes))
            {
                if (g.sumCapacity() < problem.SumDemands)
                {
                    return null;
                }
                List<int> ones = new List<int>();
                for (int i = 0; i < problem.m; i++)
                {
                    if (g[i] == 1)
                    {
                        g[i] = 0;
                        if (g.sumCapacity() >= problem.SumDemands)
                            ones.Add(i);
                        g[i] = 1;
                    }
                }
                if (ones.Count == 0)
                    return null;
                int ind = ga.settings.getRandomFromRange(0, ones.Count);
                g[ones[ind]] = 0;
                return g.adjust();
            }
            else if (g.sumCapacity() < problem.SumDemands)
            {
                return null;
                //TODO
            }
            return null;
        }
    }
}

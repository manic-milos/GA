using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA
{
    class MinPriceAdjuster : GenomeAdjusterBase
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
                int ind = -1;
                double val = double.MaxValue;
                for (int i = 0; i < problem.m;i++ )
                {
                    if(g[i]==1)
                    {
                        g[i] = 0;
                        if(g.sumCapacity()>=problem.SumDemands)
                        {
                            double fitness = g.fitness();
                            if (fitness < val)
                            {
                                ind = i;
                                val = fitness;
                            }
                        }
                        g[i] = 1;
                    }
                }
                if (ind >= 0)
                {
                    g[ind] = 0;
                    return g.adjust();
                }
                return null;

            }
            else if (g.sumCapacity() < problem.SumDemands)
            {
                //TODO
            }
            return null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA
{
    class MinLeftCapacityAdjuster : GenomeAdjusterBase
    {

        protected override Genome adjust(Genome g,GeneticAlgorithm ga)
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

                int[] capacities = new int[problem.m];
                int[] indexes = new int[problem.m];
                for (int i = 0; i < problem.m; i++)
                {
                    indexes[i] = i;
                }
                ga.fitness(g,capacities);
                Array.Sort(capacities, indexes);
                int ind = problem.m - 1;
                do
                {
                    g[indexes[ind]] = 0;
                    if (g.sumCapacity() < problem.SumDemands)
                    {
                        g[indexes[ind]] = 1;
                        ind--;
                    }
                    else
                        break;
                } while (capacities[ind] >= 0);
                if (capacities[ind] == 0)
                    return null;
                return g.adjust();

            }
            else if (g.sumCapacity() < problem.SumDemands)
            {
                //TODO
            }
            return null;
        }
    }
}

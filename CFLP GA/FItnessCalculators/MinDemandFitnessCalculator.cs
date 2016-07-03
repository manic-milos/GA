using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA
{
    class MinDemandFitnessCalculator : FitnessCalculatorBase
    {

        protected override double fitness(int[] g, 
            Problem p, int[] leftCapacities = null)
        {
            if(p.sumCapacity(g)<p.SumDemands)
            {
                throw new Exception("Nevazece resenje");
            }
            double objf = calculateOpeningCosts(g,p);
            int[] currentCapacities = new int[p.m];
            Array.Copy(p.s, currentCapacities, p.m);
            for (int i = 0; i < p.n; i++)
            {
                int sumDemandGot = 0;
                while (sumDemandGot < p.d[p.sortedDemandIndices[i]])
                {
                    int currentDemand = p.d[p.sortedDemandIndices[i]] - sumDemandGot;
                    int ind = -1;
                    double val = double.MaxValue;
                    for (int j = 0; j < p.m; j++)
                    {
                        if (g[j] != 0 && currentCapacities[j]>0)
                        {
                            if (p.c[j, p.sortedDemandIndices[i]] < val)
                            {
                                ind = j;
                                val = p.c[j, p.sortedDemandIndices[i]];
                            }
                        }

                    }
                    int minItems = (currentDemand < currentCapacities[ind] ?
                        currentDemand : currentCapacities[ind]);
                    objf += (double)val * minItems;
                    sumDemandGot += minItems;
                    currentCapacities[ind] -= minItems;
                }

            }
            if (leftCapacities != null)
            {
                leftCapacities = currentCapacities;
            }
            return objf;
        }
        public MinDemandFitnessCalculator(EvaluationCache cache = null)
            : base(cache)
        { }
    }
}

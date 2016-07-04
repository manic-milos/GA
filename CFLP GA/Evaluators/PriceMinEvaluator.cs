using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA
{
    class PriceMinEvaluator:EvaluatorBase
    {
        protected override double evaluate(int[] g, 
            Problem problem, int[] leftCapacities = null)
        {
            if (problem.sumCapacity(g) < problem.SumDemands)
            {
                throw new Exception("Nevazece resenje");
            }
            double objf=calculateOpeningCosts(g,problem);
            double[,] wholeCost = new double[problem.m, problem.n];
            double sum = 0;
            int[] currentDemands = new int[problem.n];
            Array.Copy(problem.d, currentDemands, problem.n);
            int[] currentCapacities = new int[problem.m];
            Array.Copy(problem.s, currentCapacities, problem.m);
            while (sum < problem.SumDemands)
            {
                double min = double.MaxValue;
                int indi = -1;
                int indj = -1;
                for (int i = 0; i < problem.m; i++)
                {
                    if (g[i] > 0)
                    {
                        for (int j = 0; j < problem.n; j++)
                        {
                            wholeCost[i, j] = (double)problem.c[i, j] * (currentDemands[j] < currentCapacities[i] ? currentDemands[j] : currentCapacities[i]);
                            if (wholeCost[i, j] < min && wholeCost[i, j] > 0)
                            {
                                min = wholeCost[i, j];
                                indi = i;
                                indj = j;
                            }
                        }
                    }
                }
                int quantity = (int)(min / problem.c[indi, indj]);
                currentDemands[indj] -= quantity;
                currentCapacities[indi] -= quantity;
                sum += quantity;
                objf += min;
            }
            if (leftCapacities != null)
            {
                leftCapacities = currentCapacities;
            }
            return objf;
        }
        public PriceMinEvaluator(EvaluationCache cache = null)
            : base(cache)
        { }
    }
}

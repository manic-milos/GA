using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA.IteratedLocalSearch.LocalSearch
{
    class OneFlipLS:LocalSearchBase
    {
        protected override Solution getBestLocal(Solution s,
            FitnessCalculatorBase evaluator)
        {
            Solution copy = s.Clone();
            double minSol=evaluator.Fitness(s);
            int bestI=-1;
            for(int i=0;i<copy.vars.Length;i++)
            {
                copy[i] = 1 - copy[i];
                if(!copy.check())
                {
                    copy[i] = 1 - copy[i];
                    continue;
                }
                double solEval=evaluator.Fitness(copy);
                if(solEval<minSol)
                {
                    minSol = solEval;
                    bestI = i;
                }
                //Console.WriteLine("iter:" + i + " " + copy + solEval);
                copy[i] = 1 - copy[i];
                
            }
            if(bestI>=0)
            {
                copy[bestI] = 1 - copy[bestI];
            }
            return copy;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA
{
    class GenerationLimitCriterion:StoppingCriterionBase
    {
        int i = 0;
        int n = 30;
        public GenerationLimitCriterion(int n=30)
        {
            i = 0;
            this.n = n;
        }
        protected override bool checkStoppingCriterion(GenePopulation genes)
        {
            if (i++ < n)
                return false;
            return true;
        }
        protected override void init(GeneticAlgorithm ga)
        {
            i = 0;
        }

        protected override string currentIteration()
        {
            return i.ToString();
        }
    }
}

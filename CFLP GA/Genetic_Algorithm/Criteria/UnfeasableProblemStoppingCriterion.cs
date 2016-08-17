using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA
{
    class UnfeasableProblemStoppingCriterion:StoppingCriterionBase
    {

        protected override void init(GeneticAlgorithm ga)
        {
            throw new NotImplementedException();
        }

        protected override bool checkStoppingCriterion(GenePopulation genes)
        {
            throw new NotImplementedException();
        }

        protected override string currentIteration()
        {
            return "Problem unfeasable";
        }
    }
}

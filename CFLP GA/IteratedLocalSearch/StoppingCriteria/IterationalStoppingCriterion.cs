using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA.IteratedLocalSearch.StoppingCriteria
{
    class IterationalStoppingCriterion:StoppingCriterionBase
    {
        public int iteration = 0;
        public int numberOfIterations;
        public IterationalStoppingCriterion(int numberOfIterations)
        {
            this.numberOfIterations = numberOfIterations;
            iteration++;
        }
        protected override bool checkIfEnd(Solution s, Solution globalBest)
        {
            iteration++;
            if(iteration<numberOfIterations)
            {
                return false;
            }
            return true;
        }

        public override string IterationInfo()
        {
            return iteration+"/"+numberOfIterations;
        }
    }
}

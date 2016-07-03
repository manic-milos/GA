using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA.IteratedLocalSearch.InitialSolutionGenerators.UnfeasableSolutionDecider
{
    class IterationUnfeasableDecider:UnfeasableSolutionDeciderBase
    {
        public int maxIterations;
        public IterationUnfeasableDecider(int maxIterations)
        {
            this.maxIterations = maxIterations;
        }
        public override bool decideUnfeasable(InitialSolutionGeneratorBase generator)
        {
            if (generator.iteration < maxIterations)
                return false;
            return true;
        }
    }
}

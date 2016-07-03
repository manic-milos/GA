using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA.IteratedLocalSearch.InitialSolutionGenerators.UnfeasableSolutionDecider
{
    abstract class UnfeasableSolutionDeciderBase
    {
        public abstract  bool decideUnfeasable(InitialSolutionGeneratorBase generator);
    }
}

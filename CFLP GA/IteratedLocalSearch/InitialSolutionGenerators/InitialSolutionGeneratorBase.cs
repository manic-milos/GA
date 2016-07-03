using CFLP_GA.IteratedLocalSearch.InitialSolutionGenerators.UnfeasableSolutionDecider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA.IteratedLocalSearch.InitialSolutionGenerators
{
    abstract class InitialSolutionGeneratorBase
    {
        public int iteration;
        public Problem problem;
        public UnfeasableSolutionDeciderBase decider;
        public InitialSolutionGeneratorBase(Problem problem,
            UnfeasableSolutionDeciderBase decider)
        {
            this.problem=problem;
            this.decider=decider;
        }
        public Solution Generate()
        {
            //TODO time i report
            return generate();
        }
        protected abstract Solution generate();
    }
}

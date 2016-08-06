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
            this.problem = problem;
            this.decider = decider;
        }
        public Solution Generate(bool initial = true)
        {
            Reports.IterationalReport.Report("generating initial solution...");
            Solution s = generate();
            if (s == null)
            {
                if (initial)
                    Reports.ShortReport.Report("unfeasable...");
                return null;
            }
            //TODO time i report
            Reports.IterationalReport.Report("initial solution generated: " + s);
            return s;
        }
        protected abstract Solution generate();
    }
}

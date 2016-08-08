using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA.IteratedLocalSearch.AcceptanceCriteria
{
    class RestartWalk:AcceptanceCriterionBase
    {
        public int i = 0;
        public int n = int.MaxValue;
        public AcceptanceCriterionBase defaultWalk;
        public InitialSolutionGenerators.InitialSolutionGeneratorBase randomGen;
        Solution previous = null;
        public RestartWalk(int n,AcceptanceCriterionBase defaultWalk,
            InitialSolutionGenerators.InitialSolutionGeneratorBase randomGen)
        {
            this.n = n;
            this.defaultWalk = defaultWalk;
            this.randomGen = randomGen;
        }
        protected override Solution accept(Solution newSol, Solution oldSol, EvaluatorBase evaluator)
        {
            if(i<n)
            {
                Solution s=defaultWalk.Accept(newSol, oldSol, evaluator);
                if(previous!=null&&previous.Equals(s))
                    i++;
                else
                    i=0;
                previous=s;
                return s;
            }
            Solution restarted=restart();
            if (restarted != null)
                return restarted;
            return defaultWalk.Accept(newSol, oldSol, evaluator);

        }
        private Solution restart()
        {
            Reports.IterationalReport.Report("restart...");
            Solution s = null;
            try
            {

                s = randomGen.Generate(false);
                this.i = 0;
                previous = null;
            }
            catch(UnfeasableProblemException e)
            {
                Reports.IterationalReport.Report("failed...");
            }
            return s;
        }
    }
}

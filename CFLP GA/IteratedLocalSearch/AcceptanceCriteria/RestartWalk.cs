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
            Execution_Reports.ReportController.DebugLogReport(this,"trying to restart walk...");
            Solution s = null;
            try
            {

                s = randomGen.Generate(false);
                this.i = 0;
                previous = null;

                Execution_Reports.ReportController.DebugLogReport(this,"restart succeded...");
            }
            catch(UnfeasableProblemException e)
            {
                Execution_Reports.ReportController.DebugLogReport(this,"restart failed...");
            }
            return s;
        }
    }
}

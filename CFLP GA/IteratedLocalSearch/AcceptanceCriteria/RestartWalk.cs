using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA.IteratedLocalSearch.AcceptanceCriteria
{
    class RestartWalk:AcceptanceCriterionBase
    {//TODO uporedjivanje posebno(predicate)
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
            return restart();

        }
        private Solution restart()
        {
            Console.WriteLine("restart...");
            Console.Read();
            this.i = 0;
            previous = null;
            return randomGen.Generate();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA.IteratedLocalSearch.AcceptanceCriteria
{
    abstract class AcceptanceCriterionBase
    {//TODO uporedjivanje posebno(predicate)
        public Solution Accept(Solution newSol, Solution oldSol,
            EvaluatorBase evaluator)
        {
            if(newSol==null&&oldSol==null)
                Console.WriteLine("sdfdsfdsfds");
            if (newSol == null)
                return oldSol;
            if (oldSol == null)
                return newSol;
            Solution chosen=accept(newSol, oldSol, evaluator);
            //Reports.VerboseReport.Report("chosen "+chosen+"\nfrom\t"+oldSol+"\n\t"+newSol);
            return chosen;
        }
        protected abstract Solution accept(Solution newSol, Solution oldSol,
            EvaluatorBase evaluator);
    }
}

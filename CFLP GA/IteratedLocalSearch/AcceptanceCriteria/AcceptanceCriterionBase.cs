using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA.IteratedLocalSearch.AcceptanceCriteria
{
    abstract class AcceptanceCriterionBase
    {
        public Solution Accept(Solution newSol, Solution oldSol,
            FitnessCalculatorBase evaluator)
        {
            return accept(newSol, oldSol, evaluator);
        }
        protected abstract Solution accept(Solution newSol, Solution oldSol,
            FitnessCalculatorBase evaluator);
    }
}

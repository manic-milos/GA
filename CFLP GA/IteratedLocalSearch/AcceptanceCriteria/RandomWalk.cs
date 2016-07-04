using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA.IteratedLocalSearch.AcceptanceCriteria
{
    class RandomWalk:AcceptanceCriterionBase
    {
        protected override Solution accept(Solution newSol, Solution oldSol, EvaluatorBase evaluator)
        {
            return newSol;
        }
    }
}

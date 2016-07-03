using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA.IteratedLocalSearch.StoppingCriteria
{
    class DimensionStoppingCriterion:IterationalStoppingCriterion
    {
        public DimensionStoppingCriterion(double factor, Problem problem)
            :base((int)(problem.m*factor))
        {
        }

    }
}

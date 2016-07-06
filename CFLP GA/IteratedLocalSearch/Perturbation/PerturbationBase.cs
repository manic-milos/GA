using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA.IteratedLocalSearch.Perturbation
{
    abstract class PerturbationBase
    {
        public Solution Perturb(Solution s)
        {
            //TODO time i report
            Solution perturbed=perturb(s);
            return perturbed;

        }
        protected abstract Solution perturb(Solution s);
    }
}

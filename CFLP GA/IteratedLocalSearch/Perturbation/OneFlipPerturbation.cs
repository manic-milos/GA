using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA.IteratedLocalSearch.Perturbation
{
    class OneFlipPerturbation:PerturbationBase
    {
        protected override Solution perturb(Solution s)
        {
            int pos= ControlledRandom.getRandomFromRange(0, s.vars.Length);
            var perturbed = s.Clone();
            perturbed[pos] = 1 - perturbed[pos];
            return perturbed;
        }
    }
}

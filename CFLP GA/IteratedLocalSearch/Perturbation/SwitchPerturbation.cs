using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA.IteratedLocalSearch.Perturbation
{
    class SwitchPerturbation:PerturbationBase
    {
        protected override Solution perturb(Solution s)
        {
            int pos1 = ControlledRandom.getRandomFromRange(0, s.vars.Length);
            int pos2 = ControlledRandom.getRandomFromRange(0, s.vars.Length);
            var perturbed = s.Clone();
            perturbed[pos1] = s[pos2];
            perturbed[pos2] = s[pos1];
            return perturbed;
        }
    }
}

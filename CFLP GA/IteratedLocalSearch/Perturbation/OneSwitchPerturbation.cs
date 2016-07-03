using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA.IteratedLocalSearch.Perturbation
{
    class OneSwitchPerturbation:PerturbationBase
    {
        protected override Solution perturb(Solution s)
        {
            List<int> ones = new List<int>();
            for(int i=0;i<s.vars.Length;i++)
            {
                if (s[i] > 0)
                    ones.Add(i);
            }
            int pos1 = ones[ControlledRandom.getRandomFromRange(0, ones.Count)];
            int pos2 = ControlledRandom.getRandomFromRange(0, s.vars.Length);
            var perturbed = s.Clone();
            perturbed[pos1] = s[pos2];
            perturbed[pos2] = s[pos1];
            return perturbed;
        }
    }
}

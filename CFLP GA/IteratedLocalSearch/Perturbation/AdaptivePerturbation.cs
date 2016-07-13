using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA.IteratedLocalSearch.Perturbation
{
    class AdaptivePerturbation : BinomialPerturbation
    {
        Solution previous = null;
        double initialProbability;
        double increasingPercent;
        public AdaptivePerturbation(PerturbationBase defaultPerturbation,
            int maxPerturbations, double initialProbability = 0.8,
            double increasingPercent = 0.1)
            : base(defaultPerturbation, maxPerturbations, initialProbability)
        {
            this.increasingPercent = increasingPercent;
            this.initialProbability = initialProbability;
        }
        protected override Solution perturb(Solution s)
        {
            if (previous != null && previous.Equals(s))
            {
                p = p + increasingPercent * (1 - p);
            }
            else
            {
                previous = s;
                p = initialProbability;
            }
            Solution good = s;
            s = s.Clone();
            int i;
            for (i = 0; i < maxPerturbations; i++)
            {
                double chance = ControlledRandom.getRandomDouble();
                if (chance > p)
                {
                    break;
                }
                Solution perturbed = defaultPerturbation.Perturb(s);
                if (perturbed.check())
                {
                    good = perturbed;
                }
                s = perturbed;
            }
            Reports.VerboseReport.Report("stopped at:" + i);
            return good;
        }
    }
}

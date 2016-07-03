using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA.IteratedLocalSearch.Perturbation
{
    class BinomialPerturbation:PerturbationBase
    {
        public int maxPerturbations;
        public double p;
        public PerturbationBase defaultPerturbation;
        public BinomialPerturbation(
            PerturbationBase defaultPerturbation,
            int maxPerturbations,double p=0.8
            )
        {
            this.maxPerturbations = maxPerturbations;
            this.p = p;
            this.defaultPerturbation = defaultPerturbation;
        }
        protected override Solution perturb(Solution s)
        {
            s = s.Clone();
            int i;
            for(i=0;i<maxPerturbations;i++)
            {
                double chance = ControlledRandom.getRandomDouble();
                if(chance>p)
                {
                    break;
                }
                s = defaultPerturbation.Perturb(s);
            }
            //Console.WriteLine("stopped at:"+i);
            return s;
        }
    }
}

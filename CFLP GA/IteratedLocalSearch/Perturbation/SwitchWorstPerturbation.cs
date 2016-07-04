using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA.IteratedLocalSearch.Perturbation
{
    class SwitchWorstPerturbation:OneSwitchPerturbation
    {
        EvaluatorBase evaluator;
        public SwitchWorstPerturbation(EvaluatorBase evaluator)
        {
            this.evaluator = evaluator;
        }
        protected override Solution perturb(Solution s)
        {
            double min = double.MaxValue;
            int mini = -1;
            for (int i = 0; i < s.vars.Length; i++)
            {
                if (s[i] > 0)
                {
                    s[i] = 0;
                    double v = evaluator.Evaluate(s);
                    if(v<min)
                    {
                        min = v;
                        mini = i;
                    }
                    s[i] = 1;
                }

            }
            int pos = ControlledRandom.getRandomFromRange(0, s.vars.Length);
            var perturbed = s.Clone();
            perturbed[mini] = s[pos];
            perturbed[pos] = s[mini];
            return perturbed;
        }
    }
}

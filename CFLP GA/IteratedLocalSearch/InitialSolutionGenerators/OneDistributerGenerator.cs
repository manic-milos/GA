using CFLP_GA.IteratedLocalSearch.InitialSolutionGenerators.UnfeasableSolutionDecider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA.IteratedLocalSearch.InitialSolutionGenerators
{
    class OneDistributerGenerator : InitialSolutionGeneratorBase
    {
        public double oneProbability = 0.5;
        public OneDistributerGenerator(Problem problem,
            UnfeasableSolutionDeciderBase decider,
            double oneProbability=0.5)
            : base(problem, decider)
        {
            this.oneProbability = oneProbability;
        }
        protected override Solution generate()
        {
            Solution s;
            iteration = 0;
            do
            {
                s = new Solution(problem);
                for (int i = 0; i < problem.k; i++)
                {
                    double d = ControlledRandom.getRandomDouble();
                    if (d <oneProbability)
                    {
                        int position = ControlledRandom.getRandomFromRange(
                            0, problem.m);
                        s[position] = 1;
                    }
                }

                iteration++;
                decider.decideUnfeasable(this);
            } while (!s.check());
            return s;
        }
    }
}

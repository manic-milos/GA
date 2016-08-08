using CFLP_GA.IteratedLocalSearch.InitialSolutionGenerators.UnfeasableSolutionDecider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CFLP_GA.IteratedLocalSearch.InitialSolutionGenerators
{
    class RandomSolutionGenerator:InitialSolutionGeneratorBase
    {
        public RandomSolutionGenerator(Problem problem,
            UnfeasableSolutionDeciderBase decider)
            :base(problem,decider)
        {
        }
        protected override Solution generate()
        {
            Solution s;
            iteration = 0;
            do
            {
                s = Solution.generateRandom(problem);
                iteration++;
                decider.decideUnfeasable(this);
            } while (!s.check());
            return s;
        }
    }
}

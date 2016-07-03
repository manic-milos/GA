using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA.IteratedLocalSearch.LocalSearch
{
    abstract class LocalSearchBase
    {
        public Stopwatch time = new Stopwatch();
        public TimeSpan overallTime = new TimeSpan(0);
        //TODO  reports?
        HashSet<Solution> localOptimums = new HashSet<Solution>();

        public Solution GetBestLocal(Solution s,
            FitnessCalculatorBase evaluator)
        {
            time.Restart();
            var prev = s;
            Solution bestLocal = s;
            if (localOptimums.Contains(s))
            {
                return s;
            }
            do
            {
                prev = bestLocal;
                bestLocal = getBestLocal(s, evaluator);
            } while (!prev.Equals(bestLocal));
            localOptimums.Add(bestLocal);
            time.Stop();
            overallTime += time.Elapsed;
            return bestLocal;
        }
        protected abstract Solution getBestLocal(Solution s,
            FitnessCalculatorBase evaluator);
    }
}

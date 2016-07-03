using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA.IteratedLocalSearch.StoppingCriteria
{
    class TimeStoppingCriterion:StoppingCriterionBase
    {
        Stopwatch timer = null;
        int seconds;
        public TimeStoppingCriterion(int seconds)
        {
            this.seconds = seconds;
        }
        protected override bool checkIfEnd(Solution s, Solution globalBest)
        {
            if(timer==null)
            {
                timer = new Stopwatch();
                timer.Start();
            }
            if(timer.Elapsed.Seconds>seconds)
            {
                return true;
            }
            return false;
        }

        public override string IterationInfo()
        {
            return timer.Elapsed.ToString();
        }
    }
}

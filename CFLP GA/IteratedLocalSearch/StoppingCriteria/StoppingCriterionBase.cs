using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA.IteratedLocalSearch.StoppingCriteria
{
    abstract class StoppingCriterionBase
    {
        List<StoppingCriterionBase> criteria = new List<StoppingCriterionBase>();
        public bool CheckIfEnd(Solution s,Solution globalBest=null)
        {
            bool stop = checkIfEnd(s, globalBest);
            if (stop)
                return true;
            foreach(StoppingCriterionBase criterion in criteria)
            {
                if (criterion.CheckIfEnd(s, globalBest))
                    return true;
            }
            return false;
        }
        protected abstract bool checkIfEnd(Solution s,Solution globalBest=null);
        public abstract string IterationInfo();
        public string IterationInfoAll()
        {
            string s = IterationInfo()+";";
            foreach(var criterion in criteria)
            {
                s += criterion.IterationInfoAll();
            }
            return s;
        }
        public StoppingCriterionBase AppendStoppingCriteria(
            StoppingCriterionBase criterion)
        {
            criteria.Add(criterion);
            return this;
        }
    }
}

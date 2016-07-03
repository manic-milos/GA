using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA.IteratedLocalSearch.StoppingCriteria
{
    class PlateauStoppingCriterion : StoppingCriterionBase
    {
        int plateauWidth;
        Solution previous = null;
        int currentPlateauWidth = 0;
        public PlateauStoppingCriterion(int plateauWidth)
        {
            this.plateauWidth = plateauWidth;
        }
        protected override bool checkIfEnd(Solution s,Solution globalBest)
        {
            if (previous != null && globalBest.Equals(previous))
            {
                currentPlateauWidth++;
            }
            else
            {
                previous = globalBest;
                currentPlateauWidth = 1;
                return false;
            }
            if (currentPlateauWidth < plateauWidth)
                return false;
            return true;
        }

        public override string IterationInfo()
        {
            return "Plateau width=" + currentPlateauWidth + "/" + plateauWidth;
        }
    }
}

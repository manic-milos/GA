using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA
{
    class FactorUnfeasableDecider:UnfeasableDeciderBase
    {
        public int size;
        public double decidingFactor;
        public FactorUnfeasableDecider(int size,double decidingFactor=0.5)
        {
            this.size = size;
            this.decidingFactor = decidingFactor;
        }
        public override bool DecideUnfeasable(int iteration,GenePopulation genes)
        {
            if (genes.Count == 0)
            {
                if (iteration > decidingFactor * size)
                    return true;
            }
            return false;
        }
    }
}

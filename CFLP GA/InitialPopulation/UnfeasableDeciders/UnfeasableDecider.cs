using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA
{
    abstract class UnfeasableDeciderBase
    {
        public abstract bool DecideUnfeasable(int iteration,GenePopulation genes);
    }
}

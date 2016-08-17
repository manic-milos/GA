using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA
{
    class AppendingReplacement:ReplacementBase
    {

        protected override GenePopulation replace(
            GenePopulation parents, GenePopulation children)
        {
            return parents.Append(children);
        }
    }
}

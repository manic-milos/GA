using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA
{
    class GenerationReplacement:ReplacementBase
    {
        TrimmingReplacement trim = null;
        public GenerationReplacement(TrimmingReplacement trim=null)
        {
            this.trim = trim;
        }
        protected override GenePopulation replace(GenePopulation parents, GenePopulation children)
        {
            if(trim!=null)
            {
                children = trim.Replace(children, new GenePopulation());
            }
            return children;
        }
    }
}

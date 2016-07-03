using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA
{
    abstract class MutatorBase
    {
        public Genome Mutate(Genome g)
        {
            return mutate(g.Clone());
        }
        protected abstract Genome mutate(Genome g);

    }
}

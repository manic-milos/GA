using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA
{
    abstract class MutationBase
    {
        public MutatorBase mutator;
        public Stopwatch time = new Stopwatch();
        public TimeSpan overallTime = new TimeSpan(0);
        public void Mutate(GenePopulation genePool, GeneticAlgorithm ga)
        {
            time.Restart();
            mutate(genePool, ga);
            time.Stop();
            overallTime += time.Elapsed;
        }
        protected abstract void mutate(GenePopulation genePool,GeneticAlgorithm ga);
        public MutationBase(MutatorBase mutator)
        {
            this.mutator = mutator;
        }
    }
}

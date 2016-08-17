using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA
{
    abstract class GenomeAdjusterBase
    {
        public Stopwatch time = new Stopwatch();
        public TimeSpan overallTime = new TimeSpan(0);
        public Genome Adjust(Genome g, GeneticAlgorithm ga)
        {
            time.Restart();
            Genome g1=adjust(g,ga);
            time.Stop();
            overallTime += time.Elapsed;
            return g1;
        }
        protected abstract Genome adjust(Genome g, GeneticAlgorithm ga);
    }
}

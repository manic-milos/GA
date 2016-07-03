using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace CFLP_GA
{
    abstract class CrossoverMatchingBase
    {
        public GenomeCrossBase cross;
        List<CrossoverMatchingBase> moreMatchings = new List<CrossoverMatchingBase>();
        public Stopwatch time = new Stopwatch();
        public TimeSpan overallTime = new TimeSpan(0);
        protected CrossoverMatchingBase(GenomeCrossBase cross)
        {
            this.cross = cross;
            time = new Stopwatch();
        }
        protected abstract GenePopulation crossGenomes(GenePopulation crossPool,
            GeneticAlgorithm ga);
        public GenePopulation CrossGenomes(GenePopulation crossPool,
            GeneticAlgorithm ga)
        {
            time.Restart();
            GenePopulation children=crossGenomes(crossPool, ga);
            foreach(CrossoverMatchingBase mat in moreMatchings)
            {
                children = children.Append(mat.CrossGenomes(crossPool, ga));
            }
            time.Stop();
            overallTime += time.Elapsed;
            return children;
        }
        public void AppendMatching(CrossoverMatchingBase matching)
        {
            moreMatchings.Add(matching);
        }
    }
}

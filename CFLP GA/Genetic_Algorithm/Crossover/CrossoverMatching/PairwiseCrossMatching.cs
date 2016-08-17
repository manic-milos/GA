using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA
{
    class PairwiseCrossMatching:CrossoverMatchingBase
    {
        static Stopwatch pairwisetime = new Stopwatch();
        public static TimeSpan crossTime = new TimeSpan(0);
        public PairwiseCrossMatching(GenomeCrossBase cross)
            :base(cross)
        {

        }

        protected override GenePopulation crossGenomes(GenePopulation crossPool, GeneticAlgorithm ga)
        {
            pairwisetime.Restart();
            GenePopulation children = new GenePopulation(ga);
            foreach (Genome p1 in crossPool)
            {
                foreach (Genome p2 in crossPool)
                {
                    List<Genome> parents=new List<Genome>(){p1,p2};
                    if (!p1.Equals(p2))
                    {
                        foreach(Genome child in cross.crossGenomes(parents,ga))
                        {
                            children.Include(child);
                        }
                    }
                }
            }
            pairwisetime.Stop();
            crossTime += pairwisetime.Elapsed;
            return children;
        }
    }
}

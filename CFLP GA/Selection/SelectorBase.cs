using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA
{
    abstract class SelectorBase
    {
        protected List<SelectorBase> moreSelectors = new List<SelectorBase>();
        public Stopwatch time = new Stopwatch();
        public TimeSpan overallTime = new TimeSpan(0);
        protected abstract GenePopulation select(GenePopulation population,
            GeneticAlgorithm ga);
        public GenePopulation Select(GenePopulation population,
            GeneticAlgorithm ga)
        {
            time.Restart();
            GenePopulation selected=select(population, ga);
            foreach(SelectorBase selector in moreSelectors)
            {
                selected.Append(selector.Select(population, ga));
            }
            time.Stop();
            overallTime += time.Elapsed;
            return selected;
        }
        public void AddSelector(SelectorBase selector)
        {
            moreSelectors.Add(selector);
        }
    }
}

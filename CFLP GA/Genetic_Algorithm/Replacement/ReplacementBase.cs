using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace CFLP_GA
{
    abstract class ReplacementBase
    {
        List<ReplacementBase> moreReplacements = new List<ReplacementBase>();
        public Stopwatch time = new Stopwatch();
        public TimeSpan overallTime = new TimeSpan(0);
        protected abstract GenePopulation replace(
            GenePopulation parents, GenePopulation children);
        public GenePopulation Replace(
            GenePopulation parents, GenePopulation children)
        {
            time.Restart();
            GenePopulation selected = replace(parents, children);
            foreach(ReplacementBase selector in moreReplacements)
            {
                selected=selected.Append(selector.Replace(parents, children));
            }
            time.Stop();
            overallTime += time.Elapsed;
            return selected;
        }
        public void AddInheritanceSelector(ReplacementBase selector)
        {
            moreReplacements.Add(selector);
        }
    }
}

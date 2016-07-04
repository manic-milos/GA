using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA
{
    abstract class StoppingCriterionBase
    {
        protected GeneticAlgorithm ga;
        public List<StoppingCriterionBase> moreCriteria = new List<StoppingCriterionBase>();
        public Stopwatch time = new Stopwatch();
        public TimeSpan overallTime = new TimeSpan(0);
        protected abstract void init(GeneticAlgorithm ga);
        public void Init(GeneticAlgorithm ga)
        {
            time.Restart();
            this.ga = ga;
            init(ga);
            foreach(StoppingCriterionBase criterion in moreCriteria)
            {
                criterion.Init(ga);
            }
            time.Stop();
            overallTime += time.Elapsed;
        }
        protected abstract bool checkStoppingCriterion(GenePopulation genes);
        public bool CheckStoppingCriterion(GenePopulation genes)
        {
            time.Restart();
            bool stop = false;
            stop = stop || checkStoppingCriterion(genes);
            foreach (StoppingCriterionBase criterion in moreCriteria)
            {
                stop = stop || criterion.CheckStoppingCriterion(genes);
            }
            time.Stop();
            overallTime += time.Elapsed;
            return stop;
        }
        protected abstract string currentIteration();
        public string CurrentIteration()
        {
            time.Restart();
            string it=this.currentIteration();
            foreach(StoppingCriterionBase criterion in moreCriteria)
            {
                it += "(" + criterion.CurrentIteration() + ")";
            }
            time.Stop();
            overallTime += time.Elapsed;
            return it;
        }
        public void appendCriterion(StoppingCriterionBase criterion)
        {
            moreCriteria.Add(criterion);
        }

    }
}

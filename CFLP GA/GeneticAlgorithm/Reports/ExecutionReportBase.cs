using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA.Reports
{
    abstract class ExecutionReportBase
    {
        public abstract void Initialize(GeneticAlgorithm ga);
        public abstract void Report(Problem p);
        public abstract void Report(InitialPopulationBase generator,
            GenePopulation g);
        public abstract void Report(SelectorBase selector,
            GenePopulation g);
        public abstract void Report(CrossoverMatchingBase cross,
            GenePopulation g);
        public abstract void Report(ReplacementBase replacer,
            GenePopulation g);
        public abstract void ReportIteration(StoppingCriterionBase criterion,
            GenePopulation genePool);
        public abstract void Report(GenePopulation g);
        public abstract void ReportEnd(StoppingCriterionBase criterion,
            GenePopulation g);
    }
}

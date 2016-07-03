using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA.Reports
{
    class ShortIterationalReport:ShortTabularReport
    {
        public override void ReportIteration(StoppingCriterionBase criterion, GenePopulation genePool)
        {
            writer.WriteLine(criterion.CurrentIteration() + ": min=" +genePool.Min.fitness());
        }
        public override void ReportEnd(StoppingCriterionBase criterion, GenePopulation g)
        {
            base.ReportEnd(criterion, g);
        }
    }
}

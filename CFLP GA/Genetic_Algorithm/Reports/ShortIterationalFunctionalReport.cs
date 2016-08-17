using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA.Reports
{
    class ShortIterationalFunctionalReport:ShortIterationalReport
    {
        public override void Report(CrossoverMatchingBase cross, GenePopulation g)
        {
            //writer.WriteLine("crossover time:" + cross.time.Elapsed);
            //writer.WriteLine("overall crossover time:" + cross.overallTime);
            base.Report(cross, g);
        }
        public override void Report(ReplacementBase replacer, GenePopulation g)
        {
            base.Report(replacer, g);
        }
        public override void ReportIteration(StoppingCriterionBase criterion, GenePopulation genePool)
        {
            base.ReportIteration(criterion, genePool);
        }
        public override void ReportEnd(StoppingCriterionBase criterion, GenePopulation g)
        {
            base.ReportEnd(criterion, g);
            ShortTabularFunctionalReport.reportTime(writer, ga);
        }
    }
}

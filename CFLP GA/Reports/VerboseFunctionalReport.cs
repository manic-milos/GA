using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA.Reports
{
    class VerboseFunctionalReport : ShortIterationalFunctionalReport
    {
        public override void Report(CrossoverMatchingBase cross, GenePopulation g)
        {
            base.Report(cross, g);
            writer.WriteLine("crossover time:" + cross.time.Elapsed);
            //writer.WriteLine("overall crossover time:" + cross.overallTime);
        }
        public override void Report(ReplacementBase replacer, GenePopulation g)
        {
            base.Report(replacer, g);
            writer.WriteLine("replacement time:" + replacer.time.Elapsed);
        }
        public override void ReportEnd(StoppingCriterionBase criterion, GenePopulation g)
        {
            base.ReportEnd(criterion, g);
        }
    }
}

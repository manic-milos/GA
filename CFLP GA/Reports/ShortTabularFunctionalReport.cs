using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA.Reports
{
    class ShortTabularFunctionalReport : ShortTabularReport
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
        public override void ReportEnd(StoppingCriterionBase criterion, GenePopulation g)
        {
            base.ReportEnd(criterion, g);
            reportTime(writer, ga);
        }
        public static void reportTime(TextWriter writer, GeneticAlgorithm ga)
        {
            writer.WriteLine("overall crossover time:" + ga.genomeCross.overallTime);
            writer.WriteLine("overall replacement time:" + ga.replacer.overallTime);
            writer.WriteLine("overall fitness calculation time:" + ga.fitnessCalc.overallTime);
            writer.WriteLine("overall genome adjustment time:" + ga.adjuster.overallTime);
            writer.WriteLine("overall selection time:" + ga.selector.overallTime);
            writer.WriteLine("overall mutation time:" + ga.mutation.overallTime);
            writer.WriteLine("overall criteria evaluation time:" + ga.stoppingCriterion.overallTime);
            writer.WriteLine("overall population generating time:" + ga.initialPopulation.overallTime);
        }
    }
}

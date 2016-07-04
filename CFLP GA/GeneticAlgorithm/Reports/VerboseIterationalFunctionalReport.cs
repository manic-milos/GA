using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA.Reports
{
    class VerboseIterationalFunctionalReport:ShortIterationalFunctionalReport
    {
        public Stopwatch iterationTime=new Stopwatch();
        public TimeSpan overallTime=new TimeSpan(0);
        public override void Initialize(GeneticAlgorithm ga)
        {
            base.Initialize(ga);
            iterationTime.Restart();
            writer.WriteLine("Starting population generating...");

        }
        public override void Report(Problem p)
        {
            base.Report(p);
            writer.WriteLine("sum demands="+p.SumDemands);
        }
        public override void ReportIteration(StoppingCriterionBase criterion, GenePopulation genePool)
        {
            base.ReportIteration(criterion, genePool);
            iterationTime.Stop();
            writer.WriteLine("iteration time=" + iterationTime.Elapsed);
            reportTime(writer,ga);
        }
        public override void Report(InitialPopulationBase generator, GenePopulation g)
        {
            base.Report(generator, g);
            writer.WriteLine("initial population generated...");
        }
        public static void reportTime(TextWriter writer, GeneticAlgorithm ga)
        {
            writer.WriteLine("crossover time:\t\t" + ga.genomeCross.overallTime);
            writer.WriteLine("replacement time:\t" + ga.replacer.overallTime);
            writer.WriteLine("fitness calc time:\t" + ga.fitnessCalc.overallTime);
            writer.WriteLine("genome adj time:\t" + ga.adjuster.overallTime);
            writer.WriteLine("selection time:\t\t" + ga.selector.overallTime);
            writer.WriteLine("mutation time:\t\t" + ga.mutation.overallTime);
            writer.WriteLine("criteria eval time:\t" + ga.stoppingCriterion.overallTime);
            writer.WriteLine("population gen time:\t" + ga.initialPopulation.overallTime);
        }
    }
}

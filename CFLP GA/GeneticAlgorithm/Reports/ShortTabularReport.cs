using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA.Reports
{
    class ShortTabularReport : ExecutionReportBase
    {
        protected GeneticAlgorithm ga;
        protected TextWriter writer = Console.Out;
        protected Stopwatch overallExecutionTime = new Stopwatch();
        public override void Initialize(GeneticAlgorithm ga)
        {
            overallExecutionTime.Restart();
            this.ga = ga;
            Report(ga.problem);
        }
        public override void Report(Problem p)
        {
            string s = "";
            s += "n=" + p.n + ", m=" + p.m + ", k=" + p.k;
            writer.WriteLine(s);
        }
        public override void Report(GenePopulation g)
        {
            //writer.WriteLine("{"+g.Min + "("+g.Min.fitness()+")"+ "}" + g.Count);
            writer.WriteLine("min="+g.Min.fitness());
        }

        public override void Report(InitialPopulationBase generator, GenePopulation g)
        {
        }

        public override void Report(SelectorBase selector, GenePopulation g)
        {
        }

        public override void Report(CrossoverMatchingBase cross, GenePopulation g)
        {
        }

        public override void Report(ReplacementBase replacer, GenePopulation g)
        {
        }
        public override void ReportIteration(StoppingCriterionBase criterion,
            GenePopulation genePool)
        {

            Console.Write(".");
        }

        public override void ReportEnd(StoppingCriterionBase criterion, GenePopulation g)
        {
            overallExecutionTime.Stop();
            writer.WriteLine("");
            Report(g);
            writer.WriteLine("overall time=" + overallExecutionTime.Elapsed);
            IteratedLocalSearch.Reports.ShortReport.Report(
                overallExecutionTime.Elapsed.ToString());
            IteratedLocalSearch.Reports.ShortReport.Report(
                g.Min.fitness() + g.Min);
        }
    }
}

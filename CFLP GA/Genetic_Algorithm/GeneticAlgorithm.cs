using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA
{
    class GeneticAlgorithm
    {
        public SelectorBase selector;
        public StoppingCriterionBase stoppingCriterion;
        public MutationBase mutation;
        public CrossoverMatchingBase genomeCross;
        public ReplacementBase replacer;
        public EvaluatorBase fitnessCalc;
        public GenomeAdjusterBase adjuster;
        public InitialPopulationBase initialPopulation;
        public Problem problem;
        public double lastResult = double.NaN;
        public string iterationsToBestResult = "";
        public Stopwatch timer = new Stopwatch();
        public TimeSpan timeToBestResult = new TimeSpan(0);
        public GeneticAlgorithm(
            SelectorBase selector,
            StoppingCriterionBase stoppingCriterion,
            MutationBase mutation,
            CrossoverMatchingBase genomeCross,
            ReplacementBase replacer,
            EvaluatorBase fitnessCalc,
            GenomeAdjusterBase adjuster,
            InitialPopulationBase initialPopulation,
            Problem problem)
        {
            this.selector = selector;
            this.stoppingCriterion = stoppingCriterion;
            this.mutation = mutation;
            this.problem = problem;
            this.genomeCross = genomeCross;
            this.replacer = replacer;
            this.fitnessCalc = fitnessCalc;
            this.adjuster = adjuster;
            this.initialPopulation = initialPopulation;
            //TODO provera da li je zadat problem;
        }
        public Genome adjustGenome(Genome g)
        {
            return adjuster.Adjust(g, this);
        }
        public GenePopulation createInitialPopulation(
            Reports.ExecutionReportBase report = null)
        {
            GenePopulation population = initialPopulation.Create(this);
            if (report != null)
            {
                report.Report(initialPopulation, population);
            }
            return population;
        }
        public void mutate(GenePopulation chromosome)
        {
            //TODO report
            mutation.Mutate(chromosome, this);
        }
        public GenePopulation select(GenePopulation chromosome,
            Reports.ExecutionReportBase report = null)
        {
            GenePopulation selected = selector.Select(chromosome, this);
            if (report != null)
            {
                report.Report(selector, selected);
            }
            return selected;
        }
        public GenePopulation crossover(GenePopulation mating,
            Reports.ExecutionReportBase report = null)
        {
            GenePopulation crossed = genomeCross.CrossGenomes(mating, this);
            if (report != null)
            {
                report.Report(genomeCross, crossed);
            }
            return crossed;
        }
        public GenePopulation replacement(GenePopulation genePool,
            GenePopulation children,
            Reports.ExecutionReportBase report = null)
        {
            GenePopulation replaced = replacer.Replace(genePool, children);
            if (report != null)
            {
                report.Report(replacer, replaced);
            }
            return replaced;
        }
        public double fitness(Genome g, int[] leftCapacities = null)
        {
            return fitnessCalc.Fitness(g, leftCapacities);
        }
        public GenePopulation start(Reports.ExecutionReportBase report = null)
        {
            if (report != null)
            {
                report.Initialize(this);
            }
            GenePopulation Genes;
            Genes = createInitialPopulation(report);
            stoppingCriterion.Init(this);
            return Genes;
        }
        public GenePopulation iteration(GenePopulation Genes)
        {
            mutate(Genes);
            GenePopulation parents = select(Genes);
            GenePopulation children = crossover(parents);
            Genes = replacement(Genes, children);
            //if (report == null)
            //    Console.WriteLine(stoppingCriterion.CurrentIteration() + ":" + Genes.Min + " " +
            //        fitness(Genes.Min) + " " + fitness(Genes.Max) + " " + Genes.Count);
            //else
            //    report.ReportIteration(stoppingCriterion, Genes);
            return Genes;

        }
        public GenePopulation end(GenePopulation Genes, Reports.ExecutionReportBase report = null)
        {
            if (report != null)
                report.ReportEnd(stoppingCriterion, Genes);
            return Genes;
        }
        public double execute()
        {
            Genome res;
            return execute(out res);
        }
        public double execute(out Genome result)
        {
            Execution_Reports.ReportController.progressReport.startCounting();
            timer.Restart();
            GenePopulation Genes = start();
            if (Genes == null)
            {
                result = null;
                return double.NaN;
            }
            //Console.WriteLine(Genes.Min);
            //Console.WriteLine(fitness(Genes.Min));
            while (!stoppingCriterion.CheckStoppingCriterion(Genes))
            {
                Genes = iteration(Genes);
                double newResult = Genes.Min.fitness();
                if (double.IsNaN(lastResult) || newResult < lastResult)
                {
                    lastResult = newResult;
                    iterationsToBestResult = stoppingCriterion.CurrentIteration();
                    timeToBestResult = timer.Elapsed;
                }
                Execution_Reports.ReportController.populationLog.Broadcast(Genes.Count.ToString());
                Execution_Reports.ReportController.progressReport.addCount(stoppingCriterion.CurrentIteration());

            }
            Genes = end(Genes);
            Execution_Reports.ReportController.progressReport.endCount();
            //Console.WriteLine(Genes.Min);
            //Console.WriteLine(fitness(Genes.Min));
            //Console.Read();
            result = Genes.Min;
            return result.fitness();
        }
    }
}

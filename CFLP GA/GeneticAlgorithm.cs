using System;
using System.Collections.Generic;
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
        public Settings settings = new Settings();
        public Problem problem;
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
            if(report!=null)
            {
                report.Report(genomeCross, crossed);
            }
            return crossed;
        }
        public GenePopulation replacement(GenePopulation genePool, 
            GenePopulation children,
            Reports.ExecutionReportBase report=null)
        {
            GenePopulation replaced= replacer.Replace(genePool, children);
            if(report!=null)
            {
                report.Report(replacer, replaced);
            }
            return replaced;
        }
        public double fitness(Genome g, int[] leftCapacities = null)
        {
            return fitnessCalc.Fitness(g, leftCapacities);
        }
        public double execute(Reports.ExecutionReportBase report=null)
        {
            if (report != null)
            {
                report.Initialize(this);
            }
            GenePopulation Genes;
            try
            {
                Genes = createInitialPopulation(report);
            }
            catch(UnfeasableProblemException e)
            {
                Console.WriteLine(e.Message);
                return double.NaN;
            }
            //Console.WriteLine(Genes.Min);
            //Console.WriteLine(fitness(Genes.Min));
            stoppingCriterion.Init(this);
            while (!stoppingCriterion.CheckStoppingCriterion(Genes))
            {
                mutate(Genes);
                GenePopulation parents = select(Genes,report);
                GenePopulation children = crossover(parents,report);
                Genes = replacement(Genes, children,report);
                if (report == null)
                    Console.WriteLine(stoppingCriterion.CurrentIteration() + ":" + Genes.Min + " " +
                        fitness(Genes.Min) + " " + fitness(Genes.Max) + " " + Genes.Count);
                else
                    report.ReportIteration(stoppingCriterion, Genes);
            }
            report.ReportEnd(stoppingCriterion, Genes);
            //Console.WriteLine(Genes.Min);
            //Console.WriteLine(fitness(Genes.Min));
            //Console.Read();
            return fitness(Genes.Min);
        }
    }
}

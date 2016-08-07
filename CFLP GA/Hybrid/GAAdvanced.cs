using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA.Hybrid
{
    class GAAdvanced
    {
        GeneticAlgorithm ga;
        IteratedLocalSearch.ILS ils;
        Problem problem;
        public GAAdvanced(Problem problem)
        {
            this.problem = problem;
        }
        public void setupGA()
        {
            var selector = new RankBasedSelector(20, 2.0);
            var criterion = new GenerationLimitCriterion();
            var mutator = new RandomWithPreferenceMutator();
            var mutation = new SureRandomMutation(mutator);
            var crossover = new PairUniformCross();
            var crossoverMatch = new StochasticCrossMatching(crossover, 50);
            var replacer = new GenerationReplacement(new TrimmingReplacement(300));
            replacer.AddInheritanceSelector(new TrimmingReplacement(10));
            var fitnessCalc = new MinDemandEvaluator();
            var adjuster = new RandomAdjuster();
            var initialPopulation = new RandomInitialPopulation(50);
            ga = new GeneticAlgorithm(selector, criterion,
                mutation, crossoverMatch, replacer, fitnessCalc,
                adjuster, initialPopulation, problem);
        }
        
        public void setupILS()
        {
            var decider = new IteratedLocalSearch.InitialSolutionGenerators.UnfeasableSolutionDecider.IterationUnfeasableDecider(10000);
            ils = new IteratedLocalSearch.ILS(problem,
                new MinDemandEvaluator(),
                new IteratedLocalSearch.LocalSearch.OneFlipLS(),
                new IteratedLocalSearch.InitialSolutionGenerators.OneDistributerGenerator(
                    problem,
                    decider, 0.8),
                new IteratedLocalSearch.Perturbation.AdaptivePerturbation(
                    new IteratedLocalSearch.Perturbation.OneSwitchPerturbation(),//problematicno,ima dve instance iste klase na razlicita mesta
                    50, 0.9, 0.01
                    ),
                new IteratedLocalSearch.AcceptanceCriteria.RestartWalk(
                    500, new IteratedLocalSearch.AcceptanceCriteria.BetterWalk(),
                    new IteratedLocalSearch.InitialSolutionGenerators.OneDistributerGenerator(problem,
                        decider, 0.3)),
                new IteratedLocalSearch.StoppingCriteria.IterationalStoppingCriterion(1000).AppendStoppingCriteria(
                    new IteratedLocalSearch.StoppingCriteria.TimeStoppingCriterion(10000))
                        );
        }
        public double execute(Reports.ExecutionReportBase report=null)
        {
            CFLP_GA.IteratedLocalSearch.Solution res;
            return execute(out res, report);
        }
        public double execute(out IteratedLocalSearch.Solution result,Reports.ExecutionReportBase report=null)
        {
            GenePopulation Genes=ga.start(report);
            if (Genes == null)
            {
                result = null;
                return double.NaN;
            }
            while (!ga.stoppingCriterion.CheckStoppingCriterion(Genes))
            {
                Genes = ga.iteration(Genes, report);
            }
            Genes = ga.end(Genes, report);
            double min = Genes.Min.fitness();
            result = Genes.Min;
            foreach(Genome g in Genes)
            {
                IteratedLocalSearch.Reports.IterationalReport.on = false;
                IteratedLocalSearch.Reports.ShortReport.on = false;
                IteratedLocalSearch.Reports.VerboseReport.on = false;
                IteratedLocalSearch.Solution resultTemp;
                double afterils=ils.execute(out resultTemp,g);
                if (afterils < min)
                {
                    result = resultTemp;
                    min = afterils;
                }
            }
            Console.WriteLine(min);
            return min;
        }
    }
}

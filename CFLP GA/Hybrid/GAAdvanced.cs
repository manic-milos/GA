﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CFLP_GA.IteratedLocalSearch;
using System.Diagnostics;

namespace CFLP_GA.Hybrid
{
    class GAAdvanced
    {
        public GeneticAlgorithm ga;
        IteratedLocalSearch.ILS ils;
        public Problem problem;
        public double lastResult=double.NaN;
        public EvaluatorBase evaluator;
        public string iterationsToBestResult = "";
        public Stopwatch timer = new Stopwatch();
        public TimeSpan timeToBestResult = new TimeSpan(0);
        public GAAdvanced(Problem problem)
        {
            this.problem = problem;
            evaluator = new MinDemandEvaluator(new EvaluationCache(problem.m));
        }
        public void setupGA()
        {
            var selector = new RankBasedSelector(20, 2.0);
            var criterion = new GenerationLimitCriterion(50);
            var mutator = new RandomWithPreferenceMutator();
            var mutation = new SureRandomMutation(mutator);
            var crossover = new PairUniformCross();
            var crossoverMatch = new StochasticCrossMatching(crossover, 50);
            var replacer = new GenerationReplacement(new TrimmingReplacement(300));
            replacer.AddInheritanceSelector(new TrimmingReplacement(10));
            var fitnessCalc = evaluator;
            var adjuster = new RandomAdjuster();
            var decider = new IteratedLocalSearch.InitialSolutionGenerators.UnfeasableSolutionDecider.IterationUnfeasableDecider(10000);
            int size = 500;
            //var initialPopulation = new RandomInitialPopulation(size);
            var initialPopulation = new NInitialSolutionsGenerator(
                new IteratedLocalSearch.InitialSolutionGenerators.OneDistributerGenerator(problem, decider,0.8),
                size, new FactorUnfeasableDecider(size));
            ga = new GeneticAlgorithm(selector, criterion,
                mutation, crossoverMatch, replacer, fitnessCalc,
                adjuster, initialPopulation, problem);
        }
        
        public void setupILS()
        {
            var decider = new IteratedLocalSearch.InitialSolutionGenerators.UnfeasableSolutionDecider.IterationUnfeasableDecider(10000);
            ils = new IteratedLocalSearch.ILS(problem,
                evaluator,
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
                new IteratedLocalSearch.StoppingCriteria.IterationalStoppingCriterion(100)//.AppendStoppingCriteria(new IteratedLocalSearch.StoppingCriteria.TimeStoppingCriterion(10000))
                        );
        }
        public double execute(Reports.ExecutionReportBase report=null)
        {
            CFLP_GA.IteratedLocalSearch.Solution res;
            return execute(out res, report);
        }
        public double execute(out IteratedLocalSearch.Solution result,Reports.ExecutionReportBase report=null)
        {
            Execution_Reports.ReportController.progressReport.startCounting();
            timer.Restart();
            GenePopulation Genes=ga.start();
            double min = double.MaxValue;
            Execution_Reports.ReportController.DebugLogReport(this, Genes.Count.ToString());
            if (Genes == null)
            {
                result = null;
                return double.NaN;
            }
            ga.stoppingCriterion.Init(ga);
            while (!ga.stoppingCriterion.CheckStoppingCriterion(Genes))
            {
                double newmin = Genes.Min.fitness();
                if(newmin<min)
                {
                    iterationsToBestResult = ga.stoppingCriterion.CurrentIteration();
                    timeToBestResult = timer.Elapsed;
                }
                min = newmin;
                Genes = ga.iteration(Genes);
                Execution_Reports.ReportController.progressReport.addCount(ga.stoppingCriterion.CurrentIteration());
            }
            Genes = ga.end(Genes);
            min = Genes.Min.fitness();
            result = Genes.Min;
            Execution_Reports.ReportController.progressReport.endCount();
            foreach(Genome g in Genes)
            {
                IteratedLocalSearch.Solution resultTemp;
                setupILS();
                double afterils = 0;
                try
                {
                    afterils = ils.execute(out resultTemp, g);
                }
                catch(OutOfMemoryException ex)
                {
                    lastResult = ils.lastResult;
                    throw;
                }
                if (afterils < min)
                {
                    result = resultTemp;
                    min = afterils;
                    iterationsToBestResult = "ils";
                    timeToBestResult = timer.Elapsed;
                }

            }
            return min;
        }
    }
}

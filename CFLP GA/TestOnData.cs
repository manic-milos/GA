using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA
{
    class TestOnData
    {
        public bool testBothOnFolder(string path)
        {
            string[] files;
            if (!Directory.Exists(path))
            {
                files = new string[] { path };
            }
            else
            {
                files = Directory.GetFiles(path);
            }
            foreach (string file in files)
            {
                testGAOnFile(file);
                testILSOnFile(file);
            }
            return true;
        }
        public bool testGAOnFolder(string path)
        {
            string[] files;
            if (!Directory.Exists(path))
            {
                files = new string[] { path };
            }
            else
            {
                files = Directory.GetFiles(path);
            }
            foreach (string file in files)
            {
                testGAOnFile(file);
            }
            return true;
        }
        public bool testGAOnFile(string file)
        {
            Console.WriteLine(file);
            //Console.Read();
            Problem problem = new Problem();
            problem.load(new StreamReader(file));
            //Console.WriteLine(problem.print());
            //Problem.load(new StreamReader(@"F:\Projects\ConsoleApplication8\ConsoleApplication8\test1.txt"));
            //Console.WriteLine(Problem.print());
            var selector = new RankBasedSelector(20, 2.0);
            var criterion = new GenerationLimitCriterion();
            var mutator = new RandomWithPreferenceMutator();
            var mutation = new SureRandomMutation(mutator);
            var crossover = new PairUniformCross();
            var crossoverMatch = new PairwiseCrossMatching(crossover);
            var replacer = new GenerationReplacement(new TrimmingReplacement(300));
            replacer.AddInheritanceSelector(new TrimmingReplacement(10));
            var fitnessCalc = new MinDemandEvaluator();
            var adjuster = new RandomAdjuster();
            var initialPopulation = new RandomInitialPopulation(50);
            GeneticAlgorithm ga = new GeneticAlgorithm(selector, criterion,
                mutation, crossoverMatch, replacer, fitnessCalc,
                adjuster, initialPopulation, problem);
            ga.execute(new Reports.ShortTabularFunctionalReport());
            return true;
        }
        public bool testILSOnFolder(string path)
        {
            string[] files;
            if (!Directory.Exists(path))
            {
                files = new string[] { path };
            }
            else
            {
                files = Directory.GetFiles(path);
            }
            foreach (string file in files)
            {
                testILSOnFile(file);
            }
            return true;
        }
        public bool testILSOnFile(string file)
        {
            Console.WriteLine(file);
            //Console.Read();
            Problem problem = new Problem();
            problem.load(new StreamReader(file));
            var decider = new IteratedLocalSearch.InitialSolutionGenerators.UnfeasableSolutionDecider.IterationUnfeasableDecider(int.MaxValue);
            IteratedLocalSearch.ILS ils = new IteratedLocalSearch.ILS(problem,
                new MinDemandEvaluator(),
                new IteratedLocalSearch.LocalSearch.OneFlipLS(),
                new IteratedLocalSearch.InitialSolutionGenerators.OneDistributerGenerator(
                    problem,
                    decider, 0.8),
                new IteratedLocalSearch.Perturbation.AdaptivePerturbation(
                    new IteratedLocalSearch.Perturbation.SwitchWorstPerturbation(
                        new MinDemandEvaluator()),//problematicno,ima dve instance iste klase na razlicita mesta
                    50, 0.9, 0.01
                    ),
                new IteratedLocalSearch.AcceptanceCriteria.RestartWalk(
                    100, new IteratedLocalSearch.AcceptanceCriteria.BetterWalk(),
                    new IteratedLocalSearch.InitialSolutionGenerators.OneDistributerGenerator(problem,
                        decider, 0.3)),
                new IteratedLocalSearch.StoppingCriteria.IterationalStoppingCriterion(1000).AppendStoppingCriteria(
                    new IteratedLocalSearch.StoppingCriteria.TimeStoppingCriterion(10000).AppendStoppingCriteria(
                    new IteratedLocalSearch.StoppingCriteria.PlateauStoppingCriterion(500)))
                        );
            ils.execute();
            return true;
        }
    }
}

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
            StreamWriter writer = new StreamWriter("results3-1testlist.txt");
            IteratedLocalSearch.Reports.ShortReport.Init(writer);
            TestList testlist = new TestList(path);
            //testlist.loadAllFilesFromBaseFolder();
            testlist.loadSelectFiles(new List<string>() { "capa1_5", "capa1_6" });
            foreach (string file in testlist.files)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    if(key.Key==ConsoleKey.Escape)
                    {
                        writer.Dispose();
                        return true;
                    }
                }
                Console.WriteLine(Path.GetFileName(file));
                IteratedLocalSearch.Reports.ShortReport.Report(Path.GetFileName(file));
                testGAOnFile(file);
                testILSOnFile(file);
            }
            writer.Dispose();
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
                Console.WriteLine(Path.GetFileName(file));
                testGAOnFile(file);
            }
            return true;
        }
        public bool testGAOnFile(string file)
        {
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
            var crossoverMatch = new StochasticCrossMatching(crossover,50);
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
            StreamWriter writer = new StreamWriter("resultspopravljenbag.txt");
            IteratedLocalSearch.Reports.ShortReport.Init(writer);
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
                Console.WriteLine(Path.GetFileName(file));
                IteratedLocalSearch.Reports.ShortReport.Report(Path.GetFileName(file));
                testILSOnFile(file);
            }
            return true;
        }
        public bool testILSOnFile(string file)
        {
            //Console.Read();
            Problem problem = new Problem();
            problem.load(new StreamReader(file));
            var decider = new IteratedLocalSearch.InitialSolutionGenerators.UnfeasableSolutionDecider.IterationUnfeasableDecider(10000);
            IteratedLocalSearch.ILS ils = new IteratedLocalSearch.ILS(problem,
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
            ils.execute();
            return true;
        }
    }
}

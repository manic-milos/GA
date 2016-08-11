using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CFLP_GA.Execution_Reports;

namespace CFLP_GA
{
    class TestOnData
    {
        public bool testSelectOnFolder(string path, bool GAf = true, bool ILSf = true, bool GAAf = true)
        {
            //ResultTests.ShortResultLoader resultLoader = new ResultTests.ShortResultLoader();
            //resultLoader.load("short_results.txt");
            //foreach(ResultTests.Result result in resultLoader.results)
            //{
            //    Console.WriteLine(result);
            //}
            //Console.Read();
            IteratedLocalSearch.Reports.IterationalReport.on = false;
            IteratedLocalSearch.Reports.ShortReport.on = false;
            IteratedLocalSearch.Reports.VerboseReport.on = false;
            StreamWriter writer = new StreamWriter("results4-1popravljenrandomseed.txt");
            IteratedLocalSearch.Reports.ShortReport.Init(writer);
            TestList testlist = new TestList(path);
            testlist.loadAllFilesFromBaseFolder();

            ReportController.HelperSetup();
            //testlist.loadSelectFiles(new List<string>() { "pn58","pn59","pn60","pn61","pn62","pn63", "pn64","pn65",
            //"pn66","pn67","pn68","pn69","pn69_1","pn70","pn71"});
            foreach (string file in testlist.files)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Escape)
                    {
                        writer.Dispose();
                        ReportController.Dispose();
                        return true;
                    }
                }
                Console.WriteLine(Path.GetFileName(file));
                IteratedLocalSearch.Reports.ShortReport.Report(Path.GetFileName(file));
                System.GC.Collect();
                ReportController.Broadcast(1,Path.GetFileName(file));
                if (GAf)
                {
                    ReportController.Broadcast(2, "GA:");
                    Execution_Reports.ReportController.timeReport.startMeasuring("GA");
                    ReportController.Broadcast(1, testGAOnFile(file).ToString());
                    TimeSpan time=Execution_Reports.ReportController.timeReport.stopMeasuring("GA");
                    Execution_Reports.ReportController.Broadcast(3, time.ToString());
                    ControlledRandom.reset();
                }
                System.GC.Collect();
                if (ILSf)
                {
                    ReportController.Broadcast(2, "ILS:");
                    Execution_Reports.ReportController.timeReport.startMeasuring("ILS");
                    ReportController.Broadcast(1, testILSOnFile(file).ToString());
                    TimeSpan time = Execution_Reports.ReportController.timeReport.stopMeasuring("ILS");
                    Execution_Reports.ReportController.Broadcast(3, time.ToString());
                    ControlledRandom.reset();
                }
                System.GC.Collect();
                if (GAAf)
                {
                    ReportController.Broadcast(2, "GAA:");
                    Execution_Reports.ReportController.timeReport.startMeasuring("GAA");
                    ReportController.Broadcast(1, testHybridOnFile(file).ToString());
                    TimeSpan time = Execution_Reports.ReportController.timeReport.stopMeasuring("GAA");
                    Execution_Reports.ReportController.Broadcast(3, time.ToString());
                    ControlledRandom.reset();
                }
                System.GC.Collect();
            }
            writer.Dispose();
            ReportController.Dispose();
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
        public double testGAOnFile(string file)
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
            var crossoverMatch = new StochasticCrossMatching(crossover, 50);
            var replacer = new GenerationReplacement(new TrimmingReplacement(300));
            replacer.AddInheritanceSelector(new TrimmingReplacement(10));
            var fitnessCalc = new MinDemandEvaluator();
            var adjuster = new RandomAdjuster();
            var initialPopulation = new RandomInitialPopulation(50);
            GeneticAlgorithm ga = new GeneticAlgorithm(selector, criterion,
                mutation, crossoverMatch, replacer, fitnessCalc,
                adjuster, initialPopulation, problem);
            double value;
            try
            {
                Genome result;
                value = ga.execute(out result);
                ReportController.Broadcast(2, result.ToString());
            }
            catch(UnfeasableProblemException e)
            {
                value=double.NaN;
                ReportController.Broadcast(6, "UnfeasableProblemException:"+e.Message);
                ReportController.Broadcast(2, "Problem unfeasable");
            }
            catch(OutOfMemoryException e)
            {
                value = double.NaN;
                ReportController.Broadcast(6, "OutOfMemoryException:" + e.Message);
                ReportController.Broadcast(2, "Out of memory");
            }
            return value;

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
        public double testILSOnFile(string file)
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
            IteratedLocalSearch.Solution result;
            double value;
            try
            {
                value = ils.execute(out result);
                ReportController.Broadcast(2, result.ToString());
            }
            catch(UnfeasableProblemException e)
            {
                value = double.NaN;
                ReportController.Broadcast(6, "UnfeasableProblemException:" + e.Message);
                ReportController.Broadcast(2, "Problem unfeasable");
            }
            catch (OutOfMemoryException e)
            {
                value = double.NaN;
                ReportController.Broadcast(6, "OutOfMemoryException:" + e.Message);
                ReportController.Broadcast(2, "Out of memory");
            }
            return value;

        }
        public double testHybridOnFile(string file)
        {
            Problem problem = new Problem();
            problem.load(new StreamReader(file));
            Hybrid.GAAdvanced gaa = new Hybrid.GAAdvanced(problem);
            gaa.setupGA();
            gaa.setupILS();
            CFLP_GA.IteratedLocalSearch.Solution result;
            double value;
            try
            {
                value = gaa.execute(out result);
                ReportController.Broadcast(2, result.ToString());
            }
            catch (UnfeasableProblemException e)
            {
                value = double.NaN;
                ReportController.Broadcast(6, "UnfeasableProblemException:" + e.Message);
                ReportController.Broadcast(2, "Problem unfeasable");
            }
            catch (OutOfMemoryException e)
            {
                value = double.NaN;
                ReportController.Broadcast(6, "OutOfMemoryException:" + e.Message);
                ReportController.Broadcast(2, "Out of memory");
            }
            return value;
        }
    }
}

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
        public bool testSelectOnFolder(string path, bool GAf = true, bool ILSf = true, bool GAAf = true, bool Memf = true)
        {
            //ResultTests.ShortResultLoader resultLoader = new ResultTests.ShortResultLoader();
            //resultLoader.load("short_results.txt");
            //foreach(ResultTests.Result result in resultLoader.results)
            //{
            //    Console.WriteLine(result);
            //}
            //Console.Read();
            TestList testlist = new TestList(path);
            testlist.loadAllFilesFromBaseFolder();
            //testlist.loadSelectFiles(new List<string> { "cap101" });
            //testlist.loadSelectFiles(new List<string> { "capb1", "capb2", "capb3", "capb4" });
            //testlist.loadSelectFiles(new List<string> { "capb1_1", "capb2_1", "capb3_1", "capb4_1" });
            //testlist.loadSelectFiles(new List<string>() { "pn56", "pn57", "pn58", "pn59", "pn60" });
            //testlist.loadSelectFiles(new List<string> { "pn61", "pn62", "pn63", "pn64", "pn65" });
            //testlist.loadSelectFiles(new List<string> { "pn66", "pn67", "pn68", "pn69", "pn69_1", "pn70", "pn71" });

            ReportController.HelperSetup();
            foreach (string file in testlist.files)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Escape)
                    {
                        ReportController.Dispose();
                        return true;
                    }
                }
                Console.WriteLine(Path.GetFileName(file));
                System.GC.Collect();
                ReportController.Broadcast(1, Path.GetFileName(file));
                Problem problem = new Problem();
                problem.load(new StreamReader(file));
                ReportController.Broadcast(2, "n=" + problem.n + ", m=" + problem.m + ", k=" + problem.k);
                if (GAf)
                {
                    ControlledRandom.reset();
                    ReportController.Broadcast(2, "GA:");
                    Execution_Reports.ReportController.timeReport.startMeasuring("GA");
                    ReportController.Broadcast(1, testGAOnFile(file).ToString());
                    TimeSpan time = Execution_Reports.ReportController.timeReport.stopMeasuring("GA");
                    Execution_Reports.ReportController.Broadcast(3, time.ToString());
                    ControlledRandom.reset();
                }
                System.GC.Collect();
                if (ILSf)
                {
                    ControlledRandom.reset();
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
                    ControlledRandom.reset();
                    ReportController.Broadcast(2, "GAA:");
                    Execution_Reports.ReportController.timeReport.startMeasuring("GAA");
                    ReportController.Broadcast(1, testHybridOnFile(file).ToString());
                    TimeSpan time = Execution_Reports.ReportController.timeReport.stopMeasuring("GAA");
                    Execution_Reports.ReportController.Broadcast(3, time.ToString());
                    ControlledRandom.reset();
                }
                System.GC.Collect();
                if (Memf)
                {
                    ControlledRandom.reset();
                    ReportController.Broadcast(2, "Mem:");
                    Execution_Reports.ReportController.timeReport.startMeasuring("Mem");
                    ReportController.Broadcast(1, testMemOnFile(file).ToString());
                    TimeSpan time = Execution_Reports.ReportController.timeReport.stopMeasuring("Mem");
                    Execution_Reports.ReportController.Broadcast(3, time.ToString());
                    ControlledRandom.reset();
                }
                System.GC.Collect();
            }
            ReportController.Dispose();
            return true;
        }
        public double testGAOnFile(string file, Problem problem = null)
        {
            //Console.Read();
            if (problem == null)
            {
                problem = new Problem();
                problem.load(new StreamReader(file));
            }
            //Console.WriteLine(problem.print());
            //Problem.load(new StreamReader(@"F:\Projects\ConsoleApplication8\ConsoleApplication8\test1.txt"));
            //Console.WriteLine(Problem.print());
            var selector = new RankBasedSelector(50, 2.0);
            //var selector = new FineGrainedTournamentSelector(15.5, 100);
            var criterion = new GenerationLimitCriterion(50);
            var mutator = new RandomWithPreferenceMutator();
            var mutation = new SureRandomMutation(mutator);
            var crossover = new PairUniformCross();
            var crossoverMatch = new StochasticCrossMatching(crossover, 100);
            //var replacer = new GenerationReplacement(new TrimmingReplacement((int)2*problem.m+(int)2*problem.n));
            var replacer = new GenerationReplacement(new TrimmingReplacement(100));
            replacer.AddInheritanceSelector(new TrimmingReplacement(10));
            var fitnessCalc = new MinDemandEvaluator(new EvaluationCache(problem.m));
            var adjuster = new RandomAdjuster();
            var initialPopulation = new RandomInitialPopulation(150);
            GeneticAlgorithm ga = new GeneticAlgorithm(selector, criterion,
                mutation, crossoverMatch, replacer, fitnessCalc,
                adjuster, initialPopulation, problem);
            double value;
            try
            {
                Genome result;
                value = ga.execute(out result);
                ReportController.Broadcast(2, result.ToString());
                ReportController.DebugLogReport(this, "iterations to result=" + Environment.NewLine + ga.iterationsToBestResult);
                ReportController.DebugLogReport(this, ",time to best result=" + Environment.NewLine + ga.timeToBestResult);
                ReportController.DebugLogReport(this, ",number of generations=" + Environment.NewLine +ga.stoppingCriterion.CurrentIteration());
                ReportController.DebugLogReport(this, ",calls to eval=" + Environment.NewLine + ga.fitnessCalc.evaluationCalls);
                ReportController.DebugLogReport(this, ",cache hits=" + Environment.NewLine + ga.fitnessCalc.cache.hits);
            }
            catch (UnfeasableProblemException e)
            {
                value = double.NaN;
                ReportController.Broadcast(6, "UnfeasableProblemException:" + e.Message);
                ReportController.Broadcast(2, "Problem unfeasable");
            }
            catch (OutOfMemoryException e)
            {
                value = ga.lastResult;
                ReportController.Broadcast(6, "OutOfMemoryException:" + e.Message);
                ReportController.Broadcast(2, "Out of memory");
            }
            catch (Exception e)
            {
                ReportController.Broadcast(6, "Exception:" + e.Message);
                value = double.NaN;
            }
            return value;

        }
        public double testILSOnFile(string file, Problem problem = null)
        {
            //Console.Read();
            if (problem == null)
            {
                problem = new Problem();
                problem.load(new StreamReader(file));
            }
            var decider = new IteratedLocalSearch.InitialSolutionGenerators.UnfeasableSolutionDecider.IterationUnfeasableDecider(10000);
            IteratedLocalSearch.ILS ils = new IteratedLocalSearch.ILS(problem,
                new MinDemandEvaluator(new EvaluationCache(problem.m)),
                new IteratedLocalSearch.LocalSearch.OneFlipLS(),
                new IteratedLocalSearch.InitialSolutionGenerators.OneDistributerGenerator(
                    problem,
                    decider, 0.8),
                new IteratedLocalSearch.Perturbation.AdaptivePerturbation(
                    new IteratedLocalSearch.Perturbation.OneSwitchPerturbation(),
                    50, 0.9, 0.01
                    ),
                new IteratedLocalSearch.AcceptanceCriteria.RestartWalk(
                    500, new IteratedLocalSearch.AcceptanceCriteria.BetterWalk(),
                    new IteratedLocalSearch.InitialSolutionGenerators.OneDistributerGenerator(problem,
                        decider, 0.3)),
                new IteratedLocalSearch.StoppingCriteria.IterationalStoppingCriterion(1000)
                        );
            IteratedLocalSearch.Solution result;
            double value;
            try
            {
                value = ils.execute(out result);
                ReportController.Broadcast(2, result.ToString());
                ReportController.DebugLogReport(this, "iterations to result=" + Environment.NewLine + ils.iterationsToBestResult);
                ReportController.DebugLogReport(this, ",time to best result=" + Environment.NewLine +ils.timeToBestResult);
                //ReportController.DebugLogReport(this, Environment.NewLine+",number of generations=" + ils.stoppingCriterion.IterationInfoAll());
                ReportController.DebugLogReport(this, ",calls to eval=" + Environment.NewLine + ils.evaluator.evaluationCalls);
                ReportController.DebugLogReport(this, ",cache hits=" + Environment.NewLine + ils.evaluator.cache.hits);
            }
            catch (UnfeasableProblemException e)
            {
                value = double.NaN;
                ReportController.Broadcast(6, "UnfeasableProblemException:" + e.Message);
                ReportController.Broadcast(2, "Problem unfeasable");
            }
            catch (OutOfMemoryException e)
            {
                value = ils.lastResult;
                ReportController.Broadcast(6, "OutOfMemoryException:" + e.Message);
                ReportController.Broadcast(2, "Out of memory");
            }
            catch (Exception e)
            {
                ReportController.Broadcast(6, "Exception:" + e.Message);
                value = double.NaN;
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
                ReportController.DebugLogReport(this, "iterations to result=" + Environment.NewLine + gaa.iterationsToBestResult);
                ReportController.DebugLogReport(this, ",time to best result=" + Environment.NewLine + gaa.timeToBestResult);
                ReportController.DebugLogReport(this, ",number of generations=" + Environment.NewLine + gaa.ga.stoppingCriterion.CurrentIteration());
                ReportController.DebugLogReport(this, ",calls to eval=" + Environment.NewLine + gaa.evaluator.evaluationCalls);
                ReportController.DebugLogReport(this, ",cache hits=" + Environment.NewLine + gaa.evaluator.cache.hits);

            }
            catch (UnfeasableProblemException e)
            {
                value = double.NaN;
                ReportController.Broadcast(6, "UnfeasableProblemException:" + e.Message);
                ReportController.Broadcast(2, "Problem unfeasable");
            }
            catch (OutOfMemoryException e)
            {
                value = gaa.lastResult;
                ReportController.Broadcast(6, "OutOfMemoryException:" + e.Message);
                ReportController.Broadcast(2, "Out of memory");
            }
            catch (Exception e)
            {
                ReportController.Broadcast(6, "Exception:" + e.Message);
                value = double.NaN;
            }
            return value;
        }
        public double testMemOnFile(string file)
        {
            Problem problem = new Problem();
            problem.load(new StreamReader(file));
            Hybrid.Memetic gaa = new Hybrid.Memetic(problem);
            gaa.setupGA();
            gaa.setupILS();
            CFLP_GA.IteratedLocalSearch.Solution result;
            double value;
            try
            {
                value = gaa.execute(out result);
                ReportController.Broadcast(2, result.ToString());
                ReportController.DebugLogReport(this, "iterations to result=" + Environment.NewLine + gaa.iterationsToBestResult);
                ReportController.DebugLogReport(this, ",time to best result=" + Environment.NewLine + gaa.timeToBestResult);
                ReportController.DebugLogReport(this, ",number of generations=" + Environment.NewLine + gaa.ga.stoppingCriterion.CurrentIteration());
                ReportController.DebugLogReport(this, ",calls to eval=" + Environment.NewLine + gaa.evaluator.evaluationCalls);
                ReportController.DebugLogReport(this, ",cache hits=" + Environment.NewLine + gaa.evaluator.cache.hits);
            }
            catch (UnfeasableProblemException e)
            {
                value = double.NaN;
                ReportController.Broadcast(6, "UnfeasableProblemException:" + e.Message);
                ReportController.Broadcast(2, "Problem unfeasable");
            }
            catch (OutOfMemoryException e)
            {
                value = gaa.lastResult;
                ReportController.Broadcast(6, "OutOfMemoryException:" + e.Message);
                ReportController.Broadcast(2, "Out of memory");
            }
            catch (Exception e)
            {
                ReportController.Broadcast(6, "Exception:" + e.Message);
                value = double.NaN;
            }
            return value;
        }
    }
}

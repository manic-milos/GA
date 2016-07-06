using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA
{
    class Program
    {
        public static GeneticAlgorithm ga;
        static void Main(string[] args)
        {
            TestOnData test = new TestOnData();
            test.testBothOnFolder(@"D:\reinstalacija\F\Projects\ConsoleApplication8\ConsoleApplication8\FormatedInstances",
                5);
            Console.Read();
            Problem problem = new Problem();
            problem.load(new StreamReader(@"F:\Projects\ConsoleApplication8\ConsoleApplication8\FormatedInstances\cap61"));
            Console.WriteLine(problem.print());
            //Problem.load(new StreamReader(@"F:\Projects\ConsoleApplication8\ConsoleApplication8\test1.txt"));
            //Console.WriteLine(Problem.print());
            var selector = new RankBasedSelector(20,2.0);
            var criterion=new GenerationLimitCriterion();
            var mutator=new RandomWithPreferenceMutator();
            var mutation=new SureRandomMutation(mutator);
            var crossover=new PairUniformCross();
            var crossoverMatch = new PairwiseCrossMatching(crossover);
            var replacer=new GenerationReplacement(new TrimmingReplacement(300));
            replacer.AddInheritanceSelector(new TrimmingReplacement(10));
            var fitnessCalc=new MinDemandEvaluator();
            var adjuster=new RandomAdjuster();
            var initialPopulation = new RandomInitialPopulation(50);
            ga = new GeneticAlgorithm(selector, criterion,
                mutation, crossoverMatch, replacer, fitnessCalc,
                adjuster,initialPopulation, problem);
            ga.execute();
        }
    }
}

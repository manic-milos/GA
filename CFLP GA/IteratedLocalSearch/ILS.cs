using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA.IteratedLocalSearch
{
    class ILS
    {
        public Problem problem;
        public EvaluatorBase evaluator;
        public LocalSearch.LocalSearchBase localSearch;
        public InitialSolutionGenerators.InitialSolutionGeneratorBase generator;
        public Perturbation.PerturbationBase perturber;
        public AcceptanceCriteria.AcceptanceCriterionBase acceptanceCriterion;
        public StoppingCriteria.StoppingCriterionBase stoppingCriterion;
        public ILS(Problem problem,
            EvaluatorBase evaluator,
            LocalSearch.LocalSearchBase localSearch,
            InitialSolutionGenerators.InitialSolutionGeneratorBase generator,
            Perturbation.PerturbationBase perturber,
            AcceptanceCriteria.AcceptanceCriterionBase acceptanceCriterion,
            StoppingCriteria.StoppingCriterionBase stoppingCriterion
            )
        {
            this.problem = problem;
            this.evaluator = evaluator;
            this.localSearch = localSearch;
            this.generator = generator;
            this.perturber = perturber;
            this.acceptanceCriterion = acceptanceCriterion;
            this.stoppingCriterion = stoppingCriterion;
        }
        public double execute(Solution initialSolution = null)
        {
            Solution result;
            return execute(out result, initialSolution);
        }
        public double execute(out Solution result,Solution initialSolution = null)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Restart();
            Solution s=initialSolution;
            if(initialSolution==null)
                s = generator.Generate();
            if (s == null)
            {
                result = null;
                return double.NaN;
            }
            Solution globalBest = s;
            while (!stoppingCriterion.CheckIfEnd(s,globalBest))
            {
                Reports.IterationalReport.Report("iteration:"+stoppingCriterion.IterationInfoAll());
                s = localSearch.GetBestLocal(s, evaluator);
                if(evaluator.Evaluate(s)<evaluator.Evaluate(globalBest))
                {

                    Reports.VerboseReport.Report("global best changed from " + globalBest + " to " + s);
                    globalBest = s;
                }
                Reports.IterationalReport.Report("local best:\t"+s+","+evaluator.Evaluate(s));
                Solution perturbed = perturber.Perturb(s);
                if (perturbed == null)
                {
                    Reports.IterationalReport.Report("perturbed null");
                }
                else
                {
                    Reports.IterationalReport.Report("perturbed:\t" + perturbed + "," + evaluator.Evaluate(perturbed));
                    Solution accepted = acceptanceCriterion.Accept(perturbed, s, evaluator);
                    if(accepted==null)
                        Console.WriteLine("wrong");
                    s = accepted;
                }
                Reports.IterationalReport.IterationEnd("solution accepted: " + s);
            }
            Reports.IterationalReport.FinalIteration("best solution:");
            Reports.ShortReport.Report(evaluator.Evaluate(globalBest) + " " + globalBest);
            if(!s.check())
            {
                throw new Exception("Solution is not correct");
            }
            stopwatch.Stop();
            Reports.ShortReport.Report(stopwatch.Elapsed.ToString());
            result = s;
            return evaluator.Evaluate(s);
        }
    }
}

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
        public double lastResult = double.NaN;
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
            Execution_Reports.ReportController.progressReport.startCounting();
            Solution s=initialSolution;
            if(initialSolution==null)
                s = generator.Generate();
            if (s == null)
            {
                result = null;
                return double.NaN;
            }
            Solution globalBest = s;
            lastResult = evaluator.Evaluate(s);
            while (!stoppingCriterion.CheckIfEnd(s,globalBest))
            {
                s = localSearch.GetBestLocal(s, evaluator);
                if(evaluator.Evaluate(s)<evaluator.Evaluate(globalBest))
                {

                    globalBest = s;
                }
                Solution perturbed = perturber.Perturb(s);
                if(perturbed!=null)
                {
                    Solution accepted = acceptanceCriterion.Accept(perturbed, s, evaluator);
                    if(accepted==null)
                        Console.WriteLine("wrong");
                    s = accepted;
                }
                lastResult = evaluator.Evaluate(s);
                Execution_Reports.ReportController.progressReport.addCount(stoppingCriterion.IterationInfoAll());
            }
            if(!s.check())
            {
                throw new Exception("Solution is not correct");
            }
            result = s;
            Execution_Reports.ReportController.progressReport.endCount();
            return evaluator.Evaluate(s);
        }
    }
}

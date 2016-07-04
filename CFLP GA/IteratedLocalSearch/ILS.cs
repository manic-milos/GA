﻿using System;
using System.Collections.Generic;
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
        public double execute()
        {
            Console.WriteLine("generating initial solution");
            Solution s = generator.Generate();
            Console.WriteLine("solution generated:"+s);
            if (s == null)
                return double.NaN;
            Console.WriteLine("solution exists");
            Solution globalBest = s;
            while (!stoppingCriterion.CheckIfEnd(s,globalBest))
            {
                Console.WriteLine("iteration:"+stoppingCriterion.IterationInfoAll());
                s = localSearch.GetBestLocal(s, evaluator);
                if(evaluator.Evaluate(s)<evaluator.Evaluate(globalBest))
                {
                    globalBest = s;
                }
                Console.WriteLine("local best:\t"+s+","+evaluator.Evaluate(s));
                Solution perturbed = perturber.Perturb(s);
                Console.WriteLine("perturbed:\t"+perturbed+","+evaluator.Evaluate(perturbed));
                s = acceptanceCriterion.Accept(perturbed, s, evaluator);
            }
            Console.WriteLine("best solution:");
            Console.WriteLine(globalBest+" "+evaluator.Evaluate(globalBest));
            Console.Read();
            return evaluator.Evaluate(s);
        }
    }
}
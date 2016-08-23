using CFLP_GA.IteratedLocalSearch;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA
{
    abstract class EvaluatorBase
    {
        public Stopwatch time = new Stopwatch();
        public TimeSpan overallTime = new TimeSpan(0);
        public EvaluationCache cache = null;
        public EvaluatorBase(EvaluationCache cache = null)
        {
            this.cache = cache;
        }

        public double Fitness(Genome g, int[] leftCapacities = null)
        {
            return Evaluate(g.genes, g.problem, leftCapacities);
        }
        public double Evaluate(Solution g)
        {
            return Evaluate(g.vars, g.problem);
        }
        public double Evaluate(int[] solution, Problem p, int[] leftCapacities = null)
        {
            time.Restart();
            double fitnessValue = checkCache(solution);
            if (fitnessValue < 0)
            {
                fitnessValue = evaluate(solution, p, leftCapacities);
                cacheValue(solution, fitnessValue);
            }
            time.Stop();
            overallTime += time.Elapsed;
            return fitnessValue;
        }
        protected abstract double evaluate(int[] solution, Problem p, int[] leftCapacities = null);
        public double calculateOpeningCosts(int[] g, Problem p)
        {
            double objf = 0;
            for (int i = 0; i < p.m; i++)
            {
                objf += (g[i] * p.f[i]);
            }
            return objf;
        }
        public double checkCache(int[] vars)
        {
            if (cache == null)
                return -1;
            return cache.CheckCache(vars);
        }
        public void cacheValue(int[] vars, double value)
        {
            if (cache == null)
                return;
            cache.AddToCache(vars, value);
        }

    }
}

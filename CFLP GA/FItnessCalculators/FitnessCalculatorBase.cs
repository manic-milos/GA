using CFLP_GA.IteratedLocalSearch;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA
{
    abstract class FitnessCalculatorBase
    {
        public Stopwatch time = new Stopwatch();
        public TimeSpan overallTime = new TimeSpan(0);
        public EvaluationCache cache = null;
        public FitnessCalculatorBase(EvaluationCache cache = null)
        {
            this.cache = cache;
        }

        public double Fitness(Genome g, int[] leftCapacities = null)
        {
            return Fitness(g.genes, g.geneticAlgorithm.problem, leftCapacities);
        }
        public double Fitness(Solution g, int[] leftCapacities = null)
        {
            return Fitness(g.vars, g.problem, leftCapacities);
        }
        public double Fitness(int[] solution, Problem p, int[] leftCapacities = null)
        {
            time.Restart();
            double fitnessValue = checkCache(solution);
            if (fitnessValue < 0)
            {
                fitnessValue = fitness(solution, p, leftCapacities);
                cacheValue(solution, fitnessValue);
            }
            time.Stop();
            overallTime += time.Elapsed;
            return fitnessValue;
        }
        protected abstract double fitness(int[] solution, Problem p, int[] leftCapacities = null);
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
            return cache.checkCache(vars);
        }
        public void cacheValue(int[] vars, double value)
        {
            if (cache == null)
                return;
            cache.cacheValue(vars, value);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA
{
    abstract class InitialPopulationBase
    {
        List<InitialPopulationBase> generators = new List<InitialPopulationBase>();
        public Stopwatch time = new Stopwatch();
        public TimeSpan overallTime = new TimeSpan(0);
        protected abstract GenePopulation create(GeneticAlgorithm ga);
        public GenePopulation Create(GeneticAlgorithm ga)
        {
            time.Restart();
            GenePopulation population= create(ga);
            foreach(InitialPopulationBase gen in generators)
            {
                population = population.Append(gen.Create(ga));
            }
            time.Stop();
            overallTime += time.Elapsed;
            return population;
        }
        public void Append(InitialPopulationBase generator)
        {
            generators.Add(generator);
        }
    }
}

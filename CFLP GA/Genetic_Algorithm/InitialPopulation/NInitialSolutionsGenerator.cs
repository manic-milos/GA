using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CFLP_GA.IteratedLocalSearch.InitialSolutionGenerators;

namespace CFLP_GA
{
    class NInitialSolutionsGenerator:InitialPopulationBase
    {
        InitialSolutionGeneratorBase generator;
        public int size = 150;
        public UnfeasableDeciderBase decider;
        public NInitialSolutionsGenerator(
            InitialSolutionGeneratorBase generator,
            int size,
            UnfeasableDeciderBase decider
            )
        {
            this.generator = generator;
            this.size = size;
            this.decider = decider;
        }
        protected override GenePopulation create(GeneticAlgorithm ga)
        {
            GenePopulation Genes = new GenePopulation(ga);
            int i = 0;
            while (i++ < size)
            {
                Genome a=new Genome(ga);
                try
                {

                    IteratedLocalSearch.Solution s = generator.Generate();
                    a.genes = s.vars;
                    Genes.Include(a);
                }
                catch(UnfeasableProblemException ex)
                {
                    //Execution_Reports.ReportController.DebugLogReport(this, "unfeasable solution exception");
                    decider.DecideUnfeasable(i, Genes);
                }
            }
            return Genes;
        }
    }
}

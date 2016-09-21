using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA.Genetic_Algorithm
{
    class PlateauStoppingCriterion:StoppingCriterionBase
    {
        int maxGenerations;
        int currentPlateauStatus = 0;
        Genome currentMax = null;
        public PlateauStoppingCriterion(int maxGenerations)
        {
            this.maxGenerations = maxGenerations;
        }
        protected override bool checkStoppingCriterion(GenePopulation genes)
        {
            if(currentMax==null||currentMax!=genes.Max)
            {
                currentMax = genes.Max;
                currentPlateauStatus = 1;
                return false;
            }
            if(++currentPlateauStatus>=maxGenerations)
                return true;
            return false;
        }
        protected override void init(GeneticAlgorithm ga)
        {
            currentMax = null;
            currentPlateauStatus = 0;
        }
        protected override string currentIteration()
        {
            return currentPlateauStatus.ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA
{
    class FineGrainedTournamentSelector:SelectorBase
    {
        public double Ftour;
        int n;
        public FineGrainedTournamentSelector(double Ftour,int n)
        {
            this.Ftour = Ftour;
            this.n = n;
        }
        protected override GenePopulation select(GenePopulation population,
            GeneticAlgorithm ga)
        {
            double FtourMinus=Math.Floor(Ftour);
            double FtourPlus = Math.Floor(Ftour) + 1;
            int nMinus = (int)Math.Floor(n * (FtourPlus - Ftour));
            int nPlus=n-nMinus;
            GenePopulation selected = new GenePopulation(ga);
            Genome[] ordered = new Genome[population.Count];
            int index=0;
            foreach(Genome g in population)
            {
                ordered[index++] = g;
            }
            
            for (int i = 0; i < nMinus;i++)
            {
                double max = 0;
                int maxind = 0;
                for (int j = 0; j < FtourMinus; j++)
                {
                    int ind = ControlledRandom.getRandomFromRange(0, ordered.Length);
                    double fitness = ordered[ind].fitness();
                    if (fitness > max)
                    {
                        max = fitness;
                        maxind = ind;
                    }
                }
                selected.Include(ordered[maxind]);
            }
            for (int i = 0; i < nPlus; i++)
            {
                double max = 0;
                int maxind = 0;
                for (int j = 0; j < FtourPlus; j++)
                {
                    int ind = ControlledRandom.getRandomFromRange(0, ordered.Length);
                    double fitness = ordered[ind].fitness();
                    if (fitness > max)
                    {
                        max = fitness;
                        maxind = ind;
                    }
                }
                selected.Include(ordered[maxind]);
            }
            return selected;

        }
    }
}

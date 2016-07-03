using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA
{
    class RouletteSelector:SelectorBase
    {
        public int crossPool = 20;
        public RouletteSelector(int crossPool)
        {
            this.crossPool = crossPool;
        }
        protected override GenePopulation select(GenePopulation candidates,
            GeneticAlgorithm ga)
        {

            Dictionary<int, Genome> splits = new Dictionary<int, Genome>();
            Dictionary<int, double> parts = new Dictionary<int, double>();
            double sum = 0;
            int n=0;
            foreach(Genome g in candidates)
            {
                double fitness = g.fitness();
                splits.Add(n, g);
                parts.Add(n, fitness);
                n++;
                sum += fitness;
            }
            for(int i=0;i<n;i++)
            {
                parts[i] = parts[i] / sum;
            }
            GenePopulation parents = new GenePopulation();

            for(int i=0;i<crossPool;i++)
            {
                parents.Include(splits[drawOne(parts, n, ga)]);
            }
            return parents;
        }
        private int drawOne(Dictionary<int,double> parts,int n,GeneticAlgorithm ga)
        {
            double draw = ga.settings.rand.NextDouble();
            double sum = 0;
            for(int i=0;i<n;i++)
            {
                sum += parts[i];
                if(sum>=draw)
                {
                    return i;
                }
            }
            throw new Exception("drawOne exception");
        }
    }
}

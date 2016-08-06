using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA
{
    class StochasticCrossMatching:CrossoverMatchingBase
    {
        public int pairs;
        public StochasticCrossMatching(GenomeCrossBase cross, int pairs)
            :base(cross)
        {
            this.pairs = pairs;
        }
        protected override GenePopulation crossGenomes(GenePopulation crossPool,
            GeneticAlgorithm ga)
        {
            GenePopulation children = new GenePopulation(ga);
            List<Genome> glist = new List<Genome>();
            foreach(Genome g in crossPool)
            {
                glist.Add(g);
            }
            int n=glist.Count;
            for(int i=0;i<pairs;i++)
            {
                int a = ControlledRandom.getRandomFromRange(0, n - 1);
                int b = ControlledRandom.getRandomFromRange(0, n - 1);
                Genome gena = glist[a];
                Genome genb = glist[b];
                if(!gena.Equals(genb))
                {
                    var childrenList = cross.crossGenomes(
                        new List<Genome>() { gena, genb }, ga);
                    foreach(Genome child in childrenList)
                    {
                        children.Include(child);
                    }
                }
            }
            return children;
        }
    }
}

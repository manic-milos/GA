using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA
{
    class PairUniformCross : GenomeCrossBase
    {
        public override List<Genome> crossGenomes(List<Genome> parents,
            GeneticAlgorithm ga)
        {
            List<Genome> children = new List<Genome>();
            Genome c1 = new Genome(ga),
                c2 = new Genome(ga);
            if (parents.Count < 2)
            {
                throw new Exception("Needs at least 2 parents!");
            }
            Genome p1 = parents[0];
            Genome p2 = parents[1];
            for (int i = 0; i < ga.problem.m; i++)
            {
                if (ga.settings.getRandomFromRange(0, 2) == 0)
                {
                    c1[i] = p1[i];
                    c2[i] = p2[i];
                }
                else
                {
                    c1[i] = p2[i];
                    c2[i] = p1[i];
                }
            }
            children.Add(c1.adjust());
            children.Add(c2.adjust());
            return children;
        }
    }
}

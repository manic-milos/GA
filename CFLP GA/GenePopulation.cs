using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA
{
    class GenePopulation : SortedSet<Genome>
    {
        public static Stopwatch time = new Stopwatch();
        public static TimeSpan includeTime = new TimeSpan(0);
        public static TimeSpan appendTime = new TimeSpan(0);
        public GeneticAlgorithm ga;
        public GenePopulation(GeneticAlgorithm ga)
        {
            this.ga = ga;
        }
        public void Include(Genome g)
        {
            if (g == null)
                return;
            Genome g1 = ga.adjustGenome(g);
            if (g1 == null)
                return;
            if(!g.checkGenome())
                Console.WriteLine("SDFDSFSDFDSFDS");
            this.Add(g1);
        }
        public GenePopulation Append(GenePopulation other)
        {
            foreach(Genome g in this)
            {
                if (g.checkGenome() == false)
                {
                    Console.WriteLine("greska");
                    this.Remove(g);
                }
            }
            foreach (Genome g in other)
            {
                if (g.checkGenome() == false)
                {
                    Console.WriteLine("greska");
                    other.Remove(g);
                }
            }
            this.UnionWith(other);
            return this;
        }
        public override string ToString()
        {
            string s = "";
            foreach (Genome g in this)
            {
                s += g.ToString() + " "
                    + ga.fitness(g)
                    + Environment.NewLine;
            }
            return s;
        }
        public static implicit operator string(GenePopulation g)
        {
            return g.ToString();
        }
    }
}

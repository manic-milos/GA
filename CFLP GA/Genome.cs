using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA
{
    class Genome : IComparable<Genome>
    {
        public int[] genes;
        public GeneticAlgorithm geneticAlgorithm;
        Problem problem;
        double fitnessVal = double.NaN;
        int sumCapacityVal = -1;
        public Genome(GeneticAlgorithm ga)
        {
            this.geneticAlgorithm = ga;
            problem = ga.problem;
            genes = new int[problem.m];
        }
        public bool checkGenome()
        {
            return problem.checkSolution(genes, sumCapacity());
        }
        public Genome adjust()
        {
            return geneticAlgorithm.adjustGenome(this);
        }
        public double fitness()
        {
            if (double.IsNaN(fitnessVal))
            {
                fitnessVal = geneticAlgorithm.fitness(this);
                return fitnessVal;
            }
            else
            {
                return fitnessVal;
            }

        }
        public int sumCapacity()
        {
            if (sumCapacityVal < 0)
            {
                return problem.sumCapacity(genes);
            }
            else
            {
                return sumCapacityVal;
            }
        }
        public static Genome generateRandom(GeneticAlgorithm ga)
        {
            Genome randgen = new Genome(ga);
            for (int i = 0; i < randgen.genes.Length; i++)
            {
                int r = ga.settings.getRandomFromRange(0, 2);
                randgen[i] = r;
            }
            return randgen;
        }
        public Genome Clone()
        {
            Genome copy = new Genome(geneticAlgorithm);
            for (int i = 0; i < problem.m; i++)
            {
                copy[i] = this[i];
            }
            return copy;
        }
        public int this[int i]
        {
            get
            {
                return this.genes[i];
            }
            set
            {
                this.genes[i] = value;
                sumCapacityVal = -1;
            }
        }
        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Genome other = (Genome)obj;
            for (int i = 0; i < problem.m; i++)
            {
                if (this[i] != other[i])
                    return false;
            }
            //TODO: provera da li pripadaju istom genetskom algoritmu
            return true;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public int CompareTo(Genome g)
        {
            if (this.fitness() == g.fitness())
                return 0;
            if (this.fitness() < g.fitness())
                return -1;
            //if (GeneticAlgorithm.fitness(this) > GeneticAlgorithm.fitness(g))
            return 1;
        }
        public override string ToString()
        {
            string s = "[";
            for (int i = 0; i < problem.m; i++)
            {
                s += this[i].ToString();
            }
            s += "]";
            return s;
        }
        public static implicit operator string(Genome g)
        {
            return g.ToString();
        }
    }
}

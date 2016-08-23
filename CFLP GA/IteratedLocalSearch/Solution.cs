using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA.IteratedLocalSearch
{
    class Solution
    {
        public int[] vars;
        public Problem problem;
        public Solution(Problem problem)
        {
            this.problem = problem;
            vars = new int[problem.m];
        }
        public Solution(Genome genome)
            :this(genome.problem)
        {
            vars = genome.genes;
        }
        public bool check()
        {
            return problem.checkSolution(vars, sumCapacity());
        }

        public int sumCapacity()
        {
            return problem.sumCapacity(vars);
        }
        public static Solution generateRandom(Problem problem)
        {
            Solution rand = new Solution(problem);
            for (int i = 0; i < rand.vars.Length; i++)
            {
                int r = ControlledRandom.getRandomFromRange(0, 2);
                rand[i] = r;
            }
            return rand;
        }
        public Solution Clone()
        {
            Solution copy = new Solution(problem);
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
                return this.vars[i];
            }
            set
            {
                this.vars[i] = value;
            }
        }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Solution other = (Solution)obj;
            if (other.problem.m != this.problem.m)
                return false;
            for (int i = 0; i < vars.Length; i++)
            {
                if (this[i] != other[i])
                    return false;
            }
            return true;
        }
        public static bool operator==(Solution o,Solution s)
        {
            try
            {
                return o.Equals(s);
            }
            catch(NullReferenceException)
            {
                try
                {
                    return s.Equals(o);
                }
                catch(NullReferenceException)
                {
                    return true;
                }
            }
        }
        public static bool operator !=(Solution o, Solution s)
        {
            try
            {
                return !o.Equals(s);
            }
            catch (NullReferenceException)
            {
                try
                {
                    return !s.Equals(o);
                }
                catch (NullReferenceException)
                {
                    return false;
                }
            }
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
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
        public static implicit operator string(Solution s)
        {
            return s.ToString();
        }
        public static implicit operator Solution(Genome g)
        {
            return new Solution(g);
        }
        public Genome ToGenome(GeneticAlgorithm ga)
        {
            Genome g = new Genome(ga);
            g.genes = this.vars;
            return g;
        }
    }
}

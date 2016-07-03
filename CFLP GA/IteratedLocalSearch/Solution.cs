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
        int sumCapacityVal = -1;
        public Solution(Problem problem)
        {
            this.problem = problem;
            vars = new int[problem.m];
        }
        public bool check()
        {
            return problem.checkSolution(vars, sumCapacity());
        }

        public int sumCapacity()
        {
            if (sumCapacityVal < 0)
            {
                int sum = 0;
                for (int i = 0; i < vars.Length; i++)
                {
                    sum += vars[i] * problem.s[i];
                }
                //sumCapacityVal = sum;
                return sum;
            }
            else
            {
                return sumCapacityVal;
            }
        }
        public double evaluate()
        {
            throw new NotImplementedException("fitness->evaluation");
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
                sumCapacityVal = -1;
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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA
{
    class HashedVars:IComparable<HashedVars>
    {
        List<int> vars = new List<int>();


        public int CompareTo(HashedVars other)
        {
            for(int i=0;i<vars.Count;i++)
            {
                if (vars[i] != other.vars[i])
                    return -1;
            }
            return 0;
        }
        public override bool Equals(object obj)
        {
            HashedVars other = (HashedVars)obj;
            for (int i = 0; i < vars.Count; i++)
            {
                if (vars[i] != other.vars[i])
                    return false;
            }
            return true;
        }
        public override int GetHashCode()
        {
            int h = 0;
            foreach(int i in vars)
            {
                h |= 1 >> i;
            }
            return h;
        }

    }
}

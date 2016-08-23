using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA
{
    class EvaluationCache
    {
        CacheDict dict;
        int levels = 0;
        int bitRep = 64;
        public EvaluationCache(int m)
        {
            this.levels = (int)Math.Ceiling(m / (bitRep+0.0));
            dict = new CacheDict(0, levels);
        }
        public void AddToCache(int[] numberString, double value)
        {
            

        }

        //long getKey(int[] numberString,out int[] leftover)
        //{
        //    long result = 0;
        //    if(numberString.Length<bitRep)
        //    {
        //        for(int i=0;i<numberString.Length;i++)
        //        {
        //            result *= 10;
        //            result += numberString[i];
        //        }
        //        leftover = new int[0];
        //    }
        //    else
        //    {
        //        for(int i=0;i<bitRep;i++)
        //        {
        //            result *= 10;
        //            result += numberString[i];
        //        }
        //        leftover = new int[numberString.Length - bitRep];
        //        for(int i=bitRep;i<numberString.Length;i++)
        //        {
        //            leftover[i - bitRep] = numberString[i];
        //        }
        //    }
        //    return result;
        //}

    }
}

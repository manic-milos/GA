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
        int bitRep = 60;
        public EvaluationCache(int m)
        {
            this.levels = (int)Math.Floor(m / (bitRep + 0.0));
            dict = new CacheDict(0, levels);
        }
        public void AddToCache(int[] numberString, double value)
        {
            CacheDict Currentdictionary = dict;
            long key;
            for (int level = 0; level < levels; level++)
            {
                key = getKey(numberString, level);
                CacheDict next;
                if (Currentdictionary.refDict.TryGetValue(key, out next))
                {
                    Currentdictionary = next;
                }
                else
                {
                    next = new CacheDict(level + 1, levels);
                    Currentdictionary.refDict[key] = next;
                    Currentdictionary = next;
                }
            }
            key = getKey(numberString, levels);
            Currentdictionary.valDict[key] = value;

        }
        public double CheckCache(int[] numberString)
        {
            CacheDict Currentdictionary = dict;
            long key;
            double value;
            for (int level = 0; level < levels; level++)
            {
                key = getKey(numberString, level);
                CacheDict next;
                if (Currentdictionary.refDict.TryGetValue(key, out next))
                {
                    Currentdictionary = next;
                }
                else
                {
                    next = new CacheDict(level + 1, levels);
                    Currentdictionary.refDict[key] = next;
                    Currentdictionary = next;
                }
            }
            key = getKey(numberString, levels);

            Console.WriteLine(Convert.ToString(key, 2));
            if (Currentdictionary.valDict.TryGetValue(key, out value))
            {
                return value;
            }
            else
            {
                return double.NaN;
            }
        }
        long getKey(int[] numberString, int level)
        {
            long result = 0;
            int limit;
            if (level < levels)
            {
                limit = (level + 1) * bitRep;
            }
            else
            {
                limit = numberString.Length;
            }
            string numstring = "";
            for (int i = level * bitRep; i < limit; i++)
            {
                //if (numberString[i] > 0)
                //    result |= 1;

                //result <<= 2;
                if (numberString[i] == 0)
                    numstring += "0";
                else
                    numstring += "1";
                //result += numberString[i];
                //if (i < limit - 1)
                //    result *= 2;
            }
            result = Convert.ToInt64(numstring, 2);
            //Console.WriteLine(Convert.ToString(result, 2));
            return result;
        }

    }
}

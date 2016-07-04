using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA
{
    class EvaluationCache
    {
        public class Pair<T1, T2>
        {
            public T1 Value { get; set; }
            public T2 Count { get; set; }
        }
        public Dictionary<int[], Pair<double, int>> valueCache =
            new Dictionary<int[], Pair<double, int>>();
        public int cacheCounter = 0;
        public int cleaningPeriod = 1000;
        long hitCount = 0;
        long missCount = 0;
        public EvaluationCache(int cleaningPeriod=1000)
        {
            throw new NotImplementedException("bugs bugs...");
            this.cleaningPeriod = cleaningPeriod;
        }
        public double checkCache(int[] vars)
        {
            updateCache();
            try
            {
                var found = valueCache[vars];
                found.Count = cacheCounter;
                hitCount++;
                return found.Value;
            }
            catch (KeyNotFoundException)
            {
                missCount++;
                return -1;
            }
        }
        public void cacheValue(int[] vars, double value)
        {
            valueCache[vars] = new Pair<double, int>() { Value = value, Count = cacheCounter };
        }
        public void updateCache()
        {
            cacheCounter++;
            if (valueCache.Count >= cleaningPeriod)
            {
                Console.WriteLine("cleaning time,hit=" + hitCount + ", missed=" + missCount);
                var keys = valueCache.Keys;
                List<int[]> removingIndexes = new List<int[]>();
                foreach (var key in keys)
                {
                    var cached = valueCache[key];
                    cached.Count -= cacheCounter;
                    if (cached.Count < -(cacheCounter / 2))
                        removingIndexes.Add(key);
                }
                foreach (var ind in removingIndexes)
                {
                    valueCache.Remove(ind);
                }
                cacheCounter = 0;
            }
        }
    }
}

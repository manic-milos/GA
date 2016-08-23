using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA
{
    class CacheDict
    {
        int level, maxlevel;
        public Dictionary<long, CacheDict> refDict;
        public Dictionary<long, double> valDict;
        public CacheDict(int level,int maxlevel)
        {
            this.level = level;
            this.maxlevel = maxlevel;
            if (level == maxlevel)
            {
                valDict = new Dictionary<long, double>();
            }
            else
            {
                refDict = new Dictionary<long, CacheDict>();
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA
{
    class Settings
    {

        public int initialPopulation = 20;
        public Settings(int initialPopulation=20)
        {
            this.initialPopulation = initialPopulation;
        }
    }
}

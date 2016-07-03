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
        public int randomSeed = 15;
        public Random rand;
        public Settings()
        {
            rand = new Random(randomSeed);
        }
        public Settings(int randomSeed)
        {
            rand = new Random(randomSeed);
        }

        public Settings(int initialPopulation,
                            int randomSeed = 15)
        :this(randomSeed)
        {
            this.initialPopulation = initialPopulation;
            this.randomSeed = randomSeed;
        }
        public int getRandomFromRange(int a, int b)
        {
            return rand.Next(a, b);
        }
        public double getRandomDouble()
        {
            return rand.NextDouble();
        }
    }
}

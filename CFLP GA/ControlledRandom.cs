using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA
{
    class ControlledRandom
    {
        public static int randomSeed = 15;
        public static Random rand = new Random(randomSeed);
        public static int getRandomFromRange(int a, int b)
        {
            return rand.Next(a, b);
        }
        public static double getRandomDouble()
        {
            return rand.NextDouble();
        }
        public static void reset()
        {
            rand = new Random(randomSeed);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA.IteratedLocalSearch.Reports
{
    static class IterationalReport
    {
        public static bool shortReport=true;
        static int iteration=0;
        public static bool on = true;
        public static void Report(string message)
        {
            if (!on)
                return;
            if(!shortReport)
                Console.WriteLine(message);
        }
        public static void IterationEnd(string message)
        {
            if (!on)
                return;
            if (!shortReport)
                Console.WriteLine(message);
            else
            {
                if (iteration > 0)
                {
                    Console.Write("\r\r\r\r\r\r\r\r\r\r\r");
                }
                Console.Write("i:" + iteration);
                iteration++;
            }
        }
        public static void FinalIteration(string message)
        {
            if (!on)
                return;
            if (!shortReport)
                Console.WriteLine(message);
            Console.WriteLine();
            iteration = 0;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA.IteratedLocalSearch.Reports
{
    static class VerboseReport
    {
        public static bool on = false;
        public static void Report(string message)
        {
            if(on)
                Console.WriteLine(message);
        }
    }
}

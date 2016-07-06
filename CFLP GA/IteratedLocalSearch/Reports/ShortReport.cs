using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA.IteratedLocalSearch.Reports
{
    static class ShortReport
    {
        static StreamWriter writer;
        public static void Init(StreamWriter writer)
        {
            ShortReport.writer = writer;
        }
        public static void Report(string message)
        {
            Console.WriteLine(message);
            writer.WriteLine(message);
        }
    }
}

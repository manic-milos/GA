using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA
{
    class Program
    {
        public static GeneticAlgorithm ga;
        static void Main(string[] args)
        {
            TestOnData test = new TestOnData();
            test.testSelectOnFolder(@"D:\reinstalacija\F\Projects\ConsoleApplication8\ConsoleApplication8\FormatedInstances",
                true,true,true);
            Console.Read();
        }
    }
}

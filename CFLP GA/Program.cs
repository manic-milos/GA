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
        static void Main(string[] args)
        {
            int starting_iteration = 0;
            Random r = new Random(2);
            for (int i = 0; i < starting_iteration; i++)
            {
                ControlledRandom.reset(r.Next());
                ControlledRandom.reset();
            }
            for (int i = starting_iteration; i < 30; i++)
            {
                Execution_Reports.ReportController.reportPrefix = i + "_";
                ControlledRandom.reset(r.Next());
                ControlledRandom.reset();
                TestOnData test = new TestOnData();
                test.testSelectOnFolder(@"D:\reinstalacija\F\Projects\ConsoleApplication8\ConsoleApplication8\FormatedInstances",
                    true, true, true, true);
                Execution_Reports.ReportController.RestartReports();
            }
            Console.Read();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA.Execution_Reports
{
    class ConsoleReport:ReportBase
    {

        public override void report(string message)
        {
            Console.WriteLine(message);
        }

        public override void Dispose()
        {
        }
    }
}

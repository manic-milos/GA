using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA.Execution_Reports
{
    class ConsoleReport:ReportBase
    {
        public static Dictionary<int, ConsoleColor> colors = new Dictionary<int, ConsoleColor>();
        public override void report(string message,int level)
        {
            ConsoleColor c;
            if(colors.TryGetValue(level,out c))
            {
                Console.ForegroundColor = c;
            }
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public override void Dispose()
        {
        }
    }
}

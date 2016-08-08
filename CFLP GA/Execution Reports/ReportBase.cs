using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA.Execution_Reports
{
    abstract class ReportBase
    {
        public abstract void report(string message,int level);
        public abstract void Dispose();
    }
}

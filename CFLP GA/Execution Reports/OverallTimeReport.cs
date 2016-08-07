using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA.Execution_Reports
{
    class OverallTimeReport:TimeReport
    {
        public OverallTimeReport()
            :base()
        {
        }
        public void addTime(TimeSpan time)
        {
            base.addTime(time, "overall");
        }
        public void Broadcast()
        {
            base.Broadcast("overall");
        }
    }
}

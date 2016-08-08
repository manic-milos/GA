using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA.Execution_Reports
{
    class TimeReport
    {
        Dictionary<string, Stopwatch> times = new Dictionary<string, Stopwatch>();
        public TimeReport()
        {

        }
        public void startMeasuring(string name)
        {
            Stopwatch tmp = new Stopwatch();
            times[name] = tmp;
            tmp.Start();
        }
        public TimeSpan stopMeasuring(string name)
        {
            Stopwatch tmp = times[name];
            tmp.Stop();
            return tmp.Elapsed;
        }
        public void continueMeasuring(string name)
        {
            times[name].Start();
        }
        public TimeSpan getTime(string name)
        {
            return times[name].Elapsed;
        }
    }
}

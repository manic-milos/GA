using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA.Execution_Reports
{
    class TimeReport : ResultReport
    {
        Dictionary<string, TimeSpan> times = new Dictionary<string, TimeSpan>();
        public TimeReport()
            : base()
        {

        }
        public override void Broadcast(string name)
        {
            TimeSpan timespan;
            if (times.TryGetValue(name, out timespan))
            {
                base.Broadcast(name + ":" + timespan.ToString());
            }
            else
            {
                base.Broadcast(name + " not meassured");
            }
        }
        public virtual void addTime(TimeSpan time, string name)
        {
            TimeSpan timespan;
            if (times.TryGetValue(name, out timespan))
            {
                times[name] = timespan + time;
            }
            else
            {
                times[name] = new TimeSpan(0)+time;
            }
        }
        public virtual void resetTime(string name)
        {
            times[name] = new TimeSpan(0);
        }
        public static Stopwatch startMeasuring()
        {
            Stopwatch s=new Stopwatch();
            s.Start();
            return s;
        }
        public static TimeSpan stopMeasuring(Stopwatch time)
        {
            time.Stop();
            return time.Elapsed;
        }
    }
}

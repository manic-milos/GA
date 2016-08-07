using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA.Execution_Reports
{
    static class ReportController
    {
        public enum LEVELS
        {
            SHORT_RESULT, VERBOSE_RESULT, ITER_SHORT_RESULT, ITER_VERB_RESULT,
            SHORT_TIME, VERBOSE_TIME, ITER_SHORT_TIME, ITER_VERB_TIME
        };
        static Dictionary<int, List<ReportBase>> chain = new Dictionary<int, List<ReportBase>>();
        public static void AddReport(int level, ReportBase newReport)
        {
            List<ReportBase> reportList;
            if (chain.TryGetValue(level, out reportList))
            {
                reportList.Add(newReport);
            }
            else
            {
                chain[level] = new List<ReportBase>() { newReport };
            }
        }
        public static void Broadcast(int level, string message)
        {
            foreach (int i in chain.Keys)
            {
                if (i >= level)
                {
                    foreach (ReportBase report in chain[i])
                    {
                        report.report(message);
                    }
                }
            }
        }
        public static void HelperSetup()
        {
            ResultReport shortResultReport = new ResultReport();
            shortResultReport.AddWriter(new StreamWriter("short_results.txt"));
            AddReport(1, shortResultReport);

            ResultReport verboseResultReport = new ResultReport();
            verboseResultReport.AddWriter(new StreamWriter("verb_results.txt"));
            AddReport(2, verboseResultReport);


        }
        public static void RestartReports()
        {
            chain = new Dictionary<int, List<ReportBase>>();
        }
        public static void Dispose()
        {
            foreach (List<ReportBase> reports in chain.Values)
            {
                foreach (ReportBase report in reports)
                    report.Dispose();
            }
        }
    }
}

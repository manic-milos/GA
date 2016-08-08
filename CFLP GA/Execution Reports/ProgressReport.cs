using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA.Execution_Reports
{
    class ProgressReport:ResultReport
    {
        string last_iteration = "";
        public void startCounting()
        {
            last_iteration = "";
        }
        public void addCount(string iterationInfo)
        {
            clean();
            Console.Write(iterationInfo);
            last_iteration = iterationInfo;
        }
        public void endCount()
        {
            clean();
        }
        private void clean()
        {
            Console.Write(new string('\r', last_iteration.Length) + new string(' ', last_iteration.Length)+
                new string('\r', last_iteration.Length));
        }
        
    }
}

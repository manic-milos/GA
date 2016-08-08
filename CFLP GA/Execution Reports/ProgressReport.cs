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
        public int iteration;
        public void startCounting()
        {
            iteration = 0;
            Console.Write(iteration.ToString());
        }
        public int addCount()
        {
            iteration++;
            Console.Write("\r"+"iteration: "+iteration.ToString());
            return iteration;
        }
        public int endCount()
        {
            Console.Write("\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r");
            Console.Write("                                                                          ");
            Console.Write("\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r\r");
            return ++iteration;
        }
        
    }
}

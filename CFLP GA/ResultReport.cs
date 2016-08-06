using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA
{
    class ResultReport
    {
        List<StreamWriter> writers;
        public ResultReport()
        {
            writers = new List<StreamWriter>();
        }
        public ResultReport AddWriter(StreamWriter writer)
        {
            writers.Add(writer);
            return this;
        }
        public void Broadcast(string message)
        {
            foreach(StreamWriter writer in writers)
            {
                writer.WriteLine(message);
            }
        }
        public void DisposeWriters()
        {
            foreach(StreamWriter writer in writers)
            {
                writer.Dispose();
            }
        }
    }
}

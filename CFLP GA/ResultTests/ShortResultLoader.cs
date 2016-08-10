using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA.ResultTests
{
    class ShortResultLoader
    {
        public List<Result> results=new List<Result>();
        public void load(string path)
        {
            StreamReader reader = new StreamReader(path);
            string line = reader.ReadLine();
            while(!reader.EndOfStream)
            {

                Result newresult = new Result();
                results.Add(newresult);
                string name = line;
                line = reader.ReadLine();
                newresult.name = name;
                newresult.GAresult = double.Parse(line);
                if (reader.EndOfStream)
                    break;
                line = reader.ReadLine();
                if(!double.TryParse(line,out newresult.ILSresult))
                {
                    continue;
                }
                if (reader.EndOfStream)
                    break;
                line = reader.ReadLine();
                if (!double.TryParse(line, out newresult.GAAresult))
                {
                    continue;
                }
                if (reader.EndOfStream)
                    break;
                line = reader.ReadLine();
                if (!double.TryParse(line, out newresult.MeMresult))
                {
                    continue;
                }
            }
            reader.Dispose();
        }
    }
}

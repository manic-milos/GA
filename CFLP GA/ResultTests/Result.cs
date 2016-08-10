using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA.ResultTests
{
    class Result
    {
        public string name;
        public double GAresult;
        public double ILSresult;
        public double GAAresult;
        public double MeMresult;

        public override string ToString()
        {
            return name + Environment.NewLine +
                GAresult + Environment.NewLine +
                ILSresult + Environment.NewLine +
                GAAresult + Environment.NewLine +
                MeMresult;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA
{
    class UnfeasableProblemException : Exception
    {
        public UnfeasableProblemException()
        {
        }

        public UnfeasableProblemException(string message)
            : base("Unfeasable problem:"+message)
        {
        }

        public UnfeasableProblemException(string message, Exception inner)
            : base("Unfeasable problem:" + message, inner)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA.Mutation.Mutators.Tests
{
    class MutatorTests
    {
        List<MutatorTestBase> tests = new List<MutatorTestBase>();
        public void addTest(MutatorTestBase test)
        {
            tests.Add(test);
        }
        public bool Test()
        {
            foreach(MutatorTestBase m in tests)
            {
                if(!m.Test())
                {
                    return false;
                }
            }
            return true;
        }
    }
}

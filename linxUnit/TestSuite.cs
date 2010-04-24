using System;
using System.Collections.Generic;
using System.Text;

namespace linxUnit
{
    public class TestSuite : ITest
    {
        private List<ITest> tests;
        public ICollection<ITest> Tests { get { return tests; } }

        public TestSuite()
        {
            tests = new List<ITest>();
        }

        public void Add(ITest test)
        {
            tests.Add(test);
        }

        public void Run(TestResult result)
        {
            foreach (ITest test in tests)
            {
                test.Run(result);
            }
        }
    }

}

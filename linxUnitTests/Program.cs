using System;
using System.Collections.Generic;
using System.Text;
using linxUnit;

namespace linxUnitTests
{
    class Program
    {
        static void Main(string[] args)
        {
            RunHelper.StartTimedTestRun(suite =>
            {
                suite.add(TestCase.CreateSuite(typeof(TestCaseTest)));
                suite.add(TestCase.CreateSuite(typeof(AssertTest)));
                suite.add(TestCase.CreateSuite(typeof(TestSuiteTests)));
                suite.add(TestCase.CreateSuite(typeof(TestLoaderTest)));
            });
        }
    }
}

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
                suite.Add(TestCase.CreateSuite(typeof(TestCaseTest)));
                suite.Add(TestCase.CreateSuite(typeof(AssertTest)));
                suite.Add(TestCase.CreateSuite(typeof(TestSuiteTests)));
                suite.Add(TestCase.CreateSuite(typeof(TestLoaderTest)));
                suite.Add(TestCase.CreateSuite(typeof(TestResultTest)));
            });
        }
    }
}

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
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();

            stopwatch.Start();

            TestSuite suite = new TestSuite();
            suite.add(TestCase.CreateSuite(typeof(TestCaseTest)));
            suite.add(TestCase.CreateSuite(typeof(AssertTest)));
            suite.add(TestCase.CreateSuite(typeof(TestSuiteTests)));
            suite.add(TestCase.CreateSuite(typeof(TestLoaderTest)));
            TestResult result = new TestResult();
            suite.run(result);
            Console.WriteLine(result.summary());

            stopwatch.Stop();

            Console.WriteLine("Time elapsed: " + stopwatch.Elapsed);
        }
    }
}
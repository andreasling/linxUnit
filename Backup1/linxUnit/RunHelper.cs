using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace linxUnit
{
    public static class RunHelper
    {
        public static void StartTimedTestRun(Action<TestSuite> addTests)
        {
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();

            stopwatch.Start();

            TestSuite suite = new TestSuite();

            addTests(suite);

            TestResult result = new TestResult();
            suite.Run(result);

            stopwatch.Stop();

            ResultsHelper.DisplayResults(result);

            Console.WriteLine("Time elapsed: " + stopwatch.Elapsed);
        }
    }
}

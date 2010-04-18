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

            stopwatch.Stop();

            foreach (var detail in result.details)
            {
                var oldColor = Console.ForegroundColor;

                var color = 
                    detail.inconclusive ? 
                        ConsoleColor.DarkYellow :
                    detail.success ? 
                        ConsoleColor.DarkGreen :
                        ConsoleColor.DarkRed;

                Console.ForegroundColor = color;

                Console.Write(detail.message);
                if (!detail.success)
                {
                    Console.Write(": " + detail.failure.exception.Message);
                }
                Console.WriteLine();

                Console.ForegroundColor = oldColor;
            }
            
            Console.WriteLine();

            Console.WriteLine(result.summary());

            Console.WriteLine("Time elapsed: " + stopwatch.Elapsed);

        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using linxUnit;

namespace MyCalculatorTests
{
    class Program
    {
        static void Main(string[] args)
        {
            TestSuite suite = TestCase.CreateSuite(typeof(MyCalculatorTests));
            TestResult result = new TestResult();
            suite.run(result);
            Console.WriteLine(result.summary());
        }
    }
}

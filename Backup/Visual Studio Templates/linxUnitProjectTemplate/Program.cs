using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using linxUnit;

namespace linxUnitProjectTemplate
{
    class Program
    {
        static void Main(string[] args)
        {
            TestSuite suite = new TestSuite();

            suite.add(TestCase.CreateSuite(typeof(TestTemplate)));
            
            TestResult result = new TestResult();
            
            suite.run(result);
            
            Console.WriteLine(result.summary());
        }
    }
}

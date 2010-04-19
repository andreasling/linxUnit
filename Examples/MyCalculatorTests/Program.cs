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
            RunHelper.StartTimedTestRun(suite =>
            {
                suite.add(new TestLoader().LoadFromDirectory(System.IO.Path.GetFullPath(".")));
            });
        }
    }
}

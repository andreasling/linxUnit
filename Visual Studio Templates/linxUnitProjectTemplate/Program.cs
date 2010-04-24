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
            RunHelper.StartTimedTestRun(suite =>
            {
                suite.Add(new TestLoader().LoadFromDirectory("."));
            });
        }
    }
}

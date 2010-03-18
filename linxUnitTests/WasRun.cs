using System;
using System.Collections.Generic;
using System.Text;
using linxUnit;

namespace linxUnitTests
{
    public class WasRun : TestCase
    {
        public string log;

        public WasRun(string name)
            : base(name)
        {
        }

        public override void setUp()
        {
            log = "setUp ";
        }

        public override void tearDown()
        {
            log += "tearDown ";
        }

        public void testMethod()
        {
            log += "testMethod ";
        }

        public void testBrokenMethod()
        {
            throw new Exception();
        }
    }

}

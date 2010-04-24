using System;
using System.Collections.Generic;
using System.Text;
using linxUnit;

namespace linxUnitTestTests
{
    public class WasRun : TestCase
    {
        public string log;

        public WasRun(string name)
            : base(name)
        {
        }

        public override void SetUp()
        {
            log = "setUp ";
        }

        public override void TearDown()
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

        private void notATestMethod()
        {
        }
    }

}

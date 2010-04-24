using System;
using System.Collections.Generic;
using System.Text;
using linxUnit;

namespace linxUnitTestTests
{
    public class ResultDetailsTest : TestCase
    {
        public string log;

        public ResultDetailsTest(string name)
            : base(name)
        {
        }

        public void testMethodSuccessful()
        {
        }

        public void testMethodFailed()
        {
            Assert.Fail();
        }

        public void testMethodInconclusive()
        {
            Assert.Inconclusive();
        }
    }

}

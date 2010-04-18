using System;
using System.Collections.Generic;
using System.Text;
using linxUnit;

namespace linxUnitTests
{
    public class ResultDetailsTestTest : TestCase
    {
        public string log;

        public ResultDetailsTestTest(string name)
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

using System;
using System.Collections.Generic;
using System.Text;
using linxUnit;

namespace linxUnitTests
{
    public class SetUpTearDownFailedTest : TestCase
    {
        public string log;

        public SetUpTearDownFailedTest(string name)
            : base(name)
        {
        }

        public override void SetUp()
        {
            log += "setUp ";
            throw new Exception("SetUpTearDownFailedTest.setUp");
        }

        public override void TearDown()
        {
            log += "tearDown ";
            //throw new Exception("SetUpTearDownFailedTest.tearDown");
        }

        public void testMethod()
        {
            log += "testMethod ";
        }
    }

}

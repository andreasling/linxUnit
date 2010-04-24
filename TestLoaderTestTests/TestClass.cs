using System;
using System.Collections.Generic;
using System.Text;
using linxUnit;

namespace linxUnitTests
{
    public class TestClass : TestCase
    {
        public void TestMethod()
        {
            Assert.IsTrue(true);
        }

        private void NotATestMethod()
        {
        }
    }
}

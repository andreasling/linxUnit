using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using linxUnit;

namespace linxUnitTestTests
{
    public class ExpectedExceptionTestTest : TestCase
    {
        public void TestMethodFailure()
        {
            ExpectException<InvalidOperationException>();

            InvalidOperation();
        }

        private void InvalidOperation()
        {
            throw new InvalidOperationException();
        }
    }
}

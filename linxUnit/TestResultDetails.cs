using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace linxUnit
{
    public class TestResultDetails
    {
        public bool Success { get { return !Inconclusive && isSuccess(); } }
        public bool Inconclusive { get; set; }
        public TestFailure failure { get; set; }

        private bool isSuccess()
        {
            return failure == null;
        }

        public string Message { get; set; }

        public string Name { get; set; }
    }

    public class TestFailure
    {
        public Exception Exception { get; internal set; }
    }
}

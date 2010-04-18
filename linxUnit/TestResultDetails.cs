using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace linxUnit
{
    public class TestResultDetails
    {
        public bool success { get { return !inconclusive && isSuccess(); } }
        public bool inconclusive { get; set; }
        public TestFailure failure { get; set; }

        private bool isSuccess()
        {
            return failure == null;
        }

        public string message { get; set; }

        public string name { get; set; }
    }

    public class TestFailure
    {
        //public string message { get; internal set; }
        public Exception exception { get; internal set; }
    }
}

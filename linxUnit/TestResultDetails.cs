using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace linxUnit
{
    public class TestResultDetails
    {
        public bool Success { get { return !Inconclusive && isSuccess(); } }
        public bool Inconclusive { get; internal set; }
        public Exception Exception { get; internal set; }

        private bool isSuccess()
        {
            return Exception == null;
        }

        public string Message { get; internal set; }

        public string Name { get; internal set; }
    }
}

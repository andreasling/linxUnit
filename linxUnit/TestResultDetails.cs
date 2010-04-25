using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace linxUnit
{
    public class TestResultDetails
    {
        public string Name { get; internal set; }
        public TestResultState State { get; internal set; }
        public bool Success { get { return State == TestResultState.Success; } }
        public bool Inconclusive { get { return State == TestResultState.Inconclusive; } }
        public string Message { get; internal set; }
        public Exception Exception { get; internal set; }
    }

    public enum TestResultState
    {
        Inconclusive,
        Success,
        Failure
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace linxUnit
{
    public class TestResult
    {
        private int runCount = 0;
        private int errorCount = 0;
        public IList<TestFailure> failures { get; private set; }

        public TestResult()
        {
            failures = new List<TestFailure>();
        }

        public void testStarted()
        {
            runCount++;
        }

        public void testFailed(string name, Exception exception)
        {
            errorCount++;
            failures.Add(new TestFailure() { message = string.Format("{0} failed", name), exception = exception });
        }

        public string summary()
        {
            return string.Format("{0} run, {1} failed", runCount, errorCount);
        }

    }

    public class TestFailure
    {
        public string message { get; internal set; }
        public Exception exception { get; internal set; }
    }

}

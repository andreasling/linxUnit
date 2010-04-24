using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace linxUnit
{
    public class TestResult
    {
        public IList<TestResultDetails> Details { get { return details; } }
        private List<TestResultDetails> details;

        public TestResult()
        {
            details = new List<TestResultDetails>();
        }

        public void TestStarted(string name)
        {
            var detail = new TestResultDetails() 
            { 
                Inconclusive = true, 
                Name = name,
                Message = FormatDetailMessage(name, "inconclusive")
            };

            details.Add(detail);
        }

        public void TestFailed(Exception exception)
        {
            var detail = details.Last();

            detail.Inconclusive = false;
            detail.Message = FormatDetailMessage(detail.Name, "failed");
            detail.failure = new TestFailure() { Exception = exception };
        }

        public void TestSucceeded()
        {
            var detail = details.Last();

            detail.Inconclusive = false;
            detail.Message = FormatDetailMessage(detail.Name, "succeeded");
        }

        public string Summary()
        {
            return string.Format("{0} run, {1} failed", runCount(), errorCount());
        }

        private int errorCount()
        {
            return Details.Count(d => d.failure != null);
        }

        private int runCount()
        {
            return Details.Count;
        }

        private static string FormatDetailMessage(string name, string messageText)
        {
            return string.Format("{0} {1}", name, messageText);
        }
    }
}

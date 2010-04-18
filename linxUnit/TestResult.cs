using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace linxUnit
{
    public class TestResult
    {
        public IList<TestResultDetails> details { get { return detailList; } }
        private List<TestResultDetails> detailList;

        public TestResult()
        {
            detailList = new List<TestResultDetails>();
        }

        public void testStarted(string name)
        {
            var detail = new TestResultDetails() 
            { 
                inconclusive = true, 
                name = name,
                message = FormatDetailMessage(name, "inconclusive")
            };

            detailList.Add(detail);
        }

        public void testFailed(Exception exception)
        {
            var detail = detailList.Last();

            detail.inconclusive = false;
            detail.message = FormatDetailMessage(detail.name, "failed");
            detail.failure = new TestFailure() { exception = exception };
        }

        public void testSucceeded()
        {
            var detail = detailList.Last();

            detail.inconclusive = false;
            detail.message = FormatDetailMessage(detail.name, "succeeded");
        }

        public string summary()
        {
            return string.Format("{0} run, {1} failed", runCount(), errorCount());
        }

        private int errorCount()
        {
            return details.Count(d => d.failure != null);
        }

        private int runCount()
        {
            return details.Count;
        }

        private static string FormatDetailMessage(string name, string messageText)
        {
            return string.Format("{0} {1}", name, messageText);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace linxUnit
{
    public class TestResult
    {
        private int runCount = 0;
        private int errorCount = 0;

        public void testStarted()
        {
            runCount++;
        }

        public void testFailed()
        {
            errorCount++;
        }

        public string summary()
        {
            return string.Format("{0} run, {1} failed", runCount, errorCount);
        }
    }

}

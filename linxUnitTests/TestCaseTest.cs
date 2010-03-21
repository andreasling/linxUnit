using System;
using System.Collections.Generic;
using System.Text;
using linxUnit;

namespace linxUnitTests
{
    class TestCaseTest : TestCase
    {
        private TestResult result;

        public TestCaseTest(string name)
            : base(name)
        {
        }

        public override void setUp()
        {
            result = new TestResult();
        }

        public void testTemplateMethod()
        {
            WasRun test = new WasRun("testMethod");
            test.run(result);
            Assert.AreEqual("setUp testMethod tearDown ", test.log);
        }

        public void testResult()
        {
            WasRun test = new WasRun("testMethod");
            test.run(result);
            Assert.AreEqual("1 run, 0 failed", result.summary());
        }

        public void testFailedResult()
        {
            WasRun test = new WasRun("testBrokenMethod");
            test.run(result);
            Assert.AreEqual("1 run, 1 failed", result.summary());
        }

        public void testFailedResultFormatting()
        {
            result.testStarted();
            result.testFailed("test", null);
            Assert.AreEqual("1 run, 1 failed", result.summary());
        }

        public void testSuite()
        {
            TestSuite suite = new TestSuite();
            suite.add(new WasRun("testMethod"));
            suite.add(new WasRun("testBrokenMethod"));
            suite.run(result);
            Assert.AreEqual("2 run, 1 failed", result.summary());
        }

        public void testCreateSuiteFromCase()
        {
            TestSuite suite = TestCase.CreateSuite(typeof(WasRun));
            suite.run(result);
            Assert.AreEqual("2 run, 1 failed", result.summary());
        }

        public void testSetUpTearDownFailed()
        {
            SetUpTearDownFailedTest test = new SetUpTearDownFailedTest("testMethod");
            test.run(result);
            Assert.AreEqual("setUp tearDown ", test.log);
        }

        public void testResultDetails()
        {
            TestSuite suite = TestCase.CreateSuite(typeof(WasRun));
            
            suite.run(result);

            Assert.AreEqual(1, result.failures.Count);
            Assert.AreEqual("testBrokenMethod failed", result.failures[0].message);
            Assert.AreEqual("Exception of type 'System.Exception' was thrown.", result.failures[0].exception.Message);
        }
    }
}

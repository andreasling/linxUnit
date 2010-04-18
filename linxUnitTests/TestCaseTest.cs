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

        public void testResultSummary()
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
            result.testStarted("testMethod");
            result.testFailed(new Exception());
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

        public void testResultDetailsSuccessful()
        {
            var result = new TestResult();

            result.testStarted("testMethod");
            result.testSucceeded();

            Assert.AreEqual(1, result.details.Count);

            Assert.IsFalse(result.details[0].inconclusive);
            Assert.IsTrue(result.details[0].success);
            Assert.AreEqual("testMethod", result.details[0].name);
            Assert.AreEqual("testMethod succeeded", result.details[0].message);
            Assert.IsNull(result.details[0].failure);
        }

        public void testResultDetailsInconclusive()
        {
            var result = new TestResult();

            result.testStarted("testMethod");

            Assert.AreEqual(1, result.details.Count);

            Assert.IsTrue(result.details[0].inconclusive);
            Assert.IsFalse(result.details[0].success);
            Assert.AreEqual("testMethod", result.details[0].name);
            Assert.AreEqual("testMethod inconclusive", result.details[0].message);
            Assert.IsNull(result.details[0].failure);
        }

        public void testResultDetailsFailure()
        {
            var result = new TestResult();

            result.testStarted("testMethod");
            result.testFailed(new Exception());

            Assert.AreEqual(1, result.details.Count);

            Assert.IsFalse(result.details[0].inconclusive);
            Assert.IsFalse(result.details[0].success);
            Assert.AreEqual("testMethod", result.details[0].name);
            Assert.AreEqual("testMethod failed", result.details[0].message);
            Assert.AreEqual("Exception of type 'System.Exception' was thrown.", result.details[0].failure.exception.Message);
        }

        public void testResultDetailsTwoResults()
        {
            var result = new TestResult();

            result.testStarted("testMethod");
            result.testSucceeded();

            result.testStarted("testBrokenMethod");
            result.testFailed(new Exception());

            Assert.AreEqual(2, result.details.Count);

            Assert.IsFalse(result.details[0].inconclusive);
            Assert.IsTrue(result.details[0].success);
            Assert.AreEqual("testMethod", result.details[0].name);
            Assert.IsNull(result.details[0].failure);

            Assert.IsFalse(result.details[1].inconclusive);
            Assert.IsFalse(result.details[1].success);
            Assert.AreEqual("testBrokenMethod", result.details[1].name);
            Assert.AreEqual("testBrokenMethod failed", result.details[1].message);
            Assert.AreEqual("Exception of type 'System.Exception' was thrown.", result.details[1].failure.exception.Message);
        }

        public void testResultDetailsAfterRun()
        {
            TestSuite suite = TestCase.CreateSuite(typeof(WasRun));

            suite.run(result);

            Assert.AreEqual(2, result.details.Count);

            Assert.IsFalse(result.details[0].inconclusive);
            Assert.IsTrue(result.details[0].success);
            Assert.AreEqual("testMethod", result.details[0].name);
            Assert.AreEqual("testMethod succeeded", result.details[0].message);
            Assert.IsNull(result.details[0].failure);

            Assert.IsFalse(result.details[1].inconclusive);
            Assert.IsFalse(result.details[1].success);
            Assert.AreEqual("testBrokenMethod", result.details[1].name);
            Assert.AreEqual("testBrokenMethod failed", result.details[1].message);
            Assert.AreEqual("Exception of type 'System.Exception' was thrown.", result.details[1].failure.exception.Message);
        }
    }
}

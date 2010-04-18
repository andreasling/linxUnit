using linxUnit;
using System;
using System.Collections.Generic;
using System.Text;

namespace linxUnitTests
{
    public class TestResultTest : TestCase
    {
        public TestResultTest(string name)
            : base(name)
        {
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
            Assert.IsNotNull(result.details[1].failure);
        }

        public void testResultDetailsAfterRun()
        {
            var result = new TestResult();

            TestSuite suite = TestCase.CreateSuite(typeof(ResultDetailsTestTest));

            suite.run(result);

            Assert.AreEqual(3, result.details.Count);

            Assert.IsFalse(result.details[0].inconclusive);
            Assert.IsTrue(result.details[0].success);
            Assert.AreEqual("testMethodSuccessful", result.details[0].name);
            Assert.AreEqual("testMethodSuccessful succeeded", result.details[0].message);
            Assert.IsNull(result.details[0].failure);

            Assert.IsFalse(result.details[1].inconclusive);
            Assert.IsFalse(result.details[1].success);
            Assert.AreEqual("testMethodFailed", result.details[1].name);
            Assert.AreEqual("testMethodFailed failed", result.details[1].message);
            Assert.AreEqual("Test failed.", result.details[1].failure.exception.Message);

            Assert.IsTrue(result.details[2].inconclusive);
            Assert.IsFalse(result.details[2].success);
            Assert.AreEqual("testMethodInconclusive", result.details[2].name);
            Assert.AreEqual("testMethodInconclusive inconclusive", result.details[2].message);
            //Assert.AreEqual("Test was inconclusive.", result.details[2].failure.exception.Message);
        }
    }
}

using linxUnit;
using System;
using System.Collections.Generic;
using System.Text;
using linxUnitTestTests;

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

            result.TestStarted("testMethod");
            result.TestSucceeded();

            Assert.AreEqual(1, result.Details.Count);

            Assert.IsFalse(result.Details[0].Inconclusive);
            Assert.IsTrue(result.Details[0].Success);
            Assert.AreEqual("testMethod", result.Details[0].Name);
            Assert.AreEqual("testMethod succeeded", result.Details[0].Message);
            Assert.IsNull(result.Details[0].failure);
        }

        public void testResultDetailsInconclusive()
        {
            var result = new TestResult();

            result.TestStarted("testMethod");

            Assert.AreEqual(1, result.Details.Count);

            Assert.IsTrue(result.Details[0].Inconclusive);
            Assert.IsFalse(result.Details[0].Success);
            Assert.AreEqual("testMethod", result.Details[0].Name);
            Assert.AreEqual("testMethod inconclusive", result.Details[0].Message);
            Assert.IsNull(result.Details[0].failure);
        }

        public void testResultDetailsFailure()
        {
            var result = new TestResult();

            result.TestStarted("testMethod");
            result.TestFailed(new Exception());

            Assert.AreEqual(1, result.Details.Count);

            Assert.IsFalse(result.Details[0].Inconclusive);
            Assert.IsFalse(result.Details[0].Success);
            Assert.AreEqual("testMethod", result.Details[0].Name);
            Assert.AreEqual("testMethod failed", result.Details[0].Message);
            Assert.AreEqual("Exception of type 'System.Exception' was thrown.", result.Details[0].failure.Exception.Message);
        }

        public void testResultDetailsTwoResults()
        {
            var result = new TestResult();

            result.TestStarted("testMethod");
            result.TestSucceeded();

            result.TestStarted("testBrokenMethod");
            result.TestFailed(new Exception());

            Assert.AreEqual(2, result.Details.Count);

            Assert.IsFalse(result.Details[0].Inconclusive);
            Assert.IsTrue(result.Details[0].Success);
            Assert.AreEqual("testMethod", result.Details[0].Name);
            Assert.IsNull(result.Details[0].failure);

            Assert.IsFalse(result.Details[1].Inconclusive);
            Assert.IsFalse(result.Details[1].Success);
            Assert.AreEqual("testBrokenMethod", result.Details[1].Name);
            Assert.IsNotNull(result.Details[1].failure);
        }

        public void testResultDetailsAfterRun()
        {
            var result = new TestResult();

            TestSuite suite = TestCase.CreateSuite(typeof(ResultDetailsTest));

            suite.Run(result);

            Assert.AreEqual(3, result.Details.Count);

            Assert.IsFalse(result.Details[0].Inconclusive);
            Assert.IsTrue(result.Details[0].Success);
            Assert.AreEqual("testMethodSuccessful", result.Details[0].Name);
            Assert.AreEqual("testMethodSuccessful succeeded", result.Details[0].Message);
            Assert.IsNull(result.Details[0].failure);

            Assert.IsFalse(result.Details[1].Inconclusive);
            Assert.IsFalse(result.Details[1].Success);
            Assert.AreEqual("testMethodFailed", result.Details[1].Name);
            Assert.AreEqual("testMethodFailed failed", result.Details[1].Message);
            Assert.AreEqual("Test failed.", result.Details[1].failure.Exception.Message);

            Assert.IsTrue(result.Details[2].Inconclusive);
            Assert.IsFalse(result.Details[2].Success);
            Assert.AreEqual("testMethodInconclusive", result.Details[2].Name);
            Assert.AreEqual("testMethodInconclusive inconclusive", result.Details[2].Message);
            //Assert.AreEqual("Test was inconclusive.", result.details[2].failure.exception.Message);
        }
    }
}

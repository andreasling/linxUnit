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

        public override void SetUp()
        {
            result = new TestResult();
        }

        public void testTemplateMethod()
        {
            WasRun test = new WasRun("testMethod");
            test.Run(result);
            Assert.AreEqual("setUp testMethod tearDown ", test.log);
        }

        public void testResultSummary()
        {
            WasRun test = new WasRun("testMethod");
            test.Run(result);
            Assert.AreEqual("1 run, 0 failed", result.Summary());
        }

        public void testFailedResult()
        {
            WasRun test = new WasRun("testBrokenMethod");
            test.Run(result);
            Assert.AreEqual("1 run, 1 failed", result.Summary());
        }

        public void testFailedResultFormatting()
        {
            result.TestStarted("testMethod");
            result.TestFailed(new Exception());
            Assert.AreEqual("1 run, 1 failed", result.Summary());
        }

        public void testSuite()
        {
            TestSuite suite = new TestSuite();
            suite.Add(new WasRun("testMethod"));
            suite.Add(new WasRun("testBrokenMethod"));
            suite.Run(result);
            Assert.AreEqual("2 run, 1 failed", result.Summary());
        }

        public void testCreateSuiteFromCase()
        {
            TestSuite suite = TestCase.CreateSuite(typeof(WasRun));
            suite.Run(result);
            Assert.AreEqual("2 run, 1 failed", result.Summary());
        }

        public void testCreateSuiteFromCaseGeneric()
        {
            TestSuite suite = TestCase.CreateSuite<WasRun>();
            suite.Run(result);
            Assert.AreEqual("2 run, 1 failed", result.Summary());
        }

        public void testCreateSuiteWrongType()
        {
            try
            {
                TestSuite suite = TestCase.CreateSuite(typeof(object));

                Assert.Fail();
            }
            catch (ArgumentException actual)
            {
                Assert.IsNotNull(actual.Message);
            }
        }

        public void testSetUpTearDownFailed()
        {
            SetUpTearDownFailedTest test = new SetUpTearDownFailedTest("testMethod");
            test.Run(result);
            Assert.AreEqual("setUp tearDown ", test.log);
        }

        public void testDefaultContructor()
        {
            OnlyDefaultContructorTest test = new OnlyDefaultContructorTest() { Name = "testMethod" };

            test.Run(result);

            Assert.IsTrue(result.Details[0].Success);
        }

        public void testDefaultContructorSuite()
        {
            var suite = TestCase.CreateSuite<OnlyDefaultContructorTest>();

            suite.Run(result);

            Assert.IsTrue(result.Details[0].Success);
        }
    }
}

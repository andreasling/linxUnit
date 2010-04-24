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

        public void testCreateSuiteFromCaseGeneric()
        {
            TestSuite suite = TestCase.CreateSuite<WasRun>();
            suite.run(result);
            Assert.AreEqual("2 run, 1 failed", result.summary());
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
            test.run(result);
            Assert.AreEqual("setUp tearDown ", test.log);
        }

        public void testDefaultContructor()
        {
            OnlyDefaultContructorTest test = new OnlyDefaultContructorTest() { name = "testMethod" };

            test.run(result);

            Assert.IsTrue(result.details[0].success);
        }

        public void testDefaultContructorSuite()
        {
            var suite = TestCase.CreateSuite<OnlyDefaultContructorTest>();

            suite.run(result);

            Assert.IsTrue(result.details[0].success);
        }
    }
}

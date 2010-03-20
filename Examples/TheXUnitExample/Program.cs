using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

/* todo-list:
 * ----------------------------------------------------------------------------
 * done: Invoke test method
 * done: Invoke setUp first
 * done: Invoke tearDown afterward
 * todo: Invoke tearDown even if the test method fails
 * done: Run multiple tests
 * done: Report collected results
 * done: Log string in WasRun
 * done: Report failed tests
 * todo: Catch and report setUp errors
 * todo: Create TestSuite from a TestCase class
 * ----------------------------------------------------------------------------
 * done: Create Assert.IsTrue
 * done: Create Assert.AreEqualS
 */

namespace TheXUnitExample
{
    interface ITest
    {
        void run(TestResult result);
    }

    abstract class TestCase : ITest
    {
        protected string name;

        public TestCase(string name)
        {
            this.name = name;
        }

        public virtual void setUp() {}
        public virtual void tearDown() {}

        public void run(TestResult result)
        {
            result.testStarted();
            this.setUp();
            Type type = this.GetType();
            MethodInfo methodInfo = type.GetMethod(name, BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public);
            try
            {
                methodInfo.Invoke(this, null);
            }
            catch (Exception)
            {
                result.testFailed();
            }
            finally
            {
                this.tearDown();
            }
        }

        public static TestSuite CreateSuite(Type type)
        {
            TestSuite suite = new TestSuite();
            MethodInfo[] methodInfos = type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public);

            foreach (MethodInfo methodInfo in methodInfos)
            {
                if (methodInfo.Name != "setUp" && methodInfo.Name != "tearDown")
                {
                    suite.add((TestCase)type.GetConstructor(new Type[] { typeof(string) }).Invoke(new object[] { methodInfo.Name }));
                }
            }

            return suite;
        }
    }

    class AssertionErrorException : Exception
    {
        public AssertionErrorException()
            : base()
        {
        }

        public AssertionErrorException(string message)
            : base(message)
        {
        }
    }

    class TestResult
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

    class TestSuite : ITest
    {
        private List<ITest> tests;

        public TestSuite()
        {
            tests = new List<ITest>();
        }

        public void add(ITest test)
        {
            tests.Add(test);
        }

        public void run(TestResult result)
        {
            foreach (ITest test in tests)
            {
                test.run(result);
            }
        }
    }

    class Assert
    {
        public static void IsTrue(bool expression)
        {
            if (!expression)
            {
                throw new AssertionErrorException(string.Format("Expression [{0}] was not true.", expression));
            }
        }

        public static void AreEqual(object expected, object actual)
        {
            if (!object.Equals(expected, actual))
            {
                throw new AssertionErrorException(string.Format("Actual value [{0}] did not equal expected value [{1}].", actual, expected));
            }
        }
    }

    class WasRun : TestCase
    {
        public string log;

        public WasRun(string name)
            : base(name)
        {
        }

        public override void setUp()
        {
            log = "setUp ";
        }

        public override void tearDown()
        {
            log += "tearDown ";
        }

        public void testMethod()
        {
            log += "testMethod ";
        }

        public void testBrokenMethod()
        {
            throw new Exception();
        }
    }

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
            result.testFailed();
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
    }

    class AssertTest : TestCase
    {
        public AssertTest(string name)
            : base(name)
        {
        }

        public void testIsTrueSuccess()
        {
            Assert.IsTrue(true);
        }

        public void testIsTrueFailure()
        {
            try
            {
                Assert.IsTrue(false);
                throw new Exception();
            }
            catch (AssertionErrorException)
            {
            }
        }

        public void testAreEqualSuccess()
        {
            Assert.AreEqual(true, true);
            Assert.AreEqual(0, 0);
            Assert.AreEqual("test", "test");
        }

        public void testAreEqualFailure()
        {
            try
            {
                Assert.AreEqual(true, false);
                throw new Exception();
            }
            catch (AssertionErrorException)
            {
            }

            try
            {
                Assert.AreEqual(0, 1);
                throw new Exception();
            }
            catch (AssertionErrorException)
            {
            }

            try
            {
                Assert.AreEqual("test", "fail");
                throw new Exception();
            }
            catch (AssertionErrorException)
            {
            }
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            TestSuite suite = new TestSuite();
            suite.add(TestCase.CreateSuite(typeof(TestCaseTest)));
            suite.add(TestCase.CreateSuite(typeof(AssertTest)));
            TestResult result = new TestResult();
            suite.run(result);
            Console.WriteLine(result.summary());
        }
    }
}

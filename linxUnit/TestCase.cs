using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;

namespace linxUnit
{
    public abstract class TestCase : ITest
    {
        protected string name;

        public TestCase(string name)
        {
            this.name = name;
        }

        public virtual void setUp() { }
        public virtual void tearDown() { }

        public void run(TestResult result)
        {
            result.testStarted(name);
            try
            {
                this.setUp();

                RunTestMethod();

                result.testSucceeded();
            }
            catch (Exception exception)
            {
                var innerException = exception.InnerException;

                if (!(innerException is AssertInconclusiveException))
                {
                    result.testFailed(innerException);
                }
            }
            finally
            {
                this.tearDown();
            }
        }

        private void RunTestMethod()
        {
            GetTestMethod().Invoke(this, null);
        }

        private static BindingFlags bindingFlags = 
            BindingFlags.Instance | 
            BindingFlags.DeclaredOnly | 
            BindingFlags.Public;

        private MethodInfo GetTestMethod()
        {
            return GetType().GetMethod(name, bindingFlags);
        }

        public static TestSuite CreateSuite(Type type)
        {
            var testCaseMethods = GetTestCaseMethods(type);

            var testCases = CreateTestCases(type, testCaseMethods);

            TestSuite suite = CreateTestSuite(testCases);

            return suite;
        }

        private static TestSuite CreateTestSuite(IEnumerable<TestCase> testCases)
        {
            TestSuite suite = new TestSuite();

            foreach (var testCase in testCases)
            {
                suite.add(testCase);
            }
            return suite;
        }

        private static IEnumerable<TestCase> CreateTestCases(Type type, IEnumerable<MethodInfo> testCaseMethods)
        {
            var testCases =
                testCaseMethods.Select(method => CreateTestCase(type, method));
            return testCases;
        }

        private static IEnumerable<MethodInfo> GetTestCaseMethods(Type type)
        {
            var methods = GetMethods(type);

            var testCaseMethods =
                methods.Where(method => !IsSetUpOrTearDown(method));
            return testCaseMethods;
        }

        private static TestCase CreateTestCase(Type type, MethodInfo method)
        {
            return Activator.CreateInstance(type, method.Name) as TestCase;
        }

        private static MethodInfo[] GetMethods(Type type)
        {
            return type.GetMethods(bindingFlags);
        }

        private static bool IsSetUpOrTearDown(MethodInfo methodInfo)
        {
            return 
                methodInfo.Name == "setUp" || 
                methodInfo.Name == "tearDown";
        }
    }

}

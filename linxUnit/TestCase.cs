using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;

namespace linxUnit
{
    public abstract class TestCase : ITest
    {
        internal string name;

        public TestCase(string name)
        {
            this.name = name;
        }

        protected TestCase()
        {

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

        private static BindingFlags testMethodbindingFlags = 
            BindingFlags.Instance | 
            BindingFlags.DeclaredOnly | 
            BindingFlags.Public;

        private MethodInfo GetTestMethod()
        {
            return GetType().GetMethod(name, testMethodbindingFlags);
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

        private static BindingFlags testConstructorBindingFlags = BindingFlags.Public | BindingFlags.Instance;

        private static TestCase CreateTestCase(Type type, MethodInfo method)
        {
            var constructors = type.GetConstructors(testConstructorBindingFlags);

            if (constructors.Any(c => IsNameConstructor(c)))
            {
                return Activator.CreateInstance(type, method.Name) as TestCase;
            }
            else
            {
                TestCase testCase = Activator.CreateInstance(type) as TestCase;
                testCase.name = method.Name;
                return testCase;
            }
        }

        private static bool IsNameConstructor(ConstructorInfo c)
        {
            var parameters = c.GetParameters();

            return parameters.Length == 1 && parameters[0].ParameterType == typeof(string);
        }

        private static MethodInfo[] GetMethods(Type type)
        {
            return type.GetMethods(testMethodbindingFlags);
        }

        private static bool IsSetUpOrTearDown(MethodInfo methodInfo)
        {
            return 
                methodInfo.Name == "setUp" || 
                methodInfo.Name == "tearDown";
        }
    }

}

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
        internal string Name
        {
            set { name = value; }
        }

        public TestCase(string name)
        {
            this.name = name;
        }

        protected TestCase()
        {

        }

        public virtual void SetUp() { }
        public virtual void TearDown() { }

        private Type expectedExceptionType;
        protected void ExpectException<T>() where T : Exception
        {
            expectedExceptionType = typeof(T);
        }

        public void Run(TestResult result)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new InvalidOperationException("No test name specified");
            }

            result.TestStarted(name);
            try
            {
                this.SetUp();

                RunTestMethod();

                result.TestSucceeded();
            }
            catch (InvalidOperationException exception)
            {
                throw exception;
            }
            catch (Exception exception)
            {
                var innerException = exception.InnerException;

                if (innerException != null)
                {
                    if (object.Equals(expectedExceptionType, innerException.GetType()))
                    {
                        result.TestSucceeded(innerException);
                    }
                    else if (!(innerException is AssertInconclusiveException))
                    {
                        result.TestFailed(innerException);
                    }
                }
            }
            finally
            {
                this.TearDown();
            }
        }

        private void RunTestMethod()
        {
            var method = GetTestMethod();
            
            if (method != null)
	        {
		        method.Invoke(this, null);
	        }
            else
            {
                throw new InvalidOperationException("Test name does not exists in test case");
            }
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
            if (!typeof(ITest).IsAssignableFrom(type))
            {
                throw new ArgumentException("Type must derive from ITest", "type");
            }

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
                suite.Add(testCase);
            }
            return suite;
        }

        protected static TestSuite CreateSuite<T>() where T : ITest
        {
            return CreateSuite(typeof(T));
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
                methodInfo.Name == "SetUp" || 
                methodInfo.Name == "TearDown";
        }
    }

}

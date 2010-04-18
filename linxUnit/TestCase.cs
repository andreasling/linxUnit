using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

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
                try
                {
                    this.setUp();
                }
                catch (Exception/* exception*/)
                {
                    //Console.WriteLine("setUp failed in {0}.{1}", this.GetType().Name, this.name);
                    throw;
                }
                Type type = this.GetType();
                MethodInfo methodInfo = type.GetMethod(name, BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public);
                methodInfo.Invoke(this, null);

                result.testSucceeded();
            }
            catch (Exception exception)
            {
                result.testFailed(exception.InnerException);
            }
            finally
            {
                //try
                //{
                this.tearDown();
                //}
                //catch (Exception)
                //{
                //    Console.WriteLine("tearDown failed in {0}.{1}", this.GetType().Name, this.name);
                //}
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

}

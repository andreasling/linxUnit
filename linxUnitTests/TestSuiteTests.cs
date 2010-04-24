using System;
using System.Collections.Generic;
using System.Text;
using linxUnit;

namespace linxUnitTests
{
    public class TestSuiteTests : TestCase
    {
        public TestSuiteTests(string name)
            : base(name)
        {
        }

        public void testMethod()
        {
            // TODO: Arrange
            TestSuite suite = new TestSuite(); // TestCase.CreateSuite(typeof(TestSuiteTests));

            // TODO: Act
            suite.Add(new DummyTestCase("dummyTestCase"));

            // TODO: Assert
            Assert.AreEqual(1, suite.Tests.Count);
        }

        private class DummyTestCase : TestCase
        {
            public DummyTestCase(string name)
                : base(name)
            {

            }
        }
    }

}

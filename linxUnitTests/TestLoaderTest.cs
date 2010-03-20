using System;
using System.Collections.Generic;
using System.Text;
using linxUnit;

namespace linxUnitTests
{
    public class TestLoaderTest : TestCase
    {
        public TestLoaderTest(string name)
            : base(name)
        {
        }

        public void testLoadFromDirectory()
        {
            // TODO: Arrange
            TestSuite suite = null;
            TestLoader loader = new TestLoader();
            string directory = @"D:\git\linxUnit\linxUnitTests\bin\Debug\";

            // TODO: Act
            suite = loader.LoadFromDirectory(directory);

            // TODO: Assert
            Assert.IsTrue(suite.Tests.Count > 0);
        }

        public void testLoadFromFile()
        {
            // TODO: Arrange
            TestSuite suite = null;
            TestLoader loader = new TestLoader();
            string file = @"D:\git\linxUnit\linxUnitTests\bin\Debug\linxUnitTests.exe";

            // TODO: Act
            suite = loader.LoadFromFile(file);

            // TODO: Assert
            Assert.IsTrue(suite.Tests.Count > 0);
        }
    }
}

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

        public void testLoadFromDirectoryAbsolute()
        {
            // Arrange
            TestSuite suite = null;
            TestLoader loader = new TestLoader();
            string directory = System.IO.Path.GetFullPath(@"..\..\..\TestLoaderTestTests\bin\Debug\");

            // Act
            suite = loader.LoadFromDirectory(directory);

            // Assert
            Assert.AreEqual(1, suite.Tests.Count);
        }

        public void testLoadFromDirectoryRelative()
        {
            // Arrange
            TestSuite suite = null;
            TestLoader loader = new TestLoader();
            string directory = @"..\..\..\TestLoaderTestTests\bin\Debug\";

            // Act
            suite = loader.LoadFromDirectory(directory);

            // Assert
            Assert.AreEqual(1, suite.Tests.Count);
        }

        public void testLoadFromDirectoryFail()
        {
            // Arrange
            TestSuite suite = null;
            TestLoader loader = new TestLoader();
            string directory = "x";

            try
            {
                // Act
                suite = loader.LoadFromDirectory(directory);

                Assert.Fail();
            }
            catch (ArgumentException actual)
            {
                // Assert
                Assert.AreEqual("Directory does not exist\r\nParameter name: directory", actual.Message);
            }
        }

        public void testLoadFromFileAbsolute()
        {
            // Arrange
            TestSuite suite = null;
            TestLoader loader = new TestLoader();
            string file = System.IO.Path.GetFullPath(@"..\..\..\TestLoaderTestTests\bin\Debug\TestLoaderTestTests.dll");

            // Act
            suite = loader.LoadFromFile(file);

            // Assert
            Assert.AreEqual(1, suite.Tests.Count);
        }

        public void testLoadFromFileRelative()
        {
            // Arrange
            TestSuite suite = null;
            TestLoader loader = new TestLoader();
            string file = @"..\..\..\TestLoaderTestTests\bin\Debug\TestLoaderTestTests.dll";

            // Act
            suite = loader.LoadFromFile(file);

            // Assert
            Assert.AreEqual(1, suite.Tests.Count);
        }

        public void testLoadFromFileFail()
        {
            // Arrange
            TestSuite suite = null;
            TestLoader loader = new TestLoader();
            string file = "x";

            try
            {
                // Act
                suite = loader.LoadFromFile(file);

                Assert.Fail();
            }
            catch (ArgumentException actual)
            {
                // Assert
                Assert.AreEqual("File does not exist\r\nParameter name: file", actual.Message);
            }
        }
    }
}

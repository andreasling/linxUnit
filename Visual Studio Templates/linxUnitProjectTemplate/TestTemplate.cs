using System;
using System.Collections.Generic;
using System.Text;
using linxUnit;

namespace linxUnitProjectTemplate
{
    public class TestTemplate : TestCase
    {
        /// <summary>
        /// This is the set up method
        /// </summary>
        public override void SetUp()
        {
            // TODO: Add set up logic here
        }

        /// <summary>
        /// This is the tear down method
        /// </summary>
        public override void TearDown()
        {
            // TODO: Add tear down logic here
        }

        /// <summary>
        /// This is a test method. Any public instance method except SetUp and TearDown is considered a test method.
        /// </summary>
        public void TestMethod()
        {
            // Arrange
            // TODO: Add arrange logic here
            bool b = false;

            // Act
            // TODO: Add act logic here
            b = !b;

            // Assert
            // TODO: Add assert logic here
            Assert.IsTrue(b);
        }
    }
}

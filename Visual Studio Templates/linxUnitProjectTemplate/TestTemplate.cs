using System;
using System.Collections.Generic;
using System.Text;
using linxUnit;

namespace linxUnitProjectTemplate
{
    public class TestTemplate : TestCase
    {
        public TestTemplate(string name)
            : base(name)
        {
        }

        /// <summary>
        /// This is the set up method
        /// </summary>
        public override void setUp()
        {
            // TODO: Add set up logic here
        }

        /// <summary>
        /// This is the tear down method
        /// </summary>
        public override void tearDown()
        {
            // TODO: Add tear down logic here
        }

        /// <summary>
        /// This is a test method. Test method names start with "test"
        /// </summary>
        public void testMethod()
        {
            // TODO: Arrange
            bool b = false;

            // TODO: Act
            b = !b;

            // TODO: Assert
            Assert.IsTrue(true);
        }
    }
}

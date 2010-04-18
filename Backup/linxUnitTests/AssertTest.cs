using System;
using System.Collections.Generic;
using System.Text;
using linxUnit;

namespace linxUnitTests
{
    public class AssertTest : TestCase
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
            Assert.AreEqual(new byte[] { 0, 1, 2 }, new byte[] { 0, 1, 2 });
            Assert.AreEqual(new string[] { "alfa", "beta", "gamma" }, new string[] { "alfa", "beta", "gamma" });
            Assert.AreEqual(new object[] { 1, "beta", true }, new object[] { 1, "beta", true });
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

            try
            {
                Assert.AreEqual(new byte[] { 0, 1, 2 }, new byte[] { 0, 2, 2 });
                throw new Exception();
            }
            catch (AssertionErrorException)
            {
            }

            try
            {
                Assert.AreEqual(new byte[] { 0, 1, 2 }, new byte[] { 0, 1, 2, 3 });
                throw new Exception();
            }
            catch (AssertionErrorException)
            {
            }

            // TODO: should array type matter?
            //try
            //{
            //    Assert.AreEqual(new string[] { "alfa", "beta", "gamma" }, new object[] { "alfa", "beta", "gamma" });
            //    throw new Exception();
            //}
            //catch (AssertionErrorException)
            //{
            //}
        }
    }

}

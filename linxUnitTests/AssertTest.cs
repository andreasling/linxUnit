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
            catch (AssertFailedException exception)
            {
                Assert.AreEqual("Expression was not true.", exception.Message);
            }
        }

        public void testIsFalseSuccess()
        {
            Assert.IsFalse(false);
        }

        public void testIsFalseFailure()
        {
            try
            {
                Assert.IsFalse(true);
                throw new Exception();
            }
            catch (AssertFailedException exception)
            {
                Assert.AreEqual("Expression was not false.", exception.Message);
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

        public void testAreEqualBoolFailureBool()
        {
            try
            {
                Assert.AreEqual(true, false);
                throw new Exception();
            }
            catch (AssertFailedException exception)
            {
                Assert.AreEqual("Actual value [False] did not equal expected value [True].", exception.Message);
            }
        }

        public void testAreEqualIntFailure()
        {
            try
            {
                Assert.AreEqual(0, 1);
                throw new Exception();
            }
            catch (AssertFailedException exception)
            {
                Assert.AreEqual("Actual value [1] did not equal expected value [0].", exception.Message);
            }
        }

        public void testAreEqualStringFailure()
        {
            try
            {
                Assert.AreEqual("test", "fail");
                throw new Exception();
            }
            catch (AssertFailedException exception)
            {
                Assert.AreEqual("Actual value [fail] did not equal expected value [test].", exception.Message);
            }
        }

        public void testAreEqualArrayElementFailure()
        {
            try
            {
                Assert.AreEqual(new byte[] { 0, 1, 2 }, new byte[] { 0, 2, 2 });
                throw new Exception();
            }
            catch (AssertFailedException)
            {
            }
        }

        public void testAreEqualArrayLengthFailure()
        {
            try
            {
                Assert.AreEqual(new byte[] { 0, 1, 2 }, new byte[] { 0, 1, 2, 3 });
                throw new Exception();
            }
            catch (AssertFailedException)
            {
            }
        }

        // TODO: should array type matter?
        public void testAreEqualArrayTypeFailure()
        {
            try
            {
                Assert.AreEqual(new string[] { "alfa", "beta", "gamma" }, new object[] { "alfa", "beta", "gamma" });
                throw new Exception();
            }
            catch (AssertFailedException)
            {
            }
        }

        public void testAreEqualArrayFormattingFailure()
        {
            try
            {
                Assert.AreEqual(new byte[] { 0, 1, 2, 3 }, new byte[] { 0, 2, 2 });
                throw new Exception();
            }
            catch (AssertFailedException exception)
            {
                Assert.AreEqual("Actual value [{0, 2, 2}] did not equal expected value [{0, 1, 2, 3}].", exception.Message);
            }
        }

        public void testAreEqualArrayZeroWidthBugFailure()
        {
            try
            {
                Assert.AreEqual(new byte[] { }, new byte[] { 0 });
                throw new Exception();
            }
            catch (AssertFailedException exception)
            {
                Assert.AreEqual("Actual value [{0}] did not equal expected value [{}].", exception.Message);
            }
        }

        public void testIsNullSuccess()
        {
            Assert.IsNull(null as object);
        }

        public void testIsNullFailure()
        {
            try
            {
                Assert.IsNull(new object());
                throw new Exception();
            }
            catch (AssertFailedException exception)
            {
                Assert.AreEqual("Expression was not null.", exception.Message);
            }
        }

        public void testIsNotNullSuccess()
        {
            Assert.IsNotNull(new object());
        }

        public void testIsNotNullFailure()
        {
            try
            {
                Assert.IsNotNull(null as object);
                throw new Exception();
            }
            catch (AssertFailedException exception)
            {
                Assert.AreEqual("Expression was null.", exception.Message);
            }
        }

        public void testInconclusive()
        {
            try
            {
                Assert.Inconclusive();
            }
            catch (AssertInconclusiveException exception)
            {
                Assert.AreEqual("Test was inconclusive.", exception.Message);
            }
        }

        public void testAssertFail()
        {
            try
            {
                Assert.Fail();
            }
            catch (AssertFailedException exception)
            {
                Assert.AreEqual("Test failed.", exception.Message);
            }
        }
    }

}

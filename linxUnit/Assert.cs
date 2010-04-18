using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace linxUnit
{
    public class Assert
    {
        public static void IsTrue(bool expression)
        {
            if (!expression)
            {
                throw new AssertionErrorException(string.Format("Expression was not true.", expression));
            }
        }

        public static void IsFalse(bool expression)
        {
            if (expression)
            {
                throw new AssertionErrorException(string.Format("Expression was not false.", expression));
            }
        }

        public static void AreEqual<T>(T expected, T actual)
        {
            if (!object.Equals(expected, actual))
            {
                if (expected is Array && actual is Array)
                {
                    Array expecteds = expected as Array;
                    Array actuals = actual as Array;

                    AreEqual(expecteds.Length, actuals.Length);

                    IEnumerator expectedsEnumerator = expecteds.GetEnumerator();
                    IEnumerator actualsEnumerator = actuals.GetEnumerator();

                    while (expectedsEnumerator.MoveNext() && actualsEnumerator.MoveNext())
                    {
                        AreEqual(expectedsEnumerator.Current, actualsEnumerator.Current);
                    }
                }
                else
                {
                    throw new AssertionErrorException(string.Format("Actual value [{0}] did not equal expected value [{1}].", actual, expected));
                }
            }
        }

        public static void IsNull<T>(T actual) where T : class
        {
            Assert.IsTrue(null == actual);
        }
    }
}

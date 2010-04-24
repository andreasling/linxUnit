using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Linq;

namespace linxUnit
{
    public class Assert
    {
        public static void IsTrue(bool expression)
        {
            if (!expression)
            {
                throw new AssertFailedException(string.Format("Expression was not true.", expression));
            }
        }

        public static void IsFalse(bool expression)
        {
            if (expression)
            {
                throw new AssertFailedException(string.Format("Expression was not false.", expression));
            }
        }

        public static void AreEqual<T>(T expected, T actual)
        {
            if (!object.Equals(expected, actual))
            {
                if (expected is string || actual is string)
                {
                    ThrowAssertionNotEqualError(expected, actual);
                }
                if (expected is IEnumerable && actual is IEnumerable && expected.GetType().Equals(actual.GetType()) )
                {
                    IEnumerable expecteds = expected as IEnumerable;
                    IEnumerable actuals = actual as IEnumerable;

                    IEnumerator expectedsEnumerator = expecteds.GetEnumerator();
                    IEnumerator actualsEnumerator = actuals.GetEnumerator();

                    bool areEqual = false;
                    bool expectedHadNext = false, actualHadNext = false;
                    do
                    {
                        expectedHadNext = expectedsEnumerator.MoveNext();
                        actualHadNext = actualsEnumerator.MoveNext();

                        if (expectedHadNext && actualHadNext)
                        {
                            areEqual = object.Equals(expectedsEnumerator.Current, actualsEnumerator.Current);

                            if (!areEqual)
                            {
                                break;
                            }
                        }
                        else if (!expectedHadNext && !actualHadNext)
                        {
                            areEqual = true;
                        }
                        else
                        {
                            areEqual = false;
                        }
                    } while (expectedHadNext && actualHadNext);

                    if (!areEqual)
                    {
                        ThrowAssertionNotEqualError(FormatEnumerableValue(expecteds), FormatEnumerableValue(actuals));
                    }
                }
                else
                {
                    ThrowAssertionNotEqualError(expected, actual);
                }
            }
        }

        private static void ThrowAssertionNotEqualError<T>(T expected, T actual)
        {
            string message = string.Format(
                "Actual value [{0}] did not equal expected value [{1}].",
                actual,
                expected);

            throw new AssertFailedException(message);
        }

        private static string FormatEnumerableValue(IEnumerable values)
        {
            StringBuilder formattedValue = new StringBuilder();

            foreach (var item in values)
            {
                formattedValue.AppendFormat("{0}, ", item);
            }
            if (formattedValue.Length > 0)
            {
                formattedValue.Remove(formattedValue.Length - 2, 2);
            }

            formattedValue.Insert(0, "{");
            formattedValue.Append("}");

            return formattedValue.ToString();
        }

        public static void IsNull<T>(T actual) where T : class
        {
            if (null != actual)
            {
                throw new AssertFailedException("Expression was not null.");
            }
        }

        public static void IsNotNull(object o)
        {
            if (null == o)
            {
                throw new AssertFailedException("Expression was null.");
            }
        }

        public static void Inconclusive()
        {
            throw new AssertInconclusiveException("Test was inconclusive.");
        }

        public static void Fail()
        {
            throw new AssertFailedException("Test failed.");
        }
    }
}

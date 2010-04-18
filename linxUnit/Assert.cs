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
                if (expected is string || actual is string)
                {
                    throw new AssertionErrorException(string.Format("Actual value [{0}] did not equal expected value [{1}].", actual, expected));
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
                        StringBuilder
                            formattedActual = new StringBuilder(),
                            formattedExpected = new StringBuilder();

                        foreach (var item in actuals)
                        {
                            formattedActual.AppendFormat("{0}, ", item);
                        }
                        formattedActual.Remove(formattedActual.Length - 2, 2);

                        foreach (var item in expecteds)
                        {
                            formattedExpected.AppendFormat("{0}, ", item);
                        }
                        formattedExpected.Remove(formattedExpected.Length - 2, 2);

                        throw new AssertionErrorException(string.Format("Actual value [{{{0}}}] did not equal expected value [{{{1}}}].", formattedActual, formattedExpected));
                    }
                }
                else
                {
                    //string formattedActual = actual.ToString();
                    //string formattedExpected = expected.ToString();

                    //if (T is IEnumerable)
                    //{
                    //    formattedActual = 
                    //}

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

using System;
using System.Collections.Generic;
using System.Text;
using linxUnit;
using MyCalculator;

namespace MyCalculatorTests
{
    /* todolist:
     * todo: Add integers
     */

    class MyCalculatorTests : TestCase
    {
        public MyCalculatorTests(string name)
            : base(name)
        {
        }

        public void testAdd()
        {
            Calculator calculator = new Calculator();
            int augend = 1, addend = 2, expected = 3, actual;
            actual = calculator.Add(augend, addend);
            Assert.AreEqual(expected, actual);
        }
    }
}

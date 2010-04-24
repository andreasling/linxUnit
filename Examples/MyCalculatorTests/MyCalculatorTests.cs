using System;
using System.Collections.Generic;
using System.Text;
using linxUnit;
using MyCalculator;

namespace MyCalculatorTests
{
    /* todolist:
     * done: Add integers
     * todo: Substract integers
     */

    class MyCalculatorTests : TestCase
    {
        public void testAdd()
        {
            // Arrange
            Calculator calculator = new Calculator();
            int augend = 1, 
                addend = 2, 
                expected = 3, 
                actual;

            // Act
            actual = calculator.Add(augend, addend);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        public void TestSubstract()
        {
            // Arrange
            Calculator calculator = new Calculator();
            int minuend = 3, 
                subtrahend = 2, 
                expected = 1, 
                actual;

            // Act
            actual = calculator.Substract(minuend, subtrahend);

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}

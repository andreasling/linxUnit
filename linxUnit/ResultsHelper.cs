using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace linxUnit
{
    public static class ResultsHelper
    {
        public static void DisplayResults(TestResult result)
        {
            foreach (var detail in result.details)
            {
                var oldColor = Console.ForegroundColor;

                var color =
                    detail.inconclusive ?
                        ConsoleColor.DarkYellow :
                    detail.success ?
                        ConsoleColor.DarkGreen :
                        ConsoleColor.DarkRed;

                Console.ForegroundColor = color;

                Console.Write(detail.message);
                if (!detail.success)
                {
                    Console.Write(": " + detail.failure.exception.Message);
                }
                Console.WriteLine();

                Console.ForegroundColor = oldColor;
            }

            Console.WriteLine();

            Console.WriteLine(result.summary());
        }
    }
}

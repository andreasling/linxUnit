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
            foreach (var detail in result.Details)
            {
                var oldColor = Console.ForegroundColor;

                var color =
                    detail.Inconclusive ?
                        ConsoleColor.DarkYellow :
                    detail.Success ?
                        ConsoleColor.DarkGreen :
                        ConsoleColor.DarkRed;

                Console.ForegroundColor = color;

                Console.Write(detail.Message);
                if (!detail.Success && detail.failure != null)
                {
                    Console.Write(": " + detail.failure.Exception.Message);
                }
                Console.WriteLine();

                Console.ForegroundColor = oldColor;
            }

            Console.WriteLine();

            Console.WriteLine(result.Summary());
        }
    }
}

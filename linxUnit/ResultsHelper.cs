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

                switch (detail.State)
	            {
		            case TestResultState.Inconclusive:
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        break;

                    case TestResultState.Success:
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        break;

                    case TestResultState.Failure:
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        break;

                    default:
                        break;
	            }

                Console.Write(detail.Message);
                if (!detail.Success && detail.Exception != null)
                {
                    Console.Write(": " + detail.Exception.Message);
                }
                Console.WriteLine();

                Console.ForegroundColor = oldColor;
            }

            Console.WriteLine();

            Console.WriteLine(result.Summary());
        }
    }
}

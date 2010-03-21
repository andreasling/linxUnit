using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;
using System.Diagnostics;
using System.Linq;

namespace linxUnit
{
    class Program
    {
        static void Main(string[] args)
        {
            //Debug.Listeners.Add(new ConsoleTraceListener(true));

            if (args.Length > 0)
            {
                var suite = new TestSuite();
                var loader = new TestLoader();

                foreach (var arg in args)
                {
                    Debug.WriteLine(arg);

                    if (Directory.Exists(arg))
                    {
                        suite.add(loader.LoadFromDirectory(arg));
                    } 
                    else if (File.Exists(arg))
                    {
                        suite.add(loader.LoadFromFile(arg));
                    }
                }

                TestResult result = new TestResult();

                suite.run(result);

                foreach (var failure in result.failures)
                {
                    Console.WriteLine(failure.message + ": " + failure.exception.Message);
                }
                Console.WriteLine(result.summary());
            }
            else
            {
                Console.WriteLine("No arguments.");
            }

        }
    }
}

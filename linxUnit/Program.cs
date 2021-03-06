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
            if (args.Length > 0)
            {
                RunHelper.StartTimedTestRun(suite =>
                {
                    var loader = new TestLoader();

                    foreach (var arg in args)
                    {
                        Debug.WriteLine(arg);

                        if (Directory.Exists(arg))
                        {
                            suite.Add(loader.LoadFromDirectory(arg));
                        } 
                        else if (File.Exists(arg))
                        {
                            suite.Add(loader.LoadFromFile(arg));
                        }
                    }
                });
            }
            else
            {
                Console.WriteLine("No arguments.");
            }

        }
    }
}

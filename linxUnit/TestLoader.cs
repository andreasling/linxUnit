using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.Diagnostics;

namespace linxUnit
{
    public class TestLoader
    {
        public TestSuite LoadFromDirectory(string directory)
        {
            Debug.WriteLine("LoadFromDirectory: " + directory);

            TestSuite suite = new TestSuite();

            LoadTests(suite, directory);

            return suite;
        }

        private static void LoadTests(TestSuite suite, string directory)
        {
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler((sender, eventArgs) =>
            {
                Debug.WriteLine("AssemblyResolve: " + eventArgs.Name);

                var assemblyNamex = new AssemblyName(eventArgs.Name);

                List<string> files = new List<string>();

                // Add probable file names
                var filePathWithoutExtrension = Path.Combine(directory, assemblyNamex.Name);
                files.Add(Path.ChangeExtension(filePathWithoutExtrension, "dll"));
                files.Add(Path.ChangeExtension(filePathWithoutExtrension, "exe"));

                // Add other assembly files names in directory
                files.AddRange( 
                    from file in Directory.GetFiles(directory)
                    where IsAssemblyFile(file) && !files.Contains(file)
                    select file);

                // TODO: filter out aready loaded assembly like linxUnit and the test assembly?

                foreach (var file in files)
                {
                    try
                    {
                        // TODO: is this necessary? perhaps for relative paths?
                        string fullFileName = new FileInfo(file).FullName;

                        if (File.Exists(fullFileName))
                        {
                            var assemblyName = AssemblyName.GetAssemblyName(fullFileName).FullName;

                            if (assemblyName == eventArgs.Name)
                            {
                                var assembly = TryLoadAssembly(fullFileName);

                                if (assembly != null)
                                {
                                    return assembly;
                                }
                            }
                        }
                    }
                    catch (ArgumentException)
                    {
                        // ignore exceptions from AssemblyName.GetAssemblyName(...)
                    }
                    catch (BadImageFormatException)
                    {
                        // ignore exceptions from AssemblyName.GetAssemblyName(...)
                    }
                }

                return null;
            });

            var assemblyFiles = GetAssemblyFiles(directory);

            var assemblies = LoadAssemblies(assemblyFiles);

            var testTypes = GetTestTypes(assemblies);

            foreach (var type in testTypes)
            {
                Debug.WriteLine(string.Format("Found test: {0}", type.FullName));

                suite.add(TestCase.CreateSuite(type));
            }
        }

        private static IEnumerable<Type> GetTestTypes(IEnumerable<Assembly> assemblies)
        {
            var assembliesTestTypes = 
                from assembly in assemblies
                select new { TestTypes = GetTestTypes(assembly) };

            return assembliesTestTypes.SelectMany(
                assemblyTestTypes => assemblyTestTypes.TestTypes);
        }

        private static IEnumerable<Assembly> LoadAssemblies(IEnumerable<string> assemblyFiles)
        {
            return from assemblyFile in assemblyFiles
                   select Assembly.LoadFile(assemblyFile);
        }

        private static IEnumerable<Type> GetTestTypes(Assembly assembly)
        {
            var types = assembly.GetTypes();

            var iTestType = typeof(TestCase);

            var testTypes =
                from type in types
                where iTestType.IsAssignableFrom(type)
                select type;
            return testTypes;
        }

        private static IEnumerable<string> GetAssemblyFiles(string outputFolder)
        {
            var assemblyFiles =
                from file in Directory.GetFiles(outputFolder)
                where IsAssemblyFile(file) && !IsExcludedFile(file)
                select file;

            return assemblyFiles;
        }

        private static bool IsExcludedFile(string file)
        {
            return Path.GetFileName(file) == "linxUnit.exe";
        }

        private static bool IsAssemblyFile(string file)
        {
            var extension = Path.GetExtension(file);
            return
                !file.EndsWith(".vshost.exe") &&
                extension == ".exe" ||
                extension == ".dll";
        }

        private static System.Reflection.Assembly TryLoadAssembly(string path)
        {
            Debug.WriteLine("TryLoadAssembly: " + path);

            System.Reflection.Assembly assembly = null;

            if (System.IO.File.Exists(path))
            {
                try
                {
                    assembly =
                        //System.Reflection.Assembly.LoadFile(path) :
                        System.Reflection.Assembly.LoadFrom(path);

                    if (assembly != null)
                    {
                        Debug.WriteLine("TryLoadAssembly: loaded " + path);
                    }
                }
                catch (Exception e)
                {
                    // ignore exceptions from Assembly.LoadFrom(...)
                    Debug.WriteLine("TryLoadAssembly: exception: " + e.ToString());
                }
            }

            return assembly;
        }
    }
}

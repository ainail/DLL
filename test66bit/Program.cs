using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.IO;

namespace test66bit
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintTypeAndMethodNames(args[0]);
        }
        static void PrintTypeAndMethodNames(string path)
        {
            if (!Directory.Exists(path))
            {
                Console.WriteLine("Directory does not exist");
                return;
            }
            var files = Directory.GetFiles(path);
            foreach (var file in files.Where(_ => _.EndsWith(".dll")))
            {
                try
                {
                    AssemblyName.GetAssemblyName(file);
                }
                catch (BadImageFormatException)
                {
                    Console.WriteLine(file + " is not an assembley");
                    continue;
                }
                var types = GetTypes(file);
                foreach (var type in types)
                {
                    Console.WriteLine(type.Name);
                    var methods = GetTypeMethods(type);
                    foreach (var method in methods)
                    {
                        Console.WriteLine("\t" + method);
                    }
                }
            }

            IEnumerable<TypeInfo> GetTypes(string filepath)
            {
                var dll = Assembly.LoadFile(filepath);
                return dll.DefinedTypes;
            }

            IEnumerable<string> GetTypeMethods(TypeInfo type)
            {
                return type.DeclaredMethods.Where(_ => _.Attributes.HasFlag(MethodAttributes.Public) ||
                 _.Attributes.HasFlag(MethodAttributes.Family)).Select(_ => _.Name);

            }
        }

    }
}

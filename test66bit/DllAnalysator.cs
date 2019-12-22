using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.IO;

namespace test66bit
{
    public class DllAnalysator : IDllAnalysator
    {
        private string _path;
        public DllAnalysator(string path)
        {
            _path = path;
        }
        public IEnumerable<string> GetTypesAndMethods()
        {
            var typesAndMethodNames = new List<string>();
            if (!Directory.Exists(_path))
            {
                typesAndMethodNames.Add("Directory does not exist");
                return typesAndMethodNames;
            }
            var files = Directory.GetFiles(_path);

            foreach (var file in files.Where(f => f.EndsWith(".dll")))
            {
                try
                {
                    AssemblyName.GetAssemblyName(file);
                }
                catch (BadImageFormatException)
                {
                    typesAndMethodNames.Add(file + " is not an assembley");
                    continue;
                }
                var types = GetTypes(file);
                foreach (var type in types)
                {
                    typesAndMethodNames.Add(type.Name);
                    var methods = GetTypeMethods(type);
                    foreach (var method in methods)
                    {
                        typesAndMethodNames.Add("\t" + method);
                    }
                }
            }

            IEnumerable<TypeInfo> GetTypes(string filepath)
            {
                var dll = Assembly.LoadFile(filepath);
                return dll.DefinedTypes;
            }

            return typesAndMethodNames;
        }
        private IEnumerable<TypeInfo> GetTypes(string filepath)
        {
            var dll = Assembly.LoadFile(filepath);
            return dll.DefinedTypes;
        }

        private IEnumerable<string> GetTypeMethods(TypeInfo type)
        {
            return type.DeclaredMethods.Where(m => m.Attributes.HasFlag(MethodAttributes.Public) ||
             m.Attributes.HasFlag(MethodAttributes.Family)).Select(m => m.Name);

        }
    }
}

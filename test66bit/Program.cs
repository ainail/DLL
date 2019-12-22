using System;
using System.Linq;

namespace test66bit
{
    class Program
    {
        static void Main(string[] args)
        {
            var analysator = new DllAnalysator(args[0]);
            foreach (var str in analysator.GetTypesAndMethods())
                Console.WriteLine(str);
        }
    }

}

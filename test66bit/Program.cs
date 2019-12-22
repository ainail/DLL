using System;
using System.Linq;

namespace test66bit
{
    class Program
    {
        static void Main(string[] args)
        {
            IDllAnalysator analysator = new DllAnalysator(args[0]);
            foreach (var str in analysator.GetTypesAndMethods())
                Console.WriteLine(str);
        }
    }

}

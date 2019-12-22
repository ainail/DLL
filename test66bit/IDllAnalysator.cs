using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test66bit
{
    public interface IDllAnalysator
    {
        IEnumerable<string> GetTypesAndMethods();
    }
}

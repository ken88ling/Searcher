using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Searcher
{
    public static class Helper
    {
        public static string RemoveEmpty(this string str)
        {
            return str.Replace("\r\n", string.Empty);
        }
    }
}

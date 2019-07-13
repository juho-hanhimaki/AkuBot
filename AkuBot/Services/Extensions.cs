using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AkuBot
{
    public static class Extensions
    {
        public static bool ContainsAny(this string input, IEnumerable<string> containsKeywords)
        {
            return containsKeywords.Any(keyword => input.IndexOf(keyword) >= 0);
        }
    }
}

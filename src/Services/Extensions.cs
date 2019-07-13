using System.Collections.Generic;
using System.Linq;

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

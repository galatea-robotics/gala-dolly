using System;
using System.Globalization;

namespace Gala.Dolly.Test
{
    static class StringExtension
    {
        static public bool ContainsCurrentCulture(this string str, string value)
        {
            return str.Contains(value, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}

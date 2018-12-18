using System;
using System.Globalization;

namespace Gala.Dolly.Test
{
    static class StringExtension
    {
        static public bool ContainsCurrentCulture(this string str, string value)
        {
            return str.ToUpper(CultureInfo.CurrentCulture).Contains(value.ToUpper(CultureInfo.CurrentCulture));
        }
    }
}

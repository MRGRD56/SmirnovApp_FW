using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmirnovApp.Extensions
{
    public static class StringExtensions
    {
        public static string ConcatString(this string firstString, string secondString) =>
            string.Concat(firstString, secondString);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmirnovApp.Extensions
{
    public static class Validation
    {
        public static string GetValidationErrorText(IEnumerable<string> validationErrors, string title = null)
        {
            var errorsList = validationErrors.ToList();

            if (errorsList?.Any() != true) return null;

            var message = title?.ConcatString("\n") ?? "";
            message += string.Join("\n", errorsList.Select(error => $"• {error}"));

            return message;
        }
    }
}

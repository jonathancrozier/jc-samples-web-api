using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace JC.Samples.Web.Api.Extensions
{
    /// <summary>
    /// Contains extension methods which deal with Strings.
    /// </summary>
    public static class StringExtensions
    {
        #region Methods

        /// <summary>
        /// Converts the specified name to camel case.
        /// </summary>
        /// <param name="name">The name to convert</param>
        /// <returns>A camel cased version of the specified name</returns>
        public static string ToCamelCase(this string name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            return string.Join(".", name.Split('.').Select(n => char.ToLower(n[0], CultureInfo.InvariantCulture) + n.Substring(1)));
        }

        /// <summary>
        /// Converts the name of a string (e.g. an Enum or Property name) to a user-friendly display format.
        /// e.g. 'FooBar3D' would be converted to 'Foo Bar 3D'.
        /// </summary>
        /// <param name="text">The string to operate on</param>
        /// <param name="removeStrings">A list of strings to remove from the text prior to formatting (optional)</param>
        /// <returns>A user-friendly display format string</returns>
        public static string ToDisplayFormat(this string text, string[] removeStrings = null)
        {
            string displayText = Convert.ToString(text);

            if (removeStrings != null)
            {
                foreach (string r in removeStrings)
                {
                    displayText = displayText.Replace(r, "");
                }
            }

            // The levels of Regex replacements work as follows (inner to outer).
            // 1. 'Pp' or 'PP';
            // 2. 'pP';
            // 3. Catches any kind of white-space (e.g. tabs, newlines, etc.) and replaces them with a single space.
            displayText = Regex.Replace(Regex.Replace(Regex.Replace(displayText, @"(\P{Ll})(\P{Ll}\p{Ll})", "$1 $2"), @"(\p{Ll})(\P{Ll})", "$1 $2"), @"\s+", " ");

            return displayText;
        }

        #endregion
    }
}
// <copyright file="RegexNoteMatcher.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Core.Queries.Text
{
    using System.Text.RegularExpressions;

    /// <summary>
    /// A matcher that checks that the field matches a specified regular expression.
    /// </summary>
    public class RegexNoteMatcher : TextFieldMatcher
    {
        /// <summary>
        /// The regular expression to use to check for matches.
        /// </summary>
        private readonly Regex regex;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegexNoteMatcher" /> class.
        /// </summary>
        /// <param name="searchField">The name of the note field to search.</param>
        /// <param name="isCaseSensitive">A value indicating whether a case sensitive search should be performed.</param>
        /// <param name="pattern">The regular expression pattern to look for.</param>
        public RegexNoteMatcher(string searchField, bool isCaseSensitive, string pattern)
            : base(searchField, true)
        {
            var regexOptions = RegexOptions.Compiled;
            if (!isCaseSensitive)
            {
                regexOptions |= RegexOptions.IgnoreCase;
            }

            this.regex = new Regex(pattern, regexOptions);
        }

        /// <summary>
        /// Checks if the specified note matches this rule.
        /// </summary>
        /// <param name="textFieldValue">The text to check.</param>
        /// <returns>True if the note was a match; false otherwise.</returns>
        protected override bool IsTextFieldMatch(string textFieldValue)
        {
            return this.regex.IsMatch(textFieldValue);
        }
    }
}

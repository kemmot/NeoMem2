// <copyright file="ContainsAllNoteMatcher.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Core.Queries.Text
{
    /// <summary>
    /// A note matcher that checks that all of several search terms can be found in specified note fields.
    /// </summary>
    public class ContainsAllNoteMatcher : MultiSearchTermTextFieldMatcher
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContainsAllNoteMatcher" /> class.
        /// </summary>
        /// <param name="searchField">The note fields to look in.</param>
        /// <param name="isCaseSensitive">True to perform a case sensitive match.</param>
        /// <param name="textToFind">The text to look for.</param>
        /// <param name="separator">The separator to use to split the multiple search terms from <paramref name="textToFind"/>.</param>
        public ContainsAllNoteMatcher(string searchField, bool isCaseSensitive, string textToFind, string separator)
            : base(searchField, isCaseSensitive, textToFind, separator, MultiSearchTermStrategy.All)
        {
        }
        
        /// <summary>
        /// Checks if the specified note matches this rule.
        /// </summary>
        /// <param name="caseAdjustedFieldValue">The text to check.</param>
        /// <param name="caseAdjustedSearchValue">The text to look for.</param>
        /// <returns>True if the note was a match; false otherwise.</returns>
        protected override bool IsTextFieldMatch(string caseAdjustedFieldValue, string caseAdjustedSearchValue)
        {
            return caseAdjustedFieldValue.Contains(caseAdjustedSearchValue);
        }
    }
}

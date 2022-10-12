// <copyright file="ContainsNoteMatcher.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Core.Queries.Text
{
    /// <summary>
    /// A text note matcher that checks whether the note text contains specified search text.
    /// </summary>
    public class ContainsNoteMatcher : TextFieldMatcher
    {
        /// <summary>
        /// The search text to look for.
        /// </summary>
        private readonly string searchTextToUse;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContainsNoteMatcher" /> class.
        /// </summary>
        /// <param name="searchField">The name of the note field to search.</param>
        /// <param name="isCaseSensitive">A value indicating whether a case sensitive search should be performed.</param>
        /// <param name="textToFind">The text to look for.</param>
        public ContainsNoteMatcher(string searchField, bool isCaseSensitive, string textToFind)
            : base(searchField, isCaseSensitive)
        {
            this.searchTextToUse = isCaseSensitive ? textToFind : textToFind.ToUpper();
        }

        /// <summary>
        /// Checks if the specified note matches this rule.
        /// </summary>
        /// <param name="caseAdjustedFieldValue">The text to check.</param>
        /// <returns>True if the note was a match; false otherwise.</returns>
        protected override bool IsTextFieldMatch(string caseAdjustedFieldValue)
        {
            return caseAdjustedFieldValue.Contains(this.searchTextToUse);
        }
    }
}

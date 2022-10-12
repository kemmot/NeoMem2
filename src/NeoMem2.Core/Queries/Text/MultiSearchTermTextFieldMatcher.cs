// <copyright file="MultiSearchTermTextFieldMatcher.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Core.Queries.Text
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A matcher that can look for multiple search terms in text fields.
    /// </summary>
    public abstract class MultiSearchTermTextFieldMatcher : TextFieldMatcher
    {
        /// <summary>
        /// The multiple search terms to look for.
        /// </summary>
        private readonly string[] searchTextsToUse;

        /// <summary>
        /// The multiple search term strategy to use.
        /// </summary>
        private readonly MultiSearchTermStrategy strategy;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiSearchTermTextFieldMatcher" /> class.
        /// </summary>
        /// <param name="searchField">The name of the note field to search.</param>
        /// <param name="isCaseSensitive">A value indicating whether a case sensitive search should be performed.</param>
        /// <param name="textToFind">The text to look for.</param>
        /// <param name="separator">The separator to use to split the multiple search terms from <paramref name="textToFind"/>.</param>
        /// <param name="strategy">The multiple search term strategy to use.</param>
        protected MultiSearchTermTextFieldMatcher(string searchField, bool isCaseSensitive, string textToFind, string separator, MultiSearchTermStrategy strategy)
            : base(searchField, isCaseSensitive)
        {
            if (separator == null)
            {
                throw new ArgumentNullException("separator");
            }

            if (separator.Length == 0)
            {
                throw new ArgumentException("Argument cannot be zero length", "separator");
            }

            string separatorToUse = isCaseSensitive ? separator : separator.ToUpper();
            string searchTextsToUse = isCaseSensitive ? textToFind : textToFind.ToUpper();
            this.searchTextsToUse = searchTextsToUse.Split(new[] { separatorToUse }, StringSplitOptions.RemoveEmptyEntries);
            this.strategy = strategy;
        }

        /// <summary>
        /// Checks if the fields are a match.
        /// </summary>
        /// <param name="fieldValues">The field values to check.</param>
        /// <returns>True if they are a match; false if not.</returns>
        protected override bool AreFieldsMatch(IEnumerable<KeyValuePair<string, object>> fields, out List<string> matchingFields)
        {
            bool isMatch;
            switch (this.strategy)
            {
                case MultiSearchTermStrategy.All:
                    isMatch = true;
                    break;
                case MultiSearchTermStrategy.Any:
                    isMatch = false;
                    break;
                default:
                    throw new NotSupportedException("Multi-search term strategy not supported: " + this.strategy);
            }

            matchingFields = new List<string>();
            foreach (string searchText in this.searchTextsToUse)
            {
                bool searchTextMatch = false;
                foreach (KeyValuePair<string, object> field in fields)
                {
                    string textFieldValue = field.Value == null ? string.Empty : field.Value.ToString();
                    if (!this.IsCaseSensitive)
                    {
                        textFieldValue = textFieldValue.ToUpper();
                    }

                    if (this.IsTextFieldMatch(textFieldValue, searchText))
                    {
                        searchTextMatch = true;
                        matchingFields.Add(field.Key);
                        break;
                    }
                }

                bool exit = false;
                switch (this.strategy)
                {
                    case MultiSearchTermStrategy.All:
                        if (!searchTextMatch)
                        {
                            isMatch = false;
                            exit = true;
                        }

                        break;
                    case MultiSearchTermStrategy.Any:
                        if (searchTextMatch)
                        {
                            isMatch = true;
                            exit = true;
                        }

                        break;
                    default:
                        throw new NotSupportedException("Multi-search term strategy not supported: " + this.strategy);
                }

                if (exit)
                {
                    break;
                }
            }

            return isMatch;
        }

        /// <summary>
        /// Checks if the specified note matches this rule.
        /// </summary>
        /// <param name="caseAdjustedFieldValue">The text to check.</param>
        /// <returns>True if the note was a match; false otherwise.</returns>
        protected override bool IsTextFieldMatch(string caseAdjustedFieldValue)
        {
            throw new NotSupportedException("This should never be called");
        }

        /// <summary>
        /// Checks if the specified note matches this rule.
        /// </summary>
        /// <param name="caseAdjustedFieldValue">The text to check.</param>
        /// <param name="caseAdjustedSearchValue">The text to look for.</param>
        /// <returns>True if the note was a match; false otherwise.</returns>
        protected abstract bool IsTextFieldMatch(string caseAdjustedFieldValue, string caseAdjustedSearchValue);
    }
}
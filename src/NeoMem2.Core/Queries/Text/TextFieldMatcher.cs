// <copyright file="TextFieldMatcher.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Core.Queries.Text
{
    /// <summary>
    /// A field matcher that searches text fields.
    /// </summary>
    public abstract class TextFieldMatcher : FieldMatcher
    {
        /// <summary>
        /// The backing field for the <see cref="IsCaseSensitive"/> property.
        /// </summary>
        private readonly bool isCaseSensitive;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextFieldMatcher" /> class.
        /// </summary>
        /// <param name="searchField">The name of the note field to search.</param>
        /// <param name="isCaseSensitive">A value indicating whether a case sensitive search should be performed.</param>
        protected TextFieldMatcher(string searchField, bool isCaseSensitive)
            : base(searchField)
        {
            this.isCaseSensitive = isCaseSensitive;
        }

        /// <summary>
        /// Gets a value indicating whether a case sensitive search should be performed.
        /// </summary>
        public bool IsCaseSensitive
        {
            get { return this.isCaseSensitive; }
        }
        
        /// <summary>
        /// Checks whether a field value is a match.
        /// </summary>
        /// <param name="fieldValue">The field value to check.</param>
        /// <returns>True if the field value was a match; false otherwise.</returns>
        protected override bool IsFieldMatch(object fieldValue)
        {
            string textFieldValue = fieldValue == null ? string.Empty : fieldValue.ToString();
            if (!this.IsCaseSensitive)
            {
                textFieldValue = textFieldValue.ToUpper();
            }

            return this.IsTextFieldMatch(textFieldValue);
        }

        /// <summary>
        /// Checks if the specified note matches this rule.
        /// </summary>
        /// <param name="caseAdjustedFieldValue">The text to check.</param>
        /// <returns>True if the note was a match; false otherwise.</returns>
        protected abstract bool IsTextFieldMatch(string caseAdjustedFieldValue);
    }
}

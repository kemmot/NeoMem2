// <copyright file="FieldMatcher.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Core.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// An implementation of <see cref="INoteMatcher"/> that searches individual note fields.
    /// </summary>
    public abstract class FieldMatcher : INoteMatcher
    {
        /// <summary>
        /// Backing field for the <see cref="SearchField"/> property.
        /// </summary>
        private readonly string searchField;

        /// <summary>
        /// Initializes a new instance of the <see cref="FieldMatcher" /> class.
        /// </summary>
        /// <param name="searchField">The name of the note field to search.</param>
        protected FieldMatcher(string searchField)
        {
            if (searchField == null)
            {
                throw new ArgumentNullException("searchField");
            }

            if (searchField.Length == 0)
            {
                throw new ArgumentException("Argument cannot be zero length", "searchField");
            }

            this.searchField = searchField;
        }

        /// <summary> 
        /// Gets the name of the note field to search.
        /// </summary>
        public string SearchFieldName
        {
            get { return this.searchField; }
        }

        /// <summary>
        /// Checks if the specified note matches this rule.
        /// </summary>
        /// <param name="note">The note to check.</param>
        /// <returns>True if the note was a match; false otherwise.</returns>
        public virtual bool IsMatch(Note note)
        {
            var fieldValues = new List<KeyValuePair<string, object>>();
            if (string.Compare(this.searchField, SearchField.All, StringComparison.OrdinalIgnoreCase) == 0)
            {
                fieldValues.Add(new KeyValuePair<string, object>(SearchField.Name, note.Name));
                fieldValues.Add(new KeyValuePair<string, object>(SearchField.Class, note.Class));
                fieldValues.AddRange(note.Tags.Select(tag => tag.Tag.Name).Select(tag => new KeyValuePair<string, object>(SearchField.Tags, tag)));
                fieldValues.AddRange(note.Properties.Select(property => new KeyValuePair<string, object>(property.Name, property.Value)));
            }
            else if (string.Compare(this.searchField, SearchField.Class, StringComparison.OrdinalIgnoreCase) == 0)
            {
                fieldValues.Add(new KeyValuePair<string, object>(SearchField.Class, note.Class));
            }
            else if (string.Compare(this.searchField, SearchField.Name, StringComparison.OrdinalIgnoreCase) == 0)
            {
                fieldValues.Add(new KeyValuePair<string, object>(SearchField.Name, note.Name));
            }
            else if (string.Compare(this.searchField, SearchField.Tags, StringComparison.OrdinalIgnoreCase) == 0)
            {
                fieldValues.AddRange(note.Tags.Select(tag => tag.Tag.Name).Select(tag => new KeyValuePair<string, object>(SearchField.Tags, tag)));
            }
            else if (string.Compare(this.searchField, SearchField.Text, StringComparison.OrdinalIgnoreCase) == 0)
            {
                fieldValues.Add(new KeyValuePair<string, object>(SearchField.Text, note.Text));
            }
            else
            {
                Property property;
                if (note.TryGetPropertyByName(this.searchField, out property))
                {
                    fieldValues.Add(new KeyValuePair<string, object>(property.Name, property.Value));
                }
            }

            List<string> matchingFields;
            bool isMatch = this.AreFieldsMatch(fieldValues, out matchingFields);

            if (isMatch)
            {
                int score = 0;
                foreach (string matchingField in matchingFields)
                {
                    switch (matchingField)
                    {
                        case SearchField.Class:
                            score += 50;
                            break;
                        case SearchField.Name:
                            score += 100;
                            break;
                        case SearchField.Tags:
                            score += 75;
                            break;
                        case SearchField.Text:
                            score += 10;
                            break;
                        default:
                            score += 20;
                            break;
                    }
                }

                note.Score = score;
            }
            else
            {
                note.Score = 0;
            }

            return isMatch;
        }

        /// <summary>
        /// Checks whether the specified field values match.
        /// </summary>
        /// <param name="fieldValues">The field values to check.</param>
        /// <param name="matchingFields">The fields that the note was matched on.</param>
        /// <returns>True if they are a match; false otherwise.</returns>
        protected virtual bool AreFieldsMatch(IEnumerable<KeyValuePair<string, object>> fieldValues, out List<string> matchingFields)
        {
            bool result = false;
            matchingFields = new List<string>();
            foreach (KeyValuePair<string, object> fieldValue in fieldValues)
            {
                if (IsFieldMatch(fieldValue.Value))
                {
                    result = true;
                    matchingFields.Add(fieldValue.Key);
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Checks whether a field value is a match.
        /// </summary>
        /// <param name="fieldValue">The field value to check.</param>
        /// <returns>True if the field value was a match; false otherwise.</returns>
        protected abstract bool IsFieldMatch(object fieldValue);
    }
}

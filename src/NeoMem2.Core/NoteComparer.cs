// <copyright file="NoteComparer.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Core
{
    using System.Collections.Generic;

    /// <summary>
    /// An implementation of <see cref="IComparer{T}"/> for the <see cref="Note"/> type.
    /// </summary>
    public class NoteComparer : IComparer<Note>
    {
        /// <summary>
        /// The name of the property to sort on before cleansing.
        /// </summary>
        private string originalSortPropertyName;

        /// <summary>
        /// The cleansed name of the property to sort on.
        /// </summary>
        private string sortPropertyName;

        /// <summary>
        /// A value indicating whether a reverse sort should be performed.
        /// </summary>
        private bool reverseSort;

        /// <summary>
        /// Initializes a new instance of the <see cref="NoteComparer" /> class.
        /// </summary>
        /// <param name="sortPropertyName">The name of the property to sort on.</param>
        /// <param name="reverseSort">A value indicating whether a reverse sort should be performed.</param>
        public NoteComparer(string sortPropertyName = Note.PropertyNameName, bool reverseSort = false)
        {
            this.sortPropertyName = sortPropertyName;
            this.reverseSort = reverseSort;
        }

        /// <summary>
        /// Sets the current sort.
        /// </summary>
        /// <param name="propertyName">The name of the property to sort on.</param>
        public void SetSortProperty(string propertyName)
        {
            this.originalSortPropertyName = propertyName;
            propertyName = propertyName.Replace(" ", string.Empty);

            if (propertyName == this.sortPropertyName)
            {
                this.reverseSort = !this.reverseSort;
            }
            else
            {
                this.reverseSort = false;
            }

            this.sortPropertyName = propertyName;
        }

        /// <summary>
        /// Compares two <see cref="Note"/> objects.
        /// </summary>
        /// <param name="left">The first note to compare.</param>
        /// <param name="right">The second note to compare.</param>
        /// <returns>The result of the comparison.</returns>
        public int Compare(Note left, Note right)
        {
            int result;
            switch (this.sortPropertyName)
            {
                case Note.PropertyNameClass:
                    result = left.Class.CompareTo(right.Class);
                    break;
                case Note.PropertyNameName:
                    result = left.Name.CompareTo(right.Name);
                    break;
                case Note.PropertyNameScore:
                    result = left.Score.CompareTo(right.Score);
                    break;
                case Note.PropertyNameTags:
                    result = left.TagText.CompareTo(right.TagText);
                    break;
                case Note.PropertyNameCreatedDate:
                    result = left.CreatedDate.CompareTo(right.CreatedDate);
                    break;
                case Note.PropertyNameLastAccessedDate:
                    result = left.LastAccessedDate.CompareTo(right.LastAccessedDate);
                    break;
                case Note.PropertyNameLastModifiedDate:
                    result = left.LastModifiedDate.CompareTo(right.LastModifiedDate);
                    break;
                case Note.PropertyNameTextFormat:
                    result = left.TextFormat.CompareTo(right.TextFormat);
                    break;
                default:
                    string leftValue = this.GetPropertyValueOrDefault(left);
                    string rightValue = this.GetPropertyValueOrDefault(right);
                    result = leftValue.CompareTo(rightValue);
                    break;
            }

            if (this.reverseSort)
            {
                result *= -1;
            }

            return result;
        }

        /// <summary>
        /// Gets the value of the sort property.
        /// </summary>
        /// <param name="note">The note to retrieve the property value from.</param>
        /// <returns>The property value or an empty string if it was not found.</returns>
        private string GetPropertyValueOrDefault(Note note)
        {
            Property property;
            return note.TryGetPropertyByName(this.originalSortPropertyName, out property) ? property.Value : string.Empty;
        }
    }
}

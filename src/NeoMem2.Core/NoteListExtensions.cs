// <copyright file="NoteListExtensions.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Core
{
    using System.Collections.Generic;

    /// <summary>
    /// Provides extension methods for note lists.
    /// </summary>
    public static class NoteListExtensions
    {
        /// <summary>
        /// Sorts the list by <see cref="Note.Name"/>.
        /// </summary>
        /// <param name="notes">The notes to sort.</param>
        /// <param name="pinnedAtTop">Whether to sort pinned notes to the top of the list.</param>
        public static void SortByName(this List<Note> notes, bool pinnedAtTop = true)
        {
            notes.Sort((left, right) =>
            {
                int result = left.IsPinned.CompareTo(right.IsPinned) * -1;
                if (result == 0)
                {
                    result = left.Name.CompareTo(right.Name);
                }

                return result;
            });
        }
    }
}

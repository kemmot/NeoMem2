// <copyright file="AllMatchNoteMatcher.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Core.Queries
{
    /// <summary>
    /// An implementation of <see cref="INoteMatcher"/> that always matches notes.
    /// </summary>
    public class AllMatchNoteMatcher : INoteMatcher
    {
        /// <summary>
        /// Checks if the specified note matches this rule.
        /// </summary>
        /// <param name="note">The note to check.</param>
        /// <returns>True if the note was a match; false otherwise.</returns>
        public bool IsMatch(Note note)
        {
            return true;
        }
    }
}

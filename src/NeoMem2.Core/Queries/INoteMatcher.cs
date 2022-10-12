// <copyright file="INoteMatcher.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Core.Queries
{
    /// <summary>
    /// The interface to implement to provide note searching functionality.
    /// </summary>
    public interface INoteMatcher
    {
        /// <summary>
        /// Checks if the specified note matches this rule.
        /// </summary>
        /// <param name="note">The note to check.</param>
        /// <returns>True if the note was a match; false otherwise.</returns>
        bool IsMatch(Note note);
    }
}

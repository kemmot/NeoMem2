// <copyright file="NoteMatcherType.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Core.Queries
{
    /// <summary>
    /// The supported note matcher types.
    /// </summary>
    public enum NoteMatcherType
    {
        /// <summary>
        /// All notes match.
        /// </summary>
        AllMatch,

        /// <summary>
        /// Matches based on containing text.
        /// </summary>
        Contains,

        /// <summary>
        /// Matches based on containing all of multiple texts.
        /// </summary>
        ContainsAll,

        /// <summary>
        /// Matches based on containing any of multiple texts.
        /// </summary>
        ContainsAny,

        /// <summary>
        /// Matches based on a PowerShell query.
        /// </summary>
        PowerShell,

        /// <summary>
        /// Matches based on a custom query.
        /// </summary>
        Query,

        /// <summary>
        /// Matches based on a regular expression.
        /// </summary>
        Regex
    }
}

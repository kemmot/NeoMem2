// <copyright file="MultiSearchTermStrategy.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Core.Queries
{
    /// <summary>
    /// The strategies supported when matching multiple search terms.
    /// </summary>
    public enum MultiSearchTermStrategy
    {
        /// <summary>
        /// All terms must match.
        /// </summary>
        All,

        /// <summary>
        /// Any term must match.
        /// </summary>
        Any
    }
}

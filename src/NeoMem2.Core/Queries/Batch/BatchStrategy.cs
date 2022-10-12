// <copyright file="BatchStrategy.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Core.Queries.Batch
{
    /// <summary>
    /// The multi-rule batching strategy to use.
    /// </summary>
    public enum BatchStrategy
    {
        /// <summary>
        /// All rules must match.
        /// </summary>
        AllMustMatch,

        /// <summary>
        /// Any rule must match.
        /// </summary>
        AnyMustMatch
    }
}

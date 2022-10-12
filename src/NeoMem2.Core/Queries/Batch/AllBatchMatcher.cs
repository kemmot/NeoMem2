// <copyright file="AllBatchMatcher.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Core.Queries.Batch
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A multi-rule matcher in which all rules must be successful for a match.
    /// </summary>
    public class AllBatchMatcher : BatchMatcherBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AllBatchMatcher" /> class.
        /// </summary>
        /// <param name="matchers">The matchers to use.</param>
        public AllBatchMatcher(IEnumerable<INoteMatcher> matchers = null)
            : base(matchers)
        {
        }

        /// <summary>
        /// Checks if the specified note matches this rule.
        /// </summary>
        /// <param name="note">The note to check.</param>
        /// <returns>True if the note was a match; false otherwise.</returns>
        public override bool IsMatch(Note note)
        {
            return this.Matchers.All(matcher => matcher.IsMatch(note));
        }
    }
}

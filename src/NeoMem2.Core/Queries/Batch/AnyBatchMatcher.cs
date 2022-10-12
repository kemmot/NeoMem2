// <copyright file="AnyBatchMatcher.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Core.Queries.Batch
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A multi-rule matcher in which any rule must be successful for a match.
    /// </summary>
    public class AnyBatchMatcher : BatchMatcherBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AnyBatchMatcher" /> class.
        /// </summary>
        /// <param name="matchers">The matchers to use.</param>
        public AnyBatchMatcher(IEnumerable<INoteMatcher> matchers = null)
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
            return this.Matchers.Any(matcher => matcher.IsMatch(note));
        }
    }
}

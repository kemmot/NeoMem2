// <copyright file="BatchStrategyFactory.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Core.Queries.Batch
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Creates a multi-rule note matcher to satisfy the request.
    /// </summary>
    public class BatchStrategyFactory
    {
        /// <summary>
        /// Gets a multi-rule matcher.
        /// </summary>
        /// <param name="strategy">The strategy that the multi-rule matcher should employ.</param>
        /// <param name="matchers">The matchers that should be used.</param>
        /// <returns>The new matcher.</returns>
        public INoteMatcher GetStrategyMatcher(BatchStrategy strategy, List<INoteMatcher> matchers)
        {
            INoteMatcher matcher;
            switch (strategy)
            {
                case BatchStrategy.AllMustMatch:
                    matcher = new AllBatchMatcher(matchers);
                    break;
                case BatchStrategy.AnyMustMatch:
                    matcher = new AnyBatchMatcher(matchers);
                    break;
                default:
                    string message = string.Format(
                        "Batch strategy not supported: {0}",
                        strategy);
                    throw new NotSupportedException(message);
            }

            return matcher;
        }
    }
}

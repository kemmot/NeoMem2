// <copyright file="BatchMatcherBase.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Core.Queries.Batch
{
    using System.Collections.Generic;

    /// <summary>
    /// A base class to support multi-rule matchers.
    /// </summary>
    public abstract class BatchMatcherBase : INoteMatcher
    {
        /// <summary>
        /// Backing field for the <see cref="Matchers"/> property.
        /// </summary>
        private readonly List<INoteMatcher> matchers = new List<INoteMatcher>();

        /// <summary>
        /// Initializes a new instance of the <see cref="BatchMatcherBase" /> class.
        /// </summary>
        /// <param name="matchers">The matchers to use.</param>
        protected BatchMatcherBase(IEnumerable<INoteMatcher> matchers = null)
        {
            if (matchers != null)
            {
                this.matchers.AddRange(matchers);
            }
        }

        /// <summary>
        /// Gets the matchers to use.
        /// </summary>
        public List<INoteMatcher> Matchers
        {
            get { return this.matchers; }
        }

        /// <summary>
        /// Checks if the specified note matches this rule.
        /// </summary>
        /// <param name="note">The note to check.</param>
        /// <returns>True if the note was a match; false otherwise.</returns>
        public abstract bool IsMatch(Note note);
    }
}

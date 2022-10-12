// <copyright file="NoteQuery.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Core.Queries
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    using NeoMem2.Core.Queries.Batch;

    using NLog;

    /// <summary>
    /// A facade to facilitate searching notes.
    /// </summary>
    public class NoteQuery
    {
        /// <summary>
        /// The logger to use.
        /// </summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Searches notes.
        /// </summary>
        /// <param name="notes">The notes to search.</param>
        /// <param name="options">The options to use.</param>
        /// <returns>The matching notes.</returns>
        public List<Note> Search(IEnumerable<Note> notes, NoteQueryOptions options)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            List<INoteMatcher> matchers = options.SearchFields
                .Select(searchField => new NoteMatcherFactory().GetMatcher(options, searchField))
                .ToList();
            var batch = new BatchStrategyFactory().GetStrategyMatcher(options.BatchStrategy, matchers);
            var results = notes.Where(batch.IsMatch).ToList();
            stopwatch.Stop();
            Logger.Info("Search complete, found {0} notes from {1} [{2}]", results.Count, notes.Count(), stopwatch.Elapsed);
            return results;
        }
    }
}

// <copyright file="QueryMatcher.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Core.Queries
{
    using System;
    using System.Text.RegularExpressions;

    using NeoMem2.Core.Queries.Batch;
    using NeoMem2.Core.Queries.Text;

    /// <summary>
    /// A matcher that uses a custom query.
    /// </summary>
    public class QueryMatcher : INoteMatcher
    {
        /// <summary>
        /// Caches a matcher that represents the parsed query.
        /// </summary>
        private readonly INoteMatcher batch;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryMatcher" /> class.
        /// </summary>
        /// <param name="options">The query options.</param>
        public QueryMatcher(NoteQueryOptions options)
        {
            this.batch = this.ParseQuery(options);
        }

        /// <summary>
        /// Checks if the specified note matches this rule.
        /// </summary>
        /// <param name="note">The note to check.</param>
        /// <returns>True if the note was a match; false otherwise.</returns>
        public bool IsMatch(Note note)
        {
            return this.batch.IsMatch(note);
        }

        /// <summary>
        /// Parses the query into a matcher.
        /// </summary>
        /// <param name="options">The options to use.</param>
        /// <returns>The matcher for the query.</returns>
        private INoteMatcher ParseQuery(NoteQueryOptions options)
        {
            Regex queryRegex = new Regex(@"(^|;)((?<field>[\[|\]|\w]+)=)?(?<value>[^;]+)");

            AllBatchMatcher batch = new AllBatchMatcher();

            var matches = queryRegex.Matches(options.QueryText);
            if (matches.Count == 0)
            {
                string message = string.Format(
                    "No matchers were found in query: {0}",
                    options.QueryText);
                throw new Exception(message);
            }

            int matchedLength = 0;
            foreach (Match match in matches)
            {
                string field = match.Groups["field"].Value;
                string value = match.Groups["value"].Value;

                if (string.IsNullOrEmpty(field))
                {
                    field = SearchField.Name;
                }

                batch.Matchers.Add(new ContainsNoteMatcher(field, options.IsCaseSensitive, value));
                matchedLength += match.Length;
            }

            if (matchedLength < options.QueryText.Length)
            {
                string message = string.Format(
                    "Matched query length {0} is less than entire query length {1}: {2}",
                    matchedLength,
                    options.QueryText.Length,
                    options.QueryText);
                throw new Exception(message);
            }

            return batch;
        }
    }
}

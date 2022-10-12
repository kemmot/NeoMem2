// <copyright file="NoteQueryOptions.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Core.Queries
{
    using System.Collections.Generic;

    using NeoMem2.Core.Queries.Batch;

    /// <summary>
    /// The options for customizing a note query.
    /// </summary>
    public class NoteQueryOptions
    {
        /// <summary>
        /// The backing field for the <see cref="SearchFields"/> property.
        /// </summary>
        private readonly List<string> searchFields = new List<string>();

        /// <summary>
        /// Initializes a new instance of the <see cref="NoteQueryOptions" /> class.
        /// </summary>
        public NoteQueryOptions()
        {
            this.MatcherType = NoteMatcherType.Contains;
        }

        /// <summary>
        /// Gets or sets the batch strategy.
        /// </summary>
        public BatchStrategy BatchStrategy { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to highlight the matches within a note.
        /// </summary>
        public bool HighlightMatches { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to perform a case sensitive match.
        /// </summary>
        public bool IsCaseSensitive { get; set; }

        /// <summary>
        /// Gets or sets the matcher type to use.
        /// </summary>
        public NoteMatcherType MatcherType { get; set; }

        /// <summary>
        /// Gets or sets the text to look for.
        /// </summary>
        public string QueryText { get; set; }

        /// <summary>
        /// Gets the note fields to search in.
        /// </summary>
        public List<string> SearchFields
        {
            get { return this.searchFields; }
        }

        /// <summary>
        /// Gets or sets the separator to use to split the <see cref="QueryText"/> into multiple search terms.
        /// </summary>
        public string Separator { get; set; }
    }
}

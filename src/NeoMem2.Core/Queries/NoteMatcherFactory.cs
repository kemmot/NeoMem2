// <copyright file="NoteMatcherFactory.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Core.Queries
{
    using System;

    using NeoMem2.Core.Queries.Text;

    /// <summary>
    /// A factory for note matchers.
    /// </summary>
    public class NoteMatcherFactory
    {
        /// <summary>
        /// Creates a note matcher to perform the specified search.
        /// </summary>
        /// <param name="options">The search options.</param>
        /// <param name="searchField">The name of the field or fields to search.</param>
        /// <returns>The matcher.</returns>
        public INoteMatcher GetMatcher(NoteQueryOptions options, string searchField)
        {
            INoteMatcher matcher;
            switch (options.MatcherType)
            {
                case NoteMatcherType.AllMatch:
                    matcher = new AllMatchNoteMatcher();
                    break;
                case NoteMatcherType.Contains:
                    matcher = new ContainsNoteMatcher(searchField, options.IsCaseSensitive, options.QueryText);
                    break;
                case NoteMatcherType.ContainsAll:
                    matcher = new ContainsAllNoteMatcher(searchField, options.IsCaseSensitive, options.QueryText, options.Separator);
                    break;
                case NoteMatcherType.ContainsAny:
                    matcher = new ContainsAnyNoteMatcher(searchField, options.IsCaseSensitive, options.QueryText, options.Separator);
                    break;
                case NoteMatcherType.PowerShell:
                    matcher = new PowerShellMatcher(options.QueryText);
                    break;
                case NoteMatcherType.Query:
                    matcher = new QueryMatcher(options);
                    break;
                case NoteMatcherType.Regex:
                    matcher = new RegexNoteMatcher(searchField, options.IsCaseSensitive, options.QueryText);
                    break;
                default:
                    string message = string.Format(
                        "Note matcher type not supported: {0}",
                        options.MatcherType);
                    throw new NotSupportedException(message);
            }

            return matcher;
        }
    }
}

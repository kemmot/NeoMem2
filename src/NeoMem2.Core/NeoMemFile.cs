// <copyright file="NeoMemFile.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Core
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents an entire database file.
    /// </summary>
    public class NeoMemFile
    {
        /// <summary>
        /// All notes found in the file.
        /// </summary>
        public readonly NoteView AllNotes = new NoteView();

        /// <summary>
        /// All of the different columns found in the file.
        /// </summary>
        public readonly List<string> ColumnNames = new List<string>();

        /// <summary>
        /// The root nodes of the file.
        /// </summary>
        public readonly List<Note> RootNotes = new List<Note>();
    }
}

// <copyright file="NeoMemFlatFileWriter.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Core
{
    using System.Collections.Generic;

    using NeoMem2.Core.Stores;

    /// <summary>
    /// An implementation of <see cref="IExporter"/> that exports each note to a flat file in a specified folder.
    /// </summary>
    public class NeoMemFlatFileWriter : ExporterBase
    {
        /// <summary>
        /// The backing field for the <see cref="ConnectionString"/> property.
        /// </summary>
        private string connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="NeoMemFlatFileWriter" /> class.
        /// </summary>
        /// <param name="folder">The fully qualified path to the folder to export to.</param>
        public NeoMemFlatFileWriter(string folder)
        {
            this.Store = new FlatFileStore(folder);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NeoMemFlatFileWriter" /> class.
        /// </summary>
        public NeoMemFlatFileWriter()
        {
        }

        /// <summary>
        /// Gets or sets the fully qualified path to the folder to export to.
        /// </summary>
        public override string ConnectionString
        {
            get
            {
                return this.connectionString;
            }

            set
            {
                this.connectionString = value;
                this.Store = new FlatFileStore(this.connectionString);
            }
        }

        /// <summary>
        /// Gets or sets the note store to use.
        /// </summary>
        private FlatFileStore Store { get; set; }

        /// <summary>
        /// Exports all notes from the specified file.
        /// </summary>
        /// <param name="file">The file of notes to export.</param>
        public override void Export(NeoMemFile file)
        {
            this.Export(file.RootNotes);
        }

        /// <summary>
        /// Exports the specified notes.
        /// </summary>
        /// <param name="notes">The notes to export.</param>
        private void Export(IEnumerable<Note> notes)
        {
            foreach (Note note in notes)
            {
                this.Export(note);
            }
        }

        /// <summary>
        /// Exports the specified note.
        /// </summary>
        /// <param name="note">The note to export.</param>
        private void Export(Note note)
        {
            this.Store.Save(note);

            if (note.Children.Count > 0)
            {
                this.Export(note.Children);
            }
        }
    }
}

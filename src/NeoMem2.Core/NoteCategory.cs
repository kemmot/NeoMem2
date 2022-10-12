// <copyright file="NoteCategory.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Core
{
    using System.Collections.Generic;

    /// <summary>
    /// A category containing related notes.
    /// </summary>
    public class NoteCategory
    {
        /// <summary>
        /// The backing field for the <see cref="Notes"/> property.
        /// </summary>
        private readonly List<Note> notes = new List<Note>();

        /// <summary>
        /// The backing field for the <see cref="SubCategories"/> property.
        /// </summary>
        private readonly List<NoteCategory> subCategories = new List<NoteCategory>();

        /// <summary>
        /// Initializes a new instance of the <see cref="NoteCategory" /> class.
        /// </summary>
        /// <param name="name">The name of this category.</param>
        /// <param name="notes">Another category that specifies the notes in this category.</param>
        public NoteCategory(string name, NoteCategory notes)
            : this(name, notes.Notes)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NoteCategory" /> class.
        /// </summary>
        /// <param name="name">The name of this category.</param>
        /// <param name="notes">The notes in this category.</param>
        public NoteCategory(string name, IEnumerable<Note> notes = null)
            : this()
        {
            this.Name = name;
            if (notes != null)
            {
                this.Notes.AddRange(notes);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NoteCategory" /> class.
        /// </summary>
        public NoteCategory()
        {
        }

        /// <summary>
        /// Gets or sets the name of this category.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the notes in this category.
        /// </summary>
        public List<Note> Notes
        {
            get { return this.notes; }
        }

        /// <summary>
        /// Gets the sub categories in this category.
        /// </summary>
        public List<NoteCategory> SubCategories
        {
            get { return this.subCategories; }
        }

        /// <summary>
        /// Gets any pinned notes from this category.
        /// </summary>
        /// <returns>The pinned notes.</returns>
        public NoteCategory GetPinned()
        {
            NoteCategory pinnedNotes = new NoteCategory { Name = "Pinned" };
            foreach (Note note in this.Notes)
            {
                if (note.IsPinned)
                {
                    pinnedNotes.Notes.Add(note);
                }
            }

            return pinnedNotes;
        }
    }
}

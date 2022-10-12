// <copyright file="NoteLink.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Core
{
    using System;
    using NeoMem2.Utils.ComponentModel;
    using NeoMem2.Utils.ComponentModel.Generic;

    /// <summary>
    /// Represents a link between two <see cref="Note"/> objects.
    /// </summary>
    public class NoteLink : ChangeTrackedBase
    {
        /// <summary>
        /// The name of the <see cref="IsDeleted"/> property.
        /// </summary>
        public const string PropertyNameIsDeleted = "IsDeleted";

        /// <summary>
        /// The backing field for the <see cref="IsDeleted"/> property.
        /// </summary>
        private readonly TrackedProperty<bool> isDeleted = new TrackedProperty<bool>(PropertyNameIsDeleted);

        /// <summary>
        /// The backing field for the <see cref="Note1"/> property.
        /// </summary>
        private readonly Note note1;

        /// <summary>
        /// The backing field for the <see cref="Note2"/> property.
        /// </summary>
        private readonly Note note2;

        /// <summary>
        /// Initializes a new instance of the <see cref="NoteLink" /> class.
        /// </summary>
        /// <param name="note1">The first note in the link.</param>
        /// <param name="note2">The second note in the link.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if a required argument has a null reference.
        /// </exception>
        public NoteLink(Note note1, Note note2)
            : base("NoteLink")
        {
            if (note1 == null)
            {
                throw new ArgumentNullException("note1");
            }

            if (note2 == null)
            {
                throw new ArgumentNullException("note2");
            }

            this.note1 = note1;
            this.note2 = note2;

            this.PropertyHolder.Register(this.isDeleted);
        }

        /// <summary>
        /// Gets or sets the unique identifier for this link.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this link has been deleted.
        /// </summary>
        public bool IsDeleted
        {
            get { return this.isDeleted; } 
            set { this.isDeleted.Value = value; }
        }

        /// <summary>
        /// Gets the first note in the link.
        /// </summary>
        public Note Note1
        {
            get { return this.note1; }
        }

        /// <summary>
        /// Gets the second note in the link.
        /// </summary>
        public Note Note2
        {
            get { return this.note2; }
        }

        /// <summary>
        /// Detaches this from its original storage so that it appears like a new note to be imported into another store.
        /// </summary>
        public void Detach()
        {
            this.Id = 0;
            this.IsDeleted = false;
        }
    }
}

// <copyright file="NoteTag.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Core
{
    using NeoMem2.Utils.ComponentModel;
    using NeoMem2.Utils.ComponentModel.Generic;

    /// <summary>
    /// Represents a <see cref="Tag"/> assignment to a <see cref="Note"/>.
    /// </summary>
    public class NoteTag : ChangeTrackedBase
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
        /// Initializes a new instance of the <see cref="NoteTag" /> class.
        /// </summary>
        /// <param name="note">The note that the tag is assigned to.</param>
        /// <param name="tag">The tag that is assigned to the note.</param>
        public NoteTag(Note note, Tag tag)
        {
            this.Note = note;
            this.Tag = tag;

            this.IsNew = true;

            this.PropertyHolder.Register(this.isDeleted);
            this.PropertyHolder.Register(this.Tag);
        }

        /// <summary>
        /// Gets or sets a value indicating whether this note tag allocation has been deleted.
        /// </summary>
        public bool IsDeleted
        {
            get { return this.isDeleted; } 
            set { this.isDeleted.Value = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this note tag allocation is new.
        /// </summary>
        public bool IsNew { get; set; }

        /// <summary>
        /// Gets the note that the tag is assigned to.
        /// </summary>
        public Note Note { get; private set; }

        /// <summary>
        /// Gets the tag that is assigned to the note.
        /// </summary>
        public Tag Tag { get; private set; }


        /// <summary>
        /// Detaches its original storage so that it appears like a new note to be imported into another store.
        /// </summary>
        public void Detach()
        {
            if (!this.IsNew)
            {
                this.IsNew = true;                
                this.Note.Detach();
                this.Tag.Detach();
            }
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return this.Tag.ToString();
        }
    }
}
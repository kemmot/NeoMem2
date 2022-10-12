// <copyright file="Attachment.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Core
{
    using NeoMem2.Utils.ComponentModel;
    using NeoMem2.Utils.ComponentModel.Generic;

    /// <summary>
    /// A note attachment.
    /// </summary>
    public class Attachment : ChangeTrackedBase
    {
        /// <summary>
        /// The name of the <see cref="IsDeleted"/> property for use with the <see cref="NotifyPropertyChangedBase.PropertyChanged"/> event.
        /// </summary>
        public const string PropertyNameIsDeleted = "IsDeleted";

        /// <summary>
        /// The backing field for the <see cref="IsDeleted"/> property.
        /// </summary>
        private readonly TrackedProperty<bool> isDeleted = new TrackedProperty<bool>(PropertyNameIsDeleted);
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Attachment" /> class.
        /// </summary>
        public Attachment()
        {
            PropertyHolder.Register(this.isDeleted);
        }

        /// <summary>
        /// Gets or sets the data associated with this attachment.
        /// </summary>
        public byte[] Data { get; set; }

        /// <summary>
        /// Gets or sets the name of the file that this attachment came from.
        /// </summary>
        public string Filename { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this attachment has any unloaded data associated with it.
        /// </summary>
        public bool HasUnloadedData { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of this attachment.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this attachment has been deleted.
        /// </summary>
        public bool IsDeleted
        {
            get { return this.isDeleted; } 
            set { this.isDeleted.Value = value; }
        }

        /// <summary>
        /// Gets or sets the note that this attachment is attached to.
        /// </summary>
        public Note Note { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of this note that this attachment is attached to.
        /// </summary>
        public long NoteId { get; set; }

        /// <summary>
        /// Gets or sets the length in bytes, before compression, of data in the attachment.
        /// </summary>
        public long UncompressedDataLength { get; set; }

        /// <summary>
        /// Detaches from its original storage so that it appears like a new note to be imported into another store.
        /// </summary>
        public void Detach()
        {
            this.Id = 0;
            ClearIsDirty();
        }
    }
}
// <copyright file="NeoMemStoreBase.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Core.Stores
{
    using System;
    using System.Collections.Generic;

    using NeoMem2.Automation.Updates;

    /// <summary>
    /// A base class implementation of <see cref="INeoMemStore"/> that provides functionality common to all implementations.
    /// </summary>
    public class NeoMemStoreBase : INeoMemStore
    {
        /// <summary>
        /// Backs up the store to the specified file.
        /// </summary>
        /// <param name="backupFile">The file to backup to.</param>
        public virtual void Backup(string backupFile)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a new note.
        /// </summary>
        /// <returns>The new note.</returns>
        public virtual Note CreateNewNote()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a new store of the type managed by this class.
        /// </summary>
        /// <param name="recreate">Set to true to remove and recreate it if it already exists.</param>
        public virtual void CreateNewStore(bool recreate = false)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the specified note.
        /// </summary>
        /// <param name="note">The note to delete.</param>
        public virtual void Delete(Note note)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets all attachments from the store.
        /// </summary>
        /// <returns>The attachments.</returns>
        public virtual IEnumerable<Attachment> GetAttachments()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the saved history of the specified note.
        /// </summary>
        /// <param name="note">The note to get the history for.</param>
        /// <returns>The history of changes.</returns>
        public virtual List<NoteChange> GetNoteHistory(Note note)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets all notes from the store.
        /// </summary>
        /// <returns>The notes.</returns>
        public virtual INoteView GetNotes()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets all properties from the store.
        /// </summary>
        /// <returns>The properties.</returns>
        public virtual IEnumerable<Property> GetProperties()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets all tags from the store.
        /// </summary>
        /// <returns>The tags.</returns>
        public virtual List<Tag> GetTags()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets any updates that are applicable to bring this store up to the latest version.
        /// </summary>
        /// <returns>The applicable updates.</returns>
        public virtual List<Tuple<ComponentInfo, UpdateContext>> GetUpdates()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Populates the note with any child notes that are nested underneath it.
        /// </summary>
        /// <param name="note">The note to populate.</param>
        public virtual void PopulateChildren(Note note)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Saves any changes to the specified note.
        /// </summary>
        /// <param name="note">The note to save.</param>
        public virtual void Save(Note note)
        {
            throw new NotImplementedException();
        }
    }
}

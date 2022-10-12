// <copyright file="INeoMemStore.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Core.Stores
{
    using System;
    using System.Collections.Generic;

    using NeoMem2.Automation.Updates;

    /// <summary>
    /// The interface that must be implemented to provide note storage functionality.
    /// </summary>
    public interface INeoMemStore
    {
        /// <summary>
        /// Backs up the store to the specified file.
        /// </summary>
        /// <param name="backupFile">The file to backup to.</param>
        void Backup(string backupFile);

        /// <summary>
        /// Creates a new note.
        /// </summary>
        /// <returns>The new note.</returns>
        Note CreateNewNote();

        /// <summary>
        /// Creates a new store of the type managed by this class.
        /// </summary>
        /// <param name="recreate">Set to true to remove and recreate it if it already exists.</param>
        void CreateNewStore(bool recreate = false);

        /// <summary>
        /// Deletes the specified note.
        /// </summary>
        /// <param name="note">The note to delete.</param>
        void Delete(Note note);

        /// <summary>
        /// Gets all attachments from the store.
        /// </summary>
        /// <returns>The attachments.</returns>
        IEnumerable<Attachment> GetAttachments();

        /// <summary>
        /// Gets the saved history of the specified note.
        /// </summary>
        /// <param name="note">The note to get the history for.</param>
        /// <returns>The history of changes.</returns>
        List<NoteChange> GetNoteHistory(Note note);

        /// <summary>
        /// Gets all notes from the store.
        /// </summary>
        /// <returns>The notes.</returns>
        INoteView GetNotes();

        /// <summary>
        /// Gets all properties from the store.
        /// </summary>
        /// <returns>The properties.</returns>
        IEnumerable<Property> GetProperties();

        /// <summary>
        /// Gets all tags from the store.
        /// </summary>
        /// <returns>The tags.</returns>
        List<Tag> GetTags();

        /// <summary>
        /// Gets any updates that are applicable to bring this store up to the latest version.
        /// </summary>
        /// <returns>The applicable updates.</returns>
        List<Tuple<ComponentInfo, UpdateContext>> GetUpdates();

        /// <summary>
        /// Populates the note with any child notes that are nested underneath it.
        /// </summary>
        /// <param name="note">The note to populate.</param>
        void PopulateChildren(Note note);

        /// <summary>
        /// Saves any changes to the specified note.
        /// </summary>
        /// <param name="note">The note to save.</param>
        void Save(Note note);
    }
}

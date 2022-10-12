// <copyright file="AdoStoreBase.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;

    using NeoMem2.Core;
    using NeoMem2.Core.Stores;
    using NeoMem2.Data.FluentSql;
    using NeoMem2.Utils.Diagnostics;

    using NLog;

    /// <summary>
    /// A base class for stores that use ADO for storage.
    /// </summary>
    public class AdoStoreBase : NeoMemStoreBase
    {
        /// <summary>
        /// The logger to use in this class.
        /// </summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The object to use for <see cref="Attachment"/> data access.
        /// </summary>
        private readonly IObjectDataAccess<Attachment> attachmentDataAccess;

        /// <summary>
        /// The object to use for <see cref="Note"/> data access.
        /// </summary>
        private readonly IObjectDataAccess<Note> noteDataAccess;

        /// <summary>
        /// The object to use for <see cref="NoteLink"/> data access.
        /// </summary>
        private readonly IObjectDataAccess<NoteLink> noteLinkDataAccess;

        /// <summary>
        /// The object to use for <see cref="NoteTag"/> data access.
        /// </summary>
        private readonly IObjectDataAccess<NoteTag> noteTagDataAccess;

        /// <summary>
        /// The object to use for <see cref="Property"/> data access.
        /// </summary>
        private readonly IObjectDataAccess<Property> propertyDataAccess;

        /// <summary>
        /// The object to use for <see cref="Tag"/> data access.
        /// </summary>
        private readonly IObjectDataAccess<Tag> tagDataAccess;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlServerStore" /> class.
        /// </summary>
        /// <param name="connectionString">The connection string to use to connect to the database.</param>
        /// <exception cref="ArgumentException">
        /// Thrown if a required argument has an invalid value.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if a required argument has a null reference.
        /// </exception>
        protected AdoStoreBase(IAdoDataAccess dataAccess)
        {
            if (dataAccess == null)
            {
                throw new ArgumentNullException("dataAccess");
            }

            this.DataAccess = dataAccess;
            this.attachmentDataAccess = CreateAttachmentDataAccess(dataAccess);
            this.noteDataAccess = CreateNoteDataAccess(dataAccess);
            this.noteLinkDataAccess = CreateNoteLinkDataAccess(dataAccess);
            this.noteTagDataAccess = CreateNoteTagDataAccess(dataAccess);
            this.propertyDataAccess = CreatePropertyDataAccess(dataAccess);
            this.tagDataAccess = CreateTagDataAccess(dataAccess);
        }

        public IAdoDataAccess DataAccess { get; }

        /// <summary>
        /// Creates a new note.
        /// </summary>
        /// <returns>The new note.</returns>
        public override Note CreateNewNote()
        {
            return new Note();
        }

        /// <summary>
        /// Deletes the specified note.
        /// </summary>
        /// <param name="note">The note to delete.</param>
        public override void Delete(Note note)
        {
            foreach (Property property in note.Properties)
            {
                this.propertyDataAccess.Delete(property);
            }

            foreach (NoteLink noteLink in note.LinkedNotes)
            {
                this.noteLinkDataAccess.Delete(noteLink);
            }

            using (var command = this.DataAccess.CreateCommand("DELETE FROM Note WHERE Id = @Id"))
            {
                command.CommandType = CommandType.Text;
                command.Parameters.Add(this.DataAccess.CreateParameter("Id", note.Id));

                this.DataAccess.ExecuteNonQuery(command);
            }
        }

        /// <summary>
        /// Gets all attachments from the store.
        /// </summary>
        /// <returns>The attachments.</returns>
        public override IEnumerable<Attachment> GetAttachments()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            using (var connection = this.DataAccess.CreateConnection())
            {
                connection.Open();
                foreach (Attachment attachment in this.GetAttachments(connection))
                {
                    yield return attachment;
                }

                connection.Close();
            }

            stopwatch.Stop();
            Statistics.Instance.RecordQuery(stopwatch.Elapsed);
        }

        /// <summary>
        /// Gets all notes from the store.
        /// </summary>
        /// <returns>The notes.</returns>
        public override INoteView GetNotes()
        {
            Dictionary<long, Note> notes;
            Stopwatch stopwatch = Stopwatch.StartNew();
            using (var connection = this.DataAccess.CreateConnection())
            {
                connection.Open();
                notes = this.GetNotesInternal(connection);
                this.PopulateProperties(connection, notes);
                this.PopulateNoteLinks(connection, notes);
                this.PopulateAttachments(connection, notes);
                this.PopulateTags(connection, notes);
                connection.Close();
            }

            stopwatch.Stop();
            Statistics.Instance.RecordQuery(stopwatch.Elapsed);

            return new NoteView(notes.Values);
        }

        /// <summary>
        /// Gets all properties from the store.
        /// </summary>
        /// <returns>The properties.</returns>
        public override IEnumerable<Property> GetProperties()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            using (var connection = this.DataAccess.CreateConnection())
            {
                connection.Open();
                foreach (Property property in this.GetProperties(connection))
                {
                    yield return property;
                }

                connection.Close();
            }

            stopwatch.Stop();
            Statistics.Instance.RecordQuery(stopwatch.Elapsed);
        }

        /// <summary>
        /// Gets all tags from the store.
        /// </summary>
        /// <returns>The tags.</returns>
        public override List<Tag> GetTags()
        {
            List<Tag> tags;
            using (var connection = this.DataAccess.CreateConnection())
            {
                connection.Open();
                tags = this.GetTagsInternal(connection);
            }

            return tags;
        }

        /// <summary>
        /// Populates the note with any child notes that are nested underneath it.
        /// </summary>
        /// <param name="note">The note to populate.</param>
        public override void PopulateChildren(Note note)
        {
            throw new NotSupportedException("Hierarchical notes are not supported by this provider");
        }

        /// <summary>
        /// Saves any changes to the specified note.
        /// </summary>
        /// <param name="note">The note to save.</param>
        public override void Save(Note note)
        {
            using (NestedDiagnosticsContext.Push("Saving note " + note.Id))
            {
                this.noteDataAccess.Save(note);

                foreach (var property in note.AllProperties)
                {
                    this.propertyDataAccess.Save(property);
                }

                for (int noteLinkIndex = note.LinkedNotes.Count - 1; noteLinkIndex >= 0; noteLinkIndex--)
                {
                    NoteLink noteLink = note.LinkedNotes[noteLinkIndex];
                    Note otherNote = noteLink.Note1 == note ? noteLink.Note2 : noteLink.Note1;
                    this.noteDataAccess.Save(otherNote);

                    this.noteLinkDataAccess.Save(noteLink);

                    if (noteLink.IsDeleted)
                    {
                        note.RemoveLinkedNote(noteLink);
                        otherNote.RemoveLinkedNote(noteLink);
                    }
                }

                for (int attachmentIndex = note.Attachments.Count - 1; attachmentIndex >= 0; attachmentIndex--)
                {
                    var attachment = note.Attachments[attachmentIndex];
                    this.attachmentDataAccess.Save(attachment);

                    if (attachment.IsDeleted)
                    {
                        note.Attachments.RemoveAt(attachmentIndex);
                    }
                }

                for (int tagIndex = note.Tags.Count - 1; tagIndex >= 0; tagIndex--)
                {
                    var noteTag = note.Tags[tagIndex];
                    this.tagDataAccess.Save(noteTag.Tag);
                    this.noteTagDataAccess.Save(noteTag);

                    if (noteTag.IsDeleted)
                    {
                        note.Tags.RemoveAt(tagIndex);
                    }
                }
            }
        }

        /// <summary>
        /// Gets the saved history of the specified note.
        /// </summary>
        /// <param name="note">The note to get the history for.</param>
        /// <returns>The history of changes.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if a required argument has a null reference.
        /// </exception>
        public override List<NoteChange> GetNoteHistory(Note note)
        {
            if (note == null)
            {
                throw new ArgumentNullException("note");
            }

            const string Sql = "SELECT Id, ChangeDate, ChangeTypeId, FieldName, Value, ValueString FROM NoteHistory WHERE NoteId = @NoteId ORDER BY ChangeDate DESC";

            List<NoteChange> noteChanges = new List<NoteChange>();
            using (var connection = this.DataAccess.CreateConnection())
            {
                using (var command = this.DataAccess.CreateCommand(Sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add(this.DataAccess.CreateParameter("NoteId", note.Id));
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            noteChanges.Add(new NoteChange
                            {
                                ChangeDate = reader.GetDateTime(reader.GetOrdinal("ChangeDate")),
                                ChangeType = (NoteChangeType)reader.GetInt32(reader.GetOrdinal("ChangeTypeId")),
                                FieldName = reader.GetString(reader.GetOrdinal("FieldName")),
                                Note = note,
                                Value = reader.GetString(reader.GetOrdinal("Value")),
                                ValueString = reader.GetString(reader.GetOrdinal("ValueString"))
                            });
                        }
                    }
                }
            }

            return noteChanges;
        }

        protected virtual IObjectDataAccess<Attachment> CreateAttachmentDataAccess(IAdoDataAccess dataAccess)
        {
            return new AdoAttachmentDataAccess(dataAccess);
        }

        protected virtual IObjectDataAccess<Note> CreateNoteDataAccess(IAdoDataAccess dataAccess)
        {
            return new AdoNoteDataAccess(dataAccess);
        }

        protected virtual IObjectDataAccess<NoteLink> CreateNoteLinkDataAccess(IAdoDataAccess dataAccess)
        {
            return new AdoNoteLinkDataAccess(dataAccess);
        }

        protected virtual IObjectDataAccess<NoteTag> CreateNoteTagDataAccess(IAdoDataAccess dataAccess)
        {
            return new AdoNoteTagDataAccess(dataAccess);
        }

        protected virtual IObjectDataAccess<Property> CreatePropertyDataAccess(IAdoDataAccess dataAccess)
        {
            return new AdoPropertyDataAccess(dataAccess);
        }

        protected virtual IObjectDataAccess<Tag> CreateTagDataAccess(IAdoDataAccess dataAccess)
        {
            return new AdoTagDataAccess(dataAccess);
        }

        /// <summary>
        /// Gets all notes from the store.
        /// </summary>
        /// <param name="connection">The connection to use.</param>
        /// <returns>The notes.</returns>
        private Dictionary<long, Note> GetNotesInternal(IDbConnection connection)
        {
            var notes = new Dictionary<long, Note>();
            using (var command = SqlStatementBuilder.Select().AllColumns().From("Note").GetCommand(this.DataAccess))
            {
                command.Connection = connection;
                command.CommandType = CommandType.Text;

                string sql = this.DataAccess.GetCommandDescription(command);
                var stopwatch = Stopwatch.StartNew();
                try
                {
                    Logger.Trace("About to execute command: {0}", sql);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            long id = reader.GetReferenceValue<long>("Id");
                            notes.Add(
                                id,
                                new Note
                                {
                                    Id = id,
                                    Class = reader.GetReferenceValue<string>("Class"),
                                    CreatedDate = reader.GetReferenceValue<DateTime>("CreatedDate"),
                                    DeletedDate = reader.GetReferenceValue<DateTime?>("DeletedDate"),
                                    LastAccessedDate = reader.GetReferenceValue<DateTime>("LastAccessedDate"),
                                    LastModifiedDate = reader.GetReferenceValue<DateTime>("LastModifiedDate"),
                                    Text = reader.GetReferenceValue<string>("Text"),
                                    IsPinned = reader.GetReferenceValue<bool>("IsPinned"),
                                    Name = reader.GetReferenceValue<string>("Name"),
                                    Namespace = reader.GetReferenceValue<string>("Namespace"),
                                    TextFormat = (TextFormat)reader.GetReferenceValue<int>("TextFormat"),
                                    FormattedText = reader.GetReferenceValue<string>("FormattedText")
                                });
                        }
                    }

                    Logger.Debug("Executed command {0} [{1}]", sql, stopwatch.Elapsed);
                }
                catch (Exception ex)
                {
                    stopwatch.Stop();

                    string message = string.Format(
                        "Failed to execute command: {0} [{1}]",
                        sql,
                        stopwatch.Elapsed);
                    throw new Exception(message, ex);
                }
            }

            return notes;
        }

        /// <summary>
        /// Populates notes with the relevant properties.
        /// </summary>
        /// <param name="connection">The connection to use.</param>
        /// <param name="notes">The notes to populate.</param>
        private void PopulateProperties(IDbConnection connection, Dictionary<long, Note> notes)
        {
            int propertyCount = 0;
            foreach (Property property in this.GetProperties(connection))
            {
                notes[property.NoteId].AddProperty(property);
                property.ClearIsDirty();
                propertyCount++;
            }

            Logger.Trace("Populated {0} properties", propertyCount);
        }

        /// <summary>
        /// Gets all attachments from the store.
        /// </summary>
        /// <param name="connection">The connection to use.</param>
        /// <returns>The attachments.</returns>
        private IEnumerable<Property> GetProperties(IDbConnection connection)
        {
            string sql = "SELECT * FROM Property";
            using (var command = this.DataAccess.CreateCommand(sql, connection))
            {
                command.CommandType = CommandType.Text;
                Logger.Trace("About to execute command: {0}", sql);
                var stopwatch = Stopwatch.StartNew();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        yield return new Property(
                            reader.GetInt64(reader.GetOrdinal("Id")),
                            reader.GetString(reader.GetOrdinal("Name")),
                            reader["Value"],
                            reader.GetBoolean(reader.GetOrdinal("IsSystemProperty")))
                        {
                            ClrDataType = reader.GetString(reader.GetOrdinal("ClrDataType")),
                            NoteId = reader.GetInt64(reader.GetOrdinal("NoteId"))
                        };
                    }
                }

                stopwatch.Stop();
                Logger.Debug("Executed command {0} [{1}]", sql, stopwatch.Elapsed);
            }
        }

        /// <summary>
        /// Populates notes with the relevant links.
        /// </summary>
        /// <param name="connection">The connection to use.</param>
        /// <param name="notes">The notes to populate.</param>
        private void PopulateNoteLinks(IDbConnection connection, Dictionary<long, Note> notes)
        {
            using (var command = this.DataAccess.CreateCommand("SELECT * FROM NoteLink", connection))
            {
                command.CommandType = CommandType.Text;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        long id = reader.GetInt64(reader.GetOrdinal("Id"));
                        long note1Id = reader.GetInt64(reader.GetOrdinal("Note1Id"));
                        long note2Id = reader.GetInt64(reader.GetOrdinal("Note2Id"));

                        Note note1 = notes[note1Id];
                        Note note2 = notes[note2Id];
                        var noteLink = new NoteLink(note1, note2) { Id = id };
                        noteLink.ClearIsDirty();
                        note1.AddLinkedNote(noteLink);
                        note2.AddLinkedNote(noteLink);
                    }
                }
            }
        }

        /// <summary>
        /// Populates notes with the relevant attachments.
        /// </summary>
        /// <param name="connection">The connection to use.</param>
        /// <param name="notes">The notes to populate.</param>
        private void PopulateAttachments(IDbConnection connection, Dictionary<long, Note> notes)
        {
            int count = 0;
            int populatedCount = 0;
            foreach (Attachment attachment in this.GetAttachments(connection))
            {
                Note note;
                if (notes.TryGetValue(attachment.NoteId, out note))
                {
                    attachment.Note = note;
                    populatedCount++;
                }

                count++;
            }

            Logger.Trace("Populated {0}/{1} attachments", populatedCount, count);
        }

        /// <summary>
        /// Gets all attachments from the store.
        /// </summary>
        /// <param name="connection">The connection to use.</param>
        /// <returns>The attachments.</returns>
        private IEnumerable<Attachment> GetAttachments(IDbConnection connection)
        {
            string sql = "SELECT Id, NoteId, Filename, CASE WHEN Data IS NULL THEN 0 ELSE DATALENGTH(Data) END AS StoredDataLength, DataLength FROM Attachment";
            using (var command = this.DataAccess.CreateCommand(sql, connection))
            {
                command.CommandType = CommandType.Text;
                Logger.Trace("About to execute command: {0}", sql);
                var stopwatch = Stopwatch.StartNew();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        yield return new Attachment
                        {
                            Id = reader.GetInt64(reader.GetOrdinal("Id")),
                            Filename = reader.GetString(reader.GetOrdinal("Filename")),
                            HasUnloadedData = reader.GetInt64(reader.GetOrdinal("StoredDataLength")) > 0,
                            NoteId = reader.GetInt64(reader.GetOrdinal("NoteId")),
                            UncompressedDataLength = reader.GetInt64(reader.GetOrdinal("DataLength"))
                        };
                    }
                }

                stopwatch.Stop();
                Logger.Debug("Executed command {0} [{1}]", sql, stopwatch.Elapsed);
            }
        }

        /// <summary>
        /// Populates notes with the relevant tags.
        /// </summary>
        /// <param name="connection">The connection to use.</param>
        /// <param name="notes">The notes to populate.</param>
        private void PopulateTags(IDbConnection connection, Dictionary<long, Note> notes)
        {
            var tags = new Dictionary<int, Tag>();
            foreach (var tag in this.GetTagsInternal(connection))
            {
                tags[tag.Id] = tag;
            }

            using (var command = this.DataAccess.CreateCommand("SELECT * FROM NoteTag", connection))
            {
                command.CommandType = CommandType.Text;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        long noteId = reader.GetInt64(reader.GetOrdinal("NoteId"));
                        int tagId = reader.GetInt32(reader.GetOrdinal("TagId"));

                        Tag tag;
                        if (tags.TryGetValue(tagId, out tag))
                        {
                            Note note;
                            if (notes.TryGetValue(noteId, out note))
                            {
                                var noteTag = new NoteTag(note, tag) { IsNew = false };
                                note.Tags.Add(noteTag);
                                noteTag.ClearIsDirty();
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets the available tags.
        /// </summary>
        /// <param name="connection">The connection to use.</param>
        /// <returns>The tags.</returns>
        private List<Tag> GetTagsInternal(IDbConnection connection)
        {
            var tags = new List<Tag>();
            string sql = "SELECT * FROM Tag ORDER BY Name";
            using (var command = this.DataAccess.CreateCommand(sql, connection))
            {
                command.CommandType = CommandType.Text;
                Logger.Trace("About to execute command: {0}", sql);
                var stopwatch = Stopwatch.StartNew();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(reader.GetOrdinal("Id"));
                        string name = reader.GetString(reader.GetOrdinal("Name"));
                        var tag = new Tag(id, name);
                        tag.ClearIsDirty();
                        tags.Add(tag);
                    }
                }

                stopwatch.Stop();
                Logger.Debug("Executed command {0} [{1}]", sql, stopwatch.Elapsed);
            }

            return tags;
        }
    }
}

// <copyright file="AdoNoteDataAccess.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Data
{
    using System;
    using System.Diagnostics;
    using System.Text;

    using NeoMem2.Core;
    using NeoMem2.Data.FluentSql;
    using NeoMem2.Utils.Diagnostics;
    
    /// <summary>
    /// Provides data access functionality related to <see cref="Note"/> objects.
    /// </summary>
    public class AdoNoteDataAccess : AdoDataAccessBase<Note>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdoNoteDataAccess" /> class.
        /// </summary>
        /// <param name="adoDataAccess">The data access object to use.</param>
        public AdoNoteDataAccess(IAdoDataAccess adoDataAccess)
            : base(adoDataAccess)
        {
        }

        /// <summary>
        /// Deletes the note.
        /// </summary>
        /// <param name="note">The note to delete.</param>
        /// <param name="context">The database context.</param>
        public override void Delete(Note note, AdoContext context)
        {
            throw new NotSupportedException("Irrecoverable deletion of notes is not supported");
        }

        /// <summary>
        /// Inserts the note.
        /// </summary>
        /// <param name="note">The note to insert.</param>
        /// <param name="context">The database context.</param>
        protected override void Insert(Note note, AdoContext context)
        {
            var providerStatementBuilder = this.AdoDataAccess.GetProviderSqlStatementBuilder();

            var insertBuilder = SqlStatementBuilder.Insert("Note")
                .AddColumn("Class", note.Class, true)
                .AddColumn("CreatedDate", providerStatementBuilder.DateFunction(), isFunction: true)
                .AddColumn("LastAccessedDate", providerStatementBuilder.DateFunction(), isFunction: true)
                .AddColumn("LastModifiedDate", providerStatementBuilder.DateFunction(), isFunction: true)
                .AddColumn("Text", note.Text, true)
                .AddColumn("IsPinned", note.IsPinned, true)
                .AddColumn("Name", note.Name, true)
                .AddColumn("Namespace", note.Namespace, true)
                .AddColumn("TextFormat", (int)note.TextFormat, true)
                .AddColumn("FormattedText", note.FormattedText, true)
                .AddColumn("DeletedDate", note.DeletedDate.HasValue ? (object)note.DeletedDate : DBNull.Value, true);

            var selectBuilder = SqlStatementBuilder.Select().Column(providerStatementBuilder.IdentityFunction(), isFunction:true);
            
            Stopwatch stopwatch = new Stopwatch();
            using (var command = insertBuilder.GetCommand(this.AdoDataAccess))
            {
                command.CommandText += ";" + selectBuilder.ToString();
                stopwatch.Start();
                note.Id = Convert.ToInt64(AdoDataAccess.ExecuteScalar(command, context));
                stopwatch.Stop();
            }

            Statistics.Instance.RecordInsertion(stopwatch.Elapsed);

            this.InsertChangeHistoryInsertion(note);
        }

        /// <summary>
        /// Gets a value indicating whether the note has been deleted.
        /// </summary>
        /// <param name="note">The note to check.</param>
        /// <returns>True if the note has been deleted; false otherwise.</returns>
        protected override bool IsDeleted(Note note)
        {
            return false;
        }

        /// <summary>
        /// Gets a value indicating whether the note's state has changed since it was last saved.
        /// </summary>
        /// <param name="note">The note to check.</param>
        /// <returns>True if the note's state has changed; false otherwise.</returns>
        protected override bool IsDirty(Note note)
        {
            return true;
        }

        /// <summary>
        /// Gets a value indicating whether the note is new and has never been saved.
        /// </summary>
        /// <param name="note">The note to check.</param>
        /// <returns>True if the note is new; false otherwise.</returns>
        protected override bool IsNew(Note note)
        {
            return note.Id == 0;
        }

        /// <summary>
        /// Performs any pre-save work.
        /// </summary>
        /// <param name="note">The note about to be saved.</param>
        protected override void PreSave(Note note)
        {
            base.PreSave(note);
            note.LastModifiedDate = DateTime.Now;
        }

        /// <summary>
        /// Performs any post-save work.
        /// </summary>
        /// <param name="note">The note that was saved.</param>
        protected override void PostSave(Note note)
        {
            base.PostSave(note);
        }

        /// <summary>
        /// Updates the note.
        /// </summary>
        /// <param name="note">The note to update.</param>
        /// <param name="context">The database context.</param>
        protected override void Update(Note note, AdoContext context)
        {
            this.InsertChangeHistoryUpdate(note);

            var sql = new StringBuilder("UPDATE Note");
            sql.Append(" SET LastModifiedDate = @LastModifiedDate, Text = @Text, IsPinned = @IsPinned, Name = @Name, Namespace = @Namespace, Class = @Class, TextFormat = @TextFormat, FormattedText = @FormattedText, DeletedDate = @DeletedDate");
            sql.Append(" WHERE Id = @Id");

            Stopwatch stopwatch = new Stopwatch();
            
            using (var command = this.AdoDataAccess.CreateCommand(sql.ToString()))
            {
                command.Parameters.Add(this.AdoDataAccess.CreateParameter("LastModifiedDate", note.LastModifiedDate));
                command.Parameters.Add(this.AdoDataAccess.CreateParameter("Class", note.Class));
                command.Parameters.Add(this.AdoDataAccess.CreateParameter("Text", note.Text));
                command.Parameters.Add(this.AdoDataAccess.CreateParameter("IsPinned", note.IsPinned));
                command.Parameters.Add(this.AdoDataAccess.CreateParameter("Name", note.Name));
                command.Parameters.Add(this.AdoDataAccess.CreateParameter("Id", note.Id));
                command.Parameters.Add(this.AdoDataAccess.CreateParameter("Namespace", note.Namespace));
                command.Parameters.Add(this.AdoDataAccess.CreateParameter("TextFormat", (int)note.TextFormat));
                command.Parameters.Add(this.AdoDataAccess.CreateParameter("FormattedText", note.FormattedText ?? string.Empty));
                command.Parameters.Add(this.AdoDataAccess.CreateParameter("DeletedDate", note.DeletedDate.HasValue ? (object)note.DeletedDate.Value : DBNull.Value));

                stopwatch.Start();
                this.AdoDataAccess.ExecuteNonQuery(command, context);
                stopwatch.Stop();
            }

            Statistics.Instance.RecordUpdate(stopwatch.Elapsed);
        }

        /// <summary>
        /// Inserts a change history item for a note insertion.
        /// </summary>
        /// <param name="note">The note that was inserted.</param>
        private void InsertChangeHistoryInsertion(Note note)
        {
            this.InsertChangeHistoryInsertion(note.Id, "Note", note.Name, note.Name);
        }

        /// <summary>
        /// Inserts a change history item for a note update.
        /// </summary>
        /// <param name="note">The note that was updated.</param>
        private void InsertChangeHistoryUpdate(Note note)
        {
            // ensure at most one update per hour
            if (this.CanLogUpdate(note))
            {
                this.InsertChangeHistoryUpdate(note.Id, "Text", note.FormattedText, note.Text);
            }
        }

        /// <summary>
        /// Determines whether the update should be logged to this history table.
        /// </summary>
        /// <param name="note">The note that was changed.</param>
        /// <returns>True if the change can be logged; false otherwise.</returns>
        protected virtual bool CanLogUpdate(Note note)
        {
            return false;
        }
    }
}

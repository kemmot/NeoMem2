// <copyright file="AdoNoteTagDataAccess.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Data
{
    using System;
    using System.Text;

    using NeoMem2.Core;

    /// <summary>
    /// Provides data access functionality related to <see cref="NoteTag"/> objects.
    /// </summary>
    public class AdoNoteTagDataAccess : AdoDataAccessBase<NoteTag>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdoNoteTagDataAccess" /> class.
        /// </summary>
        /// <param name="adoDataAccess">The data access object to use.</param>
        public AdoNoteTagDataAccess(IAdoDataAccess adoDataAccess)
            : base(adoDataAccess)
        {
        }

        /// <summary>
        /// Deletes the noteTag.
        /// </summary>
        /// <param name="noteTag">The noteTag to delete.</param>
        /// <param name="context">The database context.</param>
        public override void Delete(NoteTag noteTag, AdoContext context)
        {
            var sql = new StringBuilder("DELETE FROM NoteTag");
            sql.Append(" WHERE NoteId=@NoteId AND TagId = @TagId");

            using (var command = this.AdoDataAccess.CreateCommand(sql.ToString()))
            {
                command.Parameters.Add(this.AdoDataAccess.CreateParameter("NoteId", noteTag.Note.Id));
                command.Parameters.Add(this.AdoDataAccess.CreateParameter("TagId", noteTag.Tag.Id));

                AdoDataAccess.ExecuteNonQuery(command, context);
            }
        }

        /// <summary>
        /// Inserts the noteTag.
        /// </summary>
        /// <param name="noteTag">The noteTag to insert.</param>
        /// <param name="context">The database context.</param>
        protected override void Insert(NoteTag noteTag, AdoContext context)
        {
            var sql = new StringBuilder("INSERT INTO NoteTag");
            sql.Append(" (NoteId, TagId)");
            sql.Append(" VALUES (@NoteId, @TagId)");

            using (var command = this.AdoDataAccess.CreateCommand(sql.ToString()))
            {
                command.Parameters.Add(this.AdoDataAccess.CreateParameter("NoteId", noteTag.Note.Id));
                command.Parameters.Add(this.AdoDataAccess.CreateParameter("TagId", noteTag.Tag.Id));

                AdoDataAccess.ExecuteNonQuery(command, context);

                noteTag.IsNew = false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the noteTag has been deleted.
        /// </summary>
        /// <param name="noteTag">The noteTag to check.</param>
        /// <returns>True if the noteTag has been deleted; false otherwise.</returns>
        protected override bool IsDeleted(NoteTag noteTag)
        {
            return noteTag.IsDeleted;
        }

        /// <summary>
        /// Gets a value indicating whether the noteTag's state has changed since it was last saved.
        /// </summary>
        /// <param name="noteTag">The noteTag to check.</param>
        /// <returns>True if the noteTag's state has changed; false otherwise.</returns>
        protected override bool IsDirty(NoteTag noteTag)
        {
            return noteTag.IsDirty;
        }

        /// <summary>
        /// Gets a value indicating whether the noteTag is new and has never been saved.
        /// </summary>
        /// <param name="noteTag">The noteTag to check.</param>
        /// <returns>True if the noteTag is new; false otherwise.</returns>
        protected override bool IsNew(NoteTag noteTag)
        {
            return noteTag.IsNew;
        }

        /// <summary>
        /// Performs any post-save work.
        /// </summary>
        /// <param name="noteTag">The noteTag that was saved.</param>
        protected override void PostSave(NoteTag noteTag)
        {
            noteTag.ClearIsDirty();
        }

        /// <summary>
        /// Updates the noteTag.
        /// </summary>
        /// <param name="noteTag">The noteTag to update.</param>
        /// <param name="context">The database context.</param>
        protected override void Update(NoteTag noteTag, AdoContext context)
        {
            throw new NotImplementedException();
        }
    }
}

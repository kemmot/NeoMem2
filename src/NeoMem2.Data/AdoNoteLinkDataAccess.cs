// <copyright file="AdoNoteLinkDataAccess.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Data
{
    using System;
    using System.Text;

    using NeoMem2.Core;

    /// <summary>
    /// Provides data access functionality related to <see cref="NoteLink"/> objects.
    /// </summary>
    public class AdoNoteLinkDataAccess : AdoDataAccessBase<NoteLink>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdoNoteLinkDataAccess" /> class.
        /// </summary>
        /// <param name="adoDataAccess">The data access object to use.</param>
        public AdoNoteLinkDataAccess(IAdoDataAccess adoDataAccess)
            : base(adoDataAccess)
        {
        }

        /// <summary>
        /// Deletes the noteLink.
        /// </summary>
        /// <param name="noteLink">The noteLink to delete.</param>
        /// <param name="context">The database context.</param>
        public override void Delete(NoteLink noteLink, AdoContext context)
        {
            var sql = new StringBuilder("DELETE FROM NoteLink");
            sql.Append(" WHERE Id=@Id");

            using (var command = this.AdoDataAccess.CreateCommand(sql.ToString()))
            {
                command.Parameters.Add(this.AdoDataAccess.CreateParameter("Id", noteLink.Id));

                AdoDataAccess.ExecuteNonQuery(command, context);
            }
        }

        /// <summary>
        /// Inserts the noteLink.
        /// </summary>
        /// <param name="noteLink">The noteLink to insert.</param>
        /// <param name="context">The database context.</param>
        protected override void Insert(NoteLink noteLink, AdoContext context)
        {
            var sql = new StringBuilder("INSERT INTO NoteLink");
            sql.Append(" (Note1Id, Note2Id)");
            sql.Append(" VALUES (@Note1Id, @Note2Id);");
            sql.Append(" SELECT " + this.AdoDataAccess.GetProviderSqlStatementBuilder().IdentityFunction());

            using (var command = this.AdoDataAccess.CreateCommand(sql.ToString()))
            {
                command.Parameters.Add(this.AdoDataAccess.CreateParameter("Note1Id", noteLink.Note1.Id));
                command.Parameters.Add(this.AdoDataAccess.CreateParameter("Note2Id", noteLink.Note2.Id));

                noteLink.Id = Convert.ToInt64(AdoDataAccess.ExecuteScalar(command, context));
            }
        }

        /// <summary>
        /// Gets a value indicating whether the noteLink has been deleted.
        /// </summary>
        /// <param name="noteLink">The noteLink to check.</param>
        /// <returns>True if the noteLink has been deleted; false otherwise.</returns>
        protected override bool IsDeleted(NoteLink noteLink)
        {
            return noteLink.IsDeleted;
        }

        /// <summary>
        /// Gets a value indicating whether the noteLink's state has changed since it was last saved.
        /// </summary>
        /// <param name="noteLink">The noteLink to check.</param>
        /// <returns>True if the noteLink's state has changed; false otherwise.</returns>
        protected override bool IsDirty(NoteLink noteLink)
        {
            return noteLink.IsDirty;
        }

        /// <summary>
        /// Gets a value indicating whether the noteLink is new and has never been saved.
        /// </summary>
        /// <param name="noteLink">The noteLink to check.</param>
        /// <returns>True if the noteLink is new; false otherwise.</returns>
        protected override bool IsNew(NoteLink noteLink)
        {
            return noteLink.Id == 0;
        }

        /// <summary>
        /// Performs any post-save work.
        /// </summary>
        /// <param name="noteLink">The noteLink that was saved.</param>
        protected override void PostSave(NoteLink noteLink)
        {
            noteLink.ClearIsDirty();
            base.PostSave(noteLink);
        }

        /// <summary>
        /// Updates the noteLink.
        /// </summary>
        /// <param name="noteLink">The noteLink to update.</param>
        /// <param name="context">The database context.</param>
        protected override void Update(NoteLink noteLink, AdoContext context)
        {
            var sql = new StringBuilder("UPDATE NoteLink");
            sql.Append(" SET");
            sql.Append(" Note1Id=@Note1Id");
            sql.Append(", Note2Id=@Note2Id");
            sql.Append(" WHERE Id=@Id");

            using (var command = this.AdoDataAccess.CreateCommand(sql.ToString()))
            {
                command.Parameters.Add(this.AdoDataAccess.CreateParameter("Id", noteLink.Id));
                command.Parameters.Add(this.AdoDataAccess.CreateParameter("Note1Id", noteLink.Note1.Id));
                command.Parameters.Add(this.AdoDataAccess.CreateParameter("Note2Id", noteLink.Note2.Id));

                AdoDataAccess.ExecuteNonQuery(command, context);
            }
        }
    }
}

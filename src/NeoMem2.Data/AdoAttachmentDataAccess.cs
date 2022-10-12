// <copyright file="AdoAttachmentDataAccess.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Data
{
    using System;
    using System.Data.SqlClient;
    using System.Text;

    using NeoMem2.Core;

    /// <summary>
    /// Provides data access functionality related to <see cref="Attachment"/> objects.
    /// </summary>
    public class AdoAttachmentDataAccess : AdoDataAccessBase<Attachment>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdoAttachmentDataAccess" /> class.
        /// </summary>
        /// <param name="adoDataAccess">The data access object to use.</param>
        public AdoAttachmentDataAccess(IAdoDataAccess adoDataAccess)
            : base(adoDataAccess)
        {
        }

        /// <summary>
        /// Deletes the attachment.
        /// </summary>
        /// <param name="attachment">The attachment to delete.</param>
        /// <param name="context">The database context.</param>
        public override void Delete(Attachment attachment, AdoContext context)
        {
            var sql = new StringBuilder("DELETE FROM Attachment");
            sql.Append(" WHERE Id=@Id");

            using (var command = this.AdoDataAccess.CreateCommand(sql.ToString()))
            {
                command.Parameters.Add(this.AdoDataAccess.CreateParameter("Id", attachment.Id));

                AdoDataAccess.ExecuteNonQuery(command, context);
            }
        }

        /// <summary>
        /// Inserts the attachment.
        /// </summary>
        /// <param name="attachment">The attachment to insert.</param>
        /// <param name="context">The database context.</param>
        protected override void Insert(Attachment attachment, AdoContext context)
        {
            var sql = new StringBuilder("INSERT INTO Attachment");
            sql.Append(" (NoteId, Filename)");
            sql.Append(" VALUES (@NoteId, @Filename);");
            sql.Append(" SELECT SCOPE_IDENTITY()");

            using (var command = this.AdoDataAccess.CreateCommand(sql.ToString()))
            {
                command.Parameters.Add(this.AdoDataAccess.CreateParameter("NoteId", attachment.Note.Id));
                command.Parameters.Add(this.AdoDataAccess.CreateParameter("Filename", attachment.Filename));

                attachment.Id = Convert.ToInt64(AdoDataAccess.ExecuteScalar(command, context));
            }
        }

        /// <summary>
        /// Gets a value indicating whether the attachment has been deleted.
        /// </summary>
        /// <param name="attachment">The attachment to check.</param>
        /// <returns>True if the attachment has been deleted; false otherwise.</returns>
        protected override bool IsDeleted(Attachment attachment)
        {
            return attachment.IsDeleted;
        }

        /// <summary>
        /// Gets a value indicating whether the attachment's state has changed since it was last saved.
        /// </summary>
        /// <param name="attachment">The attachment to check.</param>
        /// <returns>True if the attachment's state has changed; false otherwise.</returns>
        protected override bool IsDirty(Attachment attachment)
        {
            return false;
        }

        /// <summary>
        /// Gets a value indicating whether the attachment is new and has never been saved.
        /// </summary>
        /// <param name="attachment">The attachment to check.</param>
        /// <returns>True if the attachment is new; false otherwise.</returns>
        protected override bool IsNew(Attachment attachment)
        {
            return attachment.Id == 0;
        }

        /// <summary>
        /// Updates the attachment.
        /// </summary>
        /// <param name="attachment">The attachment to update.</param>
        /// <param name="context">The database context.</param>
        protected override void Update(Attachment attachment, AdoContext context)
        {
            throw new NotSupportedException("Updating attachments is not supported");
        }
    }
}

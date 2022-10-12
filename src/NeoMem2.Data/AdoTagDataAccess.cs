// <copyright file="AdoTagDataAccess.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Data
{
    using System;
    using System.Data.SqlClient;
    using System.Text;

    using NeoMem2.Core;

    /// <summary>
    /// Provides data access functionality related to <see cref="Tag"/> objects.
    /// </summary>
    public class AdoTagDataAccess : AdoDataAccessBase<Tag>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdoTagDataAccess" /> class.
        /// </summary>
        /// <param name="adoDataAccess">The data access object to use.</param>
        public AdoTagDataAccess(IAdoDataAccess adoDataAccess)
            : base(adoDataAccess)
        {
        }

        /// <summary>
        /// Deletes the tag.
        /// </summary>
        /// <param name="tag">The tag to delete.</param>
        /// <param name="context">The database context.</param>
        public override void Delete(Tag tag, AdoContext context)
        {
            var sql = new StringBuilder("DELETE FROM Tag");
            sql.Append(" WHERE Id=@Id");

            using (var command = this.AdoDataAccess.CreateCommand(sql.ToString()))
            {
                command.Parameters.Add(this.AdoDataAccess.CreateParameter("Id", tag.Id));

                AdoDataAccess.ExecuteNonQuery(command);
            }
        }

        /// <summary>
        /// Inserts the tag.
        /// </summary>
        /// <param name="tag">The tag to insert.</param>
        /// <param name="context">The database context.</param>
        protected override void Insert(Tag tag, AdoContext context)
        {
            var sql = new StringBuilder("INSERT INTO Tag");
            sql.Append(" (Name)");
            sql.Append(" VALUES (@Name);");            
            sql.Append($" SELECT {this.AdoDataAccess.GetProviderSqlStatementBuilder().IdentityFunction()}");

            using (var command = this.AdoDataAccess.CreateCommand(sql.ToString()))
            {
                command.Parameters.Add(this.AdoDataAccess.CreateParameter("Name", tag.Name));

                tag.Id = Convert.ToInt32(AdoDataAccess.ExecuteScalar(command));
            }
        }

        /// <summary>
        /// Gets a value indicating whether the tag has been deleted.
        /// </summary>
        /// <param name="tag">The tag to check.</param>
        /// <returns>True if the tag has been deleted; false otherwise.</returns>
        protected override bool IsDeleted(Tag tag)
        {
            return tag.IsDeleted;
        }

        /// <summary>
        /// Gets a value indicating whether the tag's state has changed since it was last saved.
        /// </summary>
        /// <param name="tag">The tag to check.</param>
        /// <returns>True if the tag's state has changed; false otherwise.</returns>
        protected override bool IsDirty(Tag tag)
        {
            return tag.IsDirty;
        }

        /// <summary>
        /// Gets a value indicating whether the tag is new and has never been saved.
        /// </summary>
        /// <param name="tag">The tag to check.</param>
        /// <returns>True if the tag is new; false otherwise.</returns>
        protected override bool IsNew(Tag tag)
        {
            return tag.Id <= 0;
        }

        /// <summary>
        /// Performs any post-save work.
        /// </summary>
        /// <param name="tag">The tag that was saved.</param>
        protected override void PostSave(Tag tag)
        {
            tag.ClearIsDirty();
        }

        /// <summary>
        /// Updates the tag.
        /// </summary>
        /// <param name="tag">The tag to update.</param>
        /// <param name="context">The database context.</param>
        protected override void Update(Tag tag, AdoContext context)
        {
            var sql = new StringBuilder("UPDATE Property");
            sql.Append(" SET");
            sql.Append(" Name=@Name");
            sql.Append(" WHERE Id=@Id");

            using (var command = this.AdoDataAccess.CreateCommand(sql.ToString()))
            {
                command.Parameters.Add(this.AdoDataAccess.CreateParameter("Id", tag.Id));
                command.Parameters.Add(this.AdoDataAccess.CreateParameter("Name", tag.Name));

                AdoDataAccess.ExecuteNonQuery(command);
            }
        }
    }
}

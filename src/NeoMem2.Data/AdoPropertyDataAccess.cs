// <copyright file="AdoPropertyDataAccess.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Data
{
    using System;
    using System.Data.SqlClient;
    using System.Text;

    using NeoMem2.Core;

    /// <summary>
    /// Provides data access functionality related to <see cref="Property"/> objects.
    /// </summary>
    public class AdoPropertyDataAccess : AdoDataAccessBase<Property>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdoPropertyDataAccess" /> class.
        /// </summary>
        /// <param name="adoDataAccess">The data access object to use.</param>
        public AdoPropertyDataAccess(IAdoDataAccess adoDataAccess)
            : base(adoDataAccess)
        {
        }

        /// <summary>
        /// Deletes the property.
        /// </summary>
        /// <param name="property">The property to delete.</param>
        /// <param name="context">The database context.</param>
        public override void Delete(Property property, AdoContext context)
        {
            this.InsertChangeHistoryDeletion(property);

            var sql = new StringBuilder("DELETE FROM Property");
            sql.Append(" WHERE Id=@Id");

            using (var command = this.AdoDataAccess.CreateCommand(sql.ToString()))
            {
                command.Parameters.Add(this.AdoDataAccess.CreateParameter("Id", property.Id));

                AdoDataAccess.ExecuteNonQuery(command, context);
            }
        }

        /// <summary>
        /// Inserts the property.
        /// </summary>
        /// <param name="property">The property to insert.</param>
        /// <param name="context">The database context.</param>
        protected override void Insert(Property property, AdoContext context)
        {
            this.InsertChangeHistoryInsertion(property);

            var sql = new StringBuilder("INSERT INTO Property");
            sql.Append(" (NoteId, Name, ClrDataType, Value, ValueString, IsSystemProperty)");
            sql.Append(" VALUES (@NoteId, @Name, @ClrDataType, @Value, @ValueString, @IsSystemProperty);");
            sql.Append($" SELECT {this.AdoDataAccess.GetProviderSqlStatementBuilder().IdentityFunction()}");

            using (var command = this.AdoDataAccess.CreateCommand(sql.ToString()))
            {
                command.Parameters.Add(this.AdoDataAccess.CreateParameter("NoteId", property.Note.Id));
                command.Parameters.Add(this.AdoDataAccess.CreateParameter("Name", property.Name));
                command.Parameters.Add(this.AdoDataAccess.CreateParameter("ClrDataType", property.ClrDataType));
                command.Parameters.Add(this.AdoDataAccess.CreateParameter("Value", property.Value));
                command.Parameters.Add(this.AdoDataAccess.CreateParameter("ValueString", property.ValueString));
                command.Parameters.Add(this.AdoDataAccess.CreateParameter("IsSystemProperty", property.IsSystemProperty));

                property.Id = Convert.ToInt64(AdoDataAccess.ExecuteScalar(command, context));
            }
        }

        /// <summary>
        /// Gets a value indicating whether the property has been deleted.
        /// </summary>
        /// <param name="property">The property to check.</param>
        /// <returns>True if the property has been deleted; false otherwise.</returns>
        protected override bool IsDeleted(Property property)
        {
            return property.IsDeleted;
        }

        /// <summary>
        /// Gets a value indicating whether the property's state has changed since it was last saved.
        /// </summary>
        /// <param name="property">The property to check.</param>
        /// <returns>True if the property's state has changed; false otherwise.</returns>
        protected override bool IsDirty(Property property)
        {
            return property.IsDirty;
        }

        /// <summary>
        /// Gets a value indicating whether the property is new and has never been saved.
        /// </summary>
        /// <param name="property">The property to check.</param>
        /// <returns>True if the property is new; false otherwise.</returns>
        protected override bool IsNew(Property property)
        {
            return property.Id <= 0;
        }

        /// <summary>
        /// Performs any post-save work.
        /// </summary>
        /// <param name="property">The property that was saved.</param>
        protected override void PostSave(Property property)
        {
            property.ClearIsDirty();
        }

        /// <summary>
        /// Updates the property.
        /// </summary>
        /// <param name="property">The property to update.</param>
        /// <param name="context">The database context.</param>
        protected override void Update(Property property, AdoContext context)
        {
            this.InsertChangeHistoryUpdate(property);

            var sql = new StringBuilder("UPDATE Property");
            sql.Append(" SET");
            sql.Append(" NoteId=@NoteId");
            sql.Append(", Name=@Name");
            sql.Append(", ClrDataType=@ClrDataType");
            sql.Append(", Value=@Value");
            sql.Append(", ValueString=@ValueString");
            sql.Append(", IsSystemProperty=@IsSystemProperty");
            sql.Append(" WHERE Id=@Id");

            using (var command = this.AdoDataAccess.CreateCommand(sql.ToString()))
            {
                command.Parameters.Add(this.AdoDataAccess.CreateParameter("Id", property.Id));
                command.Parameters.Add(this.AdoDataAccess.CreateParameter("NoteId", property.Note.Id));
                command.Parameters.Add(this.AdoDataAccess.CreateParameter("Name", property.Name));
                command.Parameters.Add(this.AdoDataAccess.CreateParameter("ClrDataType", property.ClrDataType));
                command.Parameters.Add(this.AdoDataAccess.CreateParameter("Value", property.Value));
                command.Parameters.Add(this.AdoDataAccess.CreateParameter("ValueString", property.ValueString));
                command.Parameters.Add(this.AdoDataAccess.CreateParameter("IsSystemProperty", property.IsSystemProperty));

                AdoDataAccess.ExecuteNonQuery(command, context);
            }
        }

        /// <summary>
        /// Inserts a change history item for a property deletion.
        /// </summary>
        /// <param name="property">The property that was deleted.</param>
        private void InsertChangeHistoryDeletion(Property property)
        {
            this.InsertChangeHistoryDeletion(property.Note.Id, property.Name);
        }

        /// <summary>
        /// Inserts a change history item for a property insertion.
        /// </summary>
        /// <param name="property">The property that was inserted.</param>
        private void InsertChangeHistoryInsertion(Property property)
        {
            this.InsertChangeHistoryInsertion(property.Note.Id, property.Name, property.Value, property.ValueString);
        }

        /// <summary>
        /// Inserts a change history item for a property update.
        /// </summary>
        /// <param name="property">The property that was updated.</param>
        private void InsertChangeHistoryUpdate(Property property)
        {
            this.InsertChangeHistoryUpdate(property.Note.Id, property.Name, property.Value, property.ValueString);
        }
    }
}

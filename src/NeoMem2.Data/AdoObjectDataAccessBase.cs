// <copyright file="AdoDataAccessBase.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Data
{
    using NeoMem2.Core;
    using NeoMem2.Data.FluentSql;

    using System.Text;

    /// <summary>
    /// An implementation of <see cref="IObjectDataAccess{T}"/> that uses SQL Server.
    /// </summary>
    /// <typeparam name="T">The type of object to deal with.</typeparam>
    public abstract class AdoDataAccessBase<T> : ObjectDataAccessBase<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdoDataAccessBase{T}" /> class.
        /// </summary>
        /// <param name="adoDataAccess">The data access object to use.</param>
        protected AdoDataAccessBase(IAdoDataAccess adoDataAccess)
            : base(adoDataAccess)
        {
        }

        /// <summary>
        /// Inserts a change history for tracking a note deletion.
        /// </summary>
        /// <param name="noteId">The ID of the note that was changed.</param>
        /// <param name="fieldName">The name of the field that was changed.</param>
        protected void InsertChangeHistoryDeletion(long noteId, string fieldName)
        {
            this.InsertChangeHistory(noteId, fieldName, NoteChangeType.Deleted, null, null);
        }

        /// <summary>
        /// Inserts a change history for tracking a note insertion.
        /// </summary>
        /// <param name="noteId">The ID of the note that was changed.</param>
        /// <param name="fieldName">The name of the field that was changed.</param>
        /// <param name="newValue">The new value after the change.</param>
        /// <param name="newValueString">A string representation of the new value after the change.</param>
        protected void InsertChangeHistoryInsertion(long noteId, string fieldName, string newValue, string newValueString)
        {
            this.InsertChangeHistory(noteId, fieldName, NoteChangeType.Inserted, newValue, newValueString);
        }

        /// <summary>
        /// Inserts a change history for tracking a note update.
        /// </summary>
        /// <param name="noteId">The ID of the note that was changed.</param>
        /// <param name="fieldName">The name of the field that was changed.</param>
        /// <param name="newValue">The new value after the change.</param>
        /// <param name="newValueString">A string representation of the new value after the change.</param>
        protected void InsertChangeHistoryUpdate(long noteId, string fieldName, string newValue, string newValueString)
        {
            this.InsertChangeHistory(noteId, fieldName, NoteChangeType.Updated, newValue, newValueString);
        }

        /// <summary>
        /// Inserts a change history for tracking changes of various types.
        /// </summary>
        /// <param name="noteId">The ID of the note that was changed.</param>
        /// <param name="fieldName">The name of the field that was changed.</param>
        /// <param name="changeType">The type of change being tracked.</param>
        /// <param name="newValue">The new value after the change.</param>
        /// <param name="newValueString">A string representation of the new value after the change.</param>
        private void InsertChangeHistory(long noteId, string fieldName, NoteChangeType changeType, string newValue, string newValueString)
        {
            if (newValue == null)
            {
                newValue = string.Empty;
            }

            if (newValueString == null)
            {
                newValueString = string.Empty;
            }

            var insertStatement = InsertSqlStatementBuilder.Insert("NoteHistory")
                .AddColumn("NoteId", noteId)
                .AddColumn("FieldName", fieldName)
                .AddColumn("ChangeDate", this.AdoDataAccess.GetProviderSqlStatementBuilder().DateFunction(), isFunction:true)
                .AddColumn("ChangeTypeId", (int)changeType)
                .AddColumn("Value", newValue, useParameter:true)
                .AddColumn("ValueString", newValueString, useParameter: true);

            //var sql = new StringBuilder("INSERT INTO NoteHistory");
            //sql.Append(" (NoteId, FieldName, ChangeDate, ChangeTypeId, Value, ValueString)");
            //sql.Append(" VALUES (@NoteId, @FieldName, GETDATE(), @ChangeTypeId, @Value, @ValueString)");

            using (var command = insertStatement.GetCommand(this.AdoDataAccess))
            {
                //command.Parameters.Add(this.AdoDataAccess.CreateParameter("NoteId", noteId));
                //command.Parameters.Add(this.AdoDataAccess.CreateParameter("FieldName", fieldName));
                //command.Parameters.Add(this.AdoDataAccess.CreateParameter("ChangeTypeId", (int)changeType));
                //command.Parameters.Add(this.AdoDataAccess.CreateParameter("Value", newValue));
                //command.Parameters.Add(this.AdoDataAccess.CreateParameter("ValueString", newValueString));

                this.AdoDataAccess.ExecuteNonQuery(command);
            }
        }
    }
}

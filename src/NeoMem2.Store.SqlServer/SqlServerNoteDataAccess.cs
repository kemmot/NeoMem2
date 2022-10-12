namespace NeoMem2.Data.SqlServer
{
    using NeoMem2.Core;

    using System;
    using System.Data;

    public class SqlServerNoteDataAccess : AdoNoteDataAccess
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlServerNoteDataAccess" /> class.
        /// </summary>
        /// <param name="adoDataAccess">The data access object to use.</param>
        public SqlServerNoteDataAccess(IAdoDataAccess adoDataAccess)
            : base(adoDataAccess)
        {
        }

        /// <summary>
        /// Determines whether the update should be logged to this history table.
        /// </summary>
        /// <param name="note">The note that was changed.</param>
        /// <returns>True if the change can be logged; false otherwise.</returns>
        protected override bool CanLogUpdate(Note note)
        {
            bool canLogUpdate = true;
            using (var connection = this.AdoDataAccess.CreateConnection(AdoDataAccess.ConnectionString))
            {
                const string Sql = "SELECT TOP 1 ChangeDate, Value FROM NoteHistory WHERE NoteId = @NoteId AND ChangeTypeId = @ChangeTypeId ORDER BY ChangeDate DESC";
                using (var command = this.AdoDataAccess.CreateCommand(Sql, connection))
                {
                    command.Parameters.Add(this.AdoDataAccess.CreateParameter("NoteId", note.Id));
                    command.Parameters.Add(this.AdoDataAccess.CreateParameter("ChangeTypeId", NoteChangeType.Updated));

                    connection.Open();
                    try
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                DateTime changeDate = Convert.ToDateTime(reader["ChangeDate"]);
                                string text = Convert.ToString(reader["Value"]);

                                if (changeDate > DateTime.Now.Subtract(TimeSpan.FromHours(1)))
                                {
                                    canLogUpdate = false;
                                }

                                if (text == note.Text)
                                {
                                    canLogUpdate = false;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Failed executing command: {AdoDataAccess.GetCommandDescription(command)}", ex);
                    }

                    connection.Close();
                }
            }

            return canLogUpdate;
        }
    }
}

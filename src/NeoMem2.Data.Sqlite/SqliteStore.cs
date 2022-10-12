namespace NeoMem2.Data.Sqlite
{
    using NeoMem2.Core;
    using NeoMem2.Utils;

    using NLog;

    using System;
    using System.Data;
    using System.Text;

    public class SqliteStore : AdoStoreBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public SqliteStore(string connectionString)
            : base(new SqliteDataAccess(connectionString))
        {
        }

        /// <summary>
        /// Gets a default connection string for this store.
        /// </summary>
        /// <returns>The connection string.</returns>
        public static string GetDefaultConnectionString()
        {
            return "NeoMem2.sqlite.dat";
        }

        /// <summary>
        /// Creates a new store of the type managed by this class.
        /// </summary>
        /// <param name="recreate">Set to true to remove and recreate it if it already exists.</param>
        public override void CreateNewStore(bool recreate = false)
        {
            using (var connection = this.DataAccess.CreateConnection())
            {                
                connection.Open();

                // create tables
                CreateAttachmentTable(connection);
                CreateNoteTable(connection);
                CreateNoteHistoryTable(connection);
                CreateNoteHistoryTypeTable(connection);
                CreateNoteLinkTable(connection);
                CreateNoteTagTable(connection);
                CreatePropertyTable(connection);
                CreateTagTable(connection);
                CreateTextFormatTable(connection);

                // insert ref data
                InsertTextFormats(connection);
                InsertNoteHistoryTypes(connection);

                connection.Close();
            }
        }

        private void CreateAttachmentTable(IDbConnection connection)
        {
            var commandString = new StringBuilder();
            commandString.Append("CREATE TABLE Attachement (");
            commandString.Append("Id INTEGER PRIMARY KEY ASC AUTOINCREMENT");
            commandString.Append(", NoteId INTEGER NOT NULL");
            commandString.Append(", Filename TEXT NOT NULL");
            commandString.Append(", Data BLOB NOT NULL");
            commandString.Append(", DataLength INTEGER NOT NULL");
            commandString.Append(", FOREIGN KEY(NoteId) REFERENCES Note(Id)");
            commandString.Append(")");

            using (var command = this.DataAccess.CreateCommand(commandString.ToString(), connection))
            {
                ExecuteNonQuery(command);
            }
        }

        private void CreateNoteTable(IDbConnection connection)
        {
            var commandString = new StringBuilder();
            commandString.Append("CREATE TABLE Note (");
            commandString.Append("Id INTEGER PRIMARY KEY ASC AUTOINCREMENT");
            commandString.Append(", Name TEXT NOT NULL");
            commandString.Append(", Text TEXT NOT NULL");
            commandString.Append(", CreatedDate TEXT NOT NULL");
            commandString.Append(", LastAccessedDate TEXT NOT NULL");
            commandString.Append(", LastModifiedDate TEXT NOT NULL");
            commandString.Append(", IsPinned INTEGER NOT NULL");
            commandString.Append(", Namespace TEXT NOT NULL");
            commandString.Append(", Class TEXT NOT NULL");
            commandString.Append(", TextFormat INTEGER NOT NULL");
            commandString.Append(", FormattedText TEXT NULL");
            commandString.Append(", DeletedDate TEXT NULL");
            commandString.Append(", FOREIGN KEY(TextFormat) REFERENCES TextFormat(Id)");
            commandString.Append(")");

            using (var command = this.DataAccess.CreateCommand(commandString.ToString(), connection))
            {
                ExecuteNonQuery(command);
            }
        }

        private void CreateNoteHistoryTable(IDbConnection connection)
        {
            var commandString = new StringBuilder();
            commandString.Append("CREATE TABLE NoteHistory (");
            commandString.Append("Id INTEGER PRIMARY KEY ASC AUTOINCREMENT");
            commandString.Append(", NoteId INTEGER NOT NULL");
            commandString.Append(", FieldName TEXT NOT NULL");
            commandString.Append(", ChangeDate TEXT NOT NULL");
            commandString.Append(", Value TEXT NOT NULL");
            commandString.Append(", ValueString TEXT NOT NULL");
            commandString.Append(", ChangeTypeId INTEGER NOT NULL");
            commandString.Append(", FOREIGN KEY(NoteId) REFERENCES Note(Id)");
            commandString.Append(", FOREIGN KEY(ChangeTypeId) REFERENCES NoteHistoryType(Id)");
            commandString.Append(")");

            using (var command = this.DataAccess.CreateCommand(commandString.ToString(), connection))
            {
                ExecuteNonQuery(command);
            }
        }

        private void CreateNoteHistoryTypeTable(IDbConnection connection)
        {
            var commandString = new StringBuilder();
            commandString.Append("CREATE TABLE NoteHistoryType (");
            commandString.Append("Id INTEGER PRIMARY KEY ASC AUTOINCREMENT");
            commandString.Append(", Name TEXT NOT NULL");
            commandString.Append(")");

            using (var command = this.DataAccess.CreateCommand(commandString.ToString(), connection))
            {
                ExecuteNonQuery(command);
            }
        }

        private void CreateNoteLinkTable(IDbConnection connection)
        {
            var commandString = new StringBuilder();
            commandString.Append("CREATE TABLE NoteLink (");
            commandString.Append("Id INTEGER PRIMARY KEY ASC AUTOINCREMENT");
            commandString.Append(", Note1Id INTEGER NOT NULL");
            commandString.Append(", Note2Id INTEGER NOT NULL");
            commandString.Append(", FOREIGN KEY(Note1Id) REFERENCES Note(Id)");
            commandString.Append(", FOREIGN KEY(Note2Id) REFERENCES Note(Id)");
            commandString.Append(")");

            using (var command = this.DataAccess.CreateCommand(commandString.ToString(), connection))
            {
                ExecuteNonQuery(command);
            }
        }

        private void CreateNoteTagTable(IDbConnection connection)
        {
            var commandString = new StringBuilder();
            commandString.Append("CREATE TABLE NoteTag (");
            commandString.Append("Id INTEGER PRIMARY KEY ASC AUTOINCREMENT");
            commandString.Append(", NoteId INTEGER NOT NULL");
            commandString.Append(", TagId INTEGER NOT NULL");
            commandString.Append(", FOREIGN KEY(TagId) REFERENCES Tag(Id)");
            commandString.Append(")");

            using (var command = this.DataAccess.CreateCommand(commandString.ToString(), connection))
            {
                ExecuteNonQuery(command);
            }
        }

        private void CreatePropertyTable(IDbConnection connection)
        {
            var commandString = new StringBuilder();
            commandString.Append("CREATE TABLE Property (");
            commandString.Append("Id INTEGER PRIMARY KEY ASC AUTOINCREMENT");
            commandString.Append(", NoteId INTEGER NOT NULL");
            commandString.Append(", Name TEXT NOT NULL");
            commandString.Append(", ClrDataType TEXT NOT NULL");
            commandString.Append(", IsSystemProperty INTEGER NOT NULL");
            commandString.Append(", Value TEXT NOT NULL");
            commandString.Append(", ValueString TEXT NOT NULL");
            commandString.Append(", FOREIGN KEY(NoteId) REFERENCES Note(Id)");
            commandString.Append(")");

            using (var command = this.DataAccess.CreateCommand(commandString.ToString(), connection))
            {
                ExecuteNonQuery(command);
            }
        }

        private void CreateTagTable(IDbConnection connection)
        {
            var commandString = new StringBuilder();
            commandString.Append("CREATE TABLE Tag (");
            commandString.Append("Id INTEGER PRIMARY KEY ASC AUTOINCREMENT");
            commandString.Append(", Name TEXT NOT NULL");
            commandString.Append(")");

            using (var command = this.DataAccess.CreateCommand(commandString.ToString(), connection))
            {
                ExecuteNonQuery(command);
            }
        }

        private void CreateTextFormatTable(IDbConnection connection)
        {
            var commandString = new StringBuilder();
            commandString.Append("CREATE TABLE TextFormat (");
            commandString.Append("Id INTEGER PRIMARY KEY ASC AUTOINCREMENT");
            commandString.Append(", Name TEXT NOT NULL");
            commandString.Append(", Description TEXT NOT NULL");
            commandString.Append(")");

            using (var command = this.DataAccess.CreateCommand(commandString.ToString(), connection))
            {
                ExecuteNonQuery(command);
            }
        }

        private void InsertTextFormats(IDbConnection connection)
        {
            foreach (TextFormat textFormat in Enum.GetValues(typeof(TextFormat)))
            {
                InsertTextFormat(connection, textFormat);
            }
        }

        private void InsertTextFormat(IDbConnection connection, TextFormat textFormat)
        {
            int id = (int)textFormat;
            string name = textFormat.ToString();
            string description = new EnumWrapper<TextFormat>(textFormat).Description;
            InsertTextFormat(connection, id, name, description);
        }

        private void InsertTextFormat(IDbConnection connection, int id, string name, string description)
        {
            var commandString = new StringBuilder();
            commandString.Append("INSERT INTO TextFormat (Id, Name, Description)");
            commandString.Append($" VALUES ({id}, \"{name}\", \"{description}\"");
            commandString.Append(")");

            using (var command = this.DataAccess.CreateCommand(commandString.ToString(), connection))
            {
                ExecuteNonQuery(command);
            }
        }
        
        private void InsertNoteHistoryTypes(IDbConnection connection)
        {
            InsertNoteHistoryType(connection, 1, "Inserted");
            InsertNoteHistoryType(connection, 2, "Updated");
            InsertNoteHistoryType(connection, 3, "Deleted");
        }

        private void InsertNoteHistoryType(IDbConnection connection, int id, string name)
        {
            var commandString = new StringBuilder();
            commandString.Append("INSERT INTO NoteHistoryType (Id, Name)");
            commandString.Append($" VALUES ({id}, \"{name}\"");
            commandString.Append(")");

            using (var command = this.DataAccess.CreateCommand(commandString.ToString(), connection))
            {
                ExecuteNonQuery(command);
            }
        }

        private void ExecuteNonQuery(IDbCommand command)
        {
            string commandDescription = base.DataAccess.GetCommandDescription(command);
            try
            {
                command.ExecuteNonQuery();
                Logger.Trace("Executed SQL: {0}", commandDescription);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed executing SQL: {commandDescription}", ex);
            }
        }
    }
}

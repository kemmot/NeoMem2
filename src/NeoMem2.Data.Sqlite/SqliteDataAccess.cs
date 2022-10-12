// <copyright file="SqliteDataAccess.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Data.Sqlite
{
    using NeoMem2.Data.FluentSql;
    using System.Data;
    using System.Data.SQLite;

    public class SqliteDataAccess : AdoDataAccess
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqliteDataAccess" /> class.
        /// </summary>
        /// <param name="connectionString">The connection string to use for data access.</param>
        public SqliteDataAccess(string connectionString)
            : base(connectionString)
        {
        }

        public override IDbCommand CreateCommand()
        {
            return new SQLiteCommand();
        }

        public override IDbConnection CreateConnection()
        {
            return new SQLiteConnection(this.ConnectionString);
        }

        public override IDataParameter CreateParameter()
        {
            return new SQLiteParameter();
        }

        public override IProviderSqlStatementBuilder GetProviderSqlStatementBuilder()
        {
            return new SqliteStatementBuilder();
        }
    }
}

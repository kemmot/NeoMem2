// <copyright file="SqlServerDataAccess.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Data.SqlServer
{
    using NeoMem2.Data.FluentSql;
    using System.Data;
    using System.Data.SqlClient;

    public class SqlServerDataAccess : AdoDataAccess
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlServerDataAccess" /> class.
        /// </summary>
        /// <param name="connectionString">The connection string to use for data access.</param>
        public SqlServerDataAccess(string connectionString)
            : base(connectionString)
        {
        }

        public override IDbCommand CreateCommand()
        {
            return new SqlCommand();
        }

        public override IDbConnection CreateConnection()
        {
            return new SqlConnection(this.ConnectionString);
        }

        public override IDataParameter CreateParameter()
        {
            return new SqlParameter();
        }

        public override IProviderSqlStatementBuilder GetProviderSqlStatementBuilder()
        {
            return new SqlServerStatementBuilder();
        }
    }
}

// <copyright file="IAdoDataAccess.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Data
{
    using NeoMem2.Data.FluentSql;
    using System.Data;

    public interface IAdoDataAccess
    {
        string ConnectionString { get; }

        IDbConnection CreateConnection(string connectionString);

        IDbConnection CreateConnection();

        IDbCommand CreateCommand(string commandText);

        IDbCommand CreateCommand(string commandText, IDbConnection connection);

        AdoContext CreateContext(bool transaction = false);

        IDataParameter CreateParameter(string name, object value);

        IDataParameter CreateParameter();

        void ExecuteNonQuery(IDbCommand command);

        void ExecuteNonQuery(IDbCommand command, AdoContext context);

        object ExecuteScalar(IDbCommand command);

        object ExecuteScalar(IDbCommand command, AdoContext context);

        string GetCommandDescription(IDbCommand command, bool raw = false);

        IProviderSqlStatementBuilder GetProviderSqlStatementBuilder();
    }
}
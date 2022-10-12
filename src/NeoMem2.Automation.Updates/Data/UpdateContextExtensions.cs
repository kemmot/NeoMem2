// <copyright file="UpdateContextExtensions.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Automation.Updates.Data
{
    using System;
    using System.Data;
    using System.Data.SqlClient;

    /// <summary>
    /// Provides data access extension methods for the <see cref="UpdateContext"/> class.
    /// </summary>
    public static class UpdateContextExtensions
    {
        /// <summary>
        /// The name of the connection variable.
        /// </summary>
        private const string ConnectionVariableName = "Connection";

        /// <summary>
        /// Executes a non-query SQL statement against a specific database.
        /// </summary>
        /// <param name="context">The context to use.</param>
        /// <param name="databaseName">The name of the database to execute the statement against.</param>
        /// <param name="sqlFormat">The format of the SQL statement to execute.</param>
        /// <param name="args">The arguments to use with the format string.</param>
        public static void ExecuteDatabaseNonQueryText(this UpdateContext context, string databaseName, string sqlFormat, params object[] args)
        {
            context.ExecuteDatabaseNonQueryText(databaseName, string.Format(sqlFormat, args));
        }

        /// <summary>
        /// Executes a non-query SQL statement against a specific database.
        /// </summary>
        /// <param name="context">The context to use.</param>
        /// <param name="databaseName">The name of the database to execute the statement against.</param>
        /// <param name="sql">The SQL statement to execute.</param>
        public static void ExecuteDatabaseNonQueryText(this UpdateContext context, string databaseName, string sql)
        {
            var connection = context.GetDatabaseConnection(databaseName);
            
            using (var command = connection.CreateCommand())
            {
                command.CommandText = sql;
                command.CommandType = CommandType.Text;
                command.Connection = connection;

                ExecuteAction<object>(
                    command,
                    localCommand =>
                        {
                            localCommand.ExecuteNonQuery();
                            return null;
                        });
            }
        }

        /// <summary>
        /// Executes a scalar SQL statement against a specific database.
        /// </summary>
        /// <param name="context">The context to use.</param>
        /// <param name="databaseName">The name of the database to execute the statement against.</param>
        /// <param name="sqlFormat">The format of the SQL statement to execute.</param>
        /// <param name="args">The arguments to use with the format string.</param>
        /// <returns>The scalar result.</returns>
        public static object ExecuteDatabaseScalarText(this UpdateContext context, string databaseName, string sqlFormat, params object[] args)
        {
            return context.ExecuteDatabaseScalarText(databaseName, string.Format(sqlFormat, args));
        }

        /// <summary>
        /// Executes a scalar SQL statement against a specific database.
        /// </summary>
        /// <param name="context">The context to use.</param>
        /// <param name="databaseName">The name of the database to execute the statement against.</param>
        /// <param name="sql">The SQL statement to execute.</param>
        /// <returns>The scalar result.</returns>
        public static object ExecuteDatabaseScalarText(this UpdateContext context, string databaseName, string sql)
        {
            var connection = context.GetDatabaseConnection(databaseName);

            object result;
            using (var command = connection.CreateCommand())
            {
                command.CommandText = sql;
                command.CommandType = CommandType.Text;
                command.Connection = connection;

                result = ExecuteAction(command, localCommand => localCommand.ExecuteScalar());
            }

            return result;
        }

        /// <summary>
        /// Gets a connection to a specific database from the update context creating and storing one if it does not exist.
        /// </summary>
        /// <param name="context">The update context to use.</param>
        /// <param name="databaseName">The name of the database to target.</param>
        /// <returns>The connection.</returns>
        public static IDbConnection GetDatabaseConnection(this UpdateContext context, string databaseName)
        {
            IDbConnection connection;

            string variableName = string.Format("{0}-{1}", ConnectionVariableName, databaseName);

            object connectionObject;
            if (context.Variables.TryGetValue(variableName, out connectionObject))
            {
                connection = (IDbConnection)connectionObject;
            }
            else
            {
                connection = new SqlConnection(context.GetDatabaseConnectionString(databaseName));
                context.Variables[variableName] = connection;
            }

            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            return connection;
        }

        /// <summary>
        /// Gets a connection string to a specific database.
        /// </summary>
        /// <param name="context">The update context to use.</param>
        /// <param name="databaseName">The name of the database to target.</param>
        /// <returns>The connection string.</returns>
        public static string GetDatabaseConnectionString(this UpdateContext context, string databaseName)
        {
            var builder = new SqlConnectionStringBuilder(context.GetDefaultConnectionString())
            {
                InitialCatalog = databaseName
            };

            return builder.ConnectionString;
        }

        /// <summary>
        /// Executes a SQL statement expecting a result set.
        /// </summary>
        /// <param name="context">The context to use.</param>
        /// <param name="sqlFormat">The format of the SQL statement to execute.</param>
        /// <param name="args">The arguments to use with the format string.</param>
        /// <returns>The result set.</returns>
        public static DataTable ExecuteDefaultDataSetText(this UpdateContext context, string sqlFormat, params object[] args)
        {
            return context.ExecuteDefaultDataSetText(string.Format(sqlFormat, args));
        }

        /// <summary>
        /// Executes a SQL statement expecting a result set.
        /// </summary>
        /// <param name="context">The context to use.</param>
        /// <param name="sql">The SQL statement to execute.</param>
        /// <returns>The result set.</returns>
        public static DataTable ExecuteDefaultDataSetText(this UpdateContext context, string sql)
        {
            DataTable table;
            var connection = context.GetDefaultConnection();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = sql;
                command.CommandType = CommandType.Text;
                command.Connection = connection;

                table = ExecuteAction<DataTable>(
                    command,
                    localCommand =>
                        {
                            var localTable = new DataTable();
                            localTable.Load(localCommand.ExecuteReader());
                            return localTable;
                        });
            }

            return table;
        }

        /// <summary>
        /// Executes a non-query SQL statement.
        /// </summary>
        /// <param name="context">The context to use.</param>
        /// <param name="sqlFormat">The format of the SQL statement to execute.</param>
        /// <param name="args">The arguments to use with the format string.</param>
        public static void ExecuteDefaultNonQueryText(this UpdateContext context, string sqlFormat, params object[] args)
        {
            context.ExecuteDefaultNonQueryText(string.Format(sqlFormat, args));
        }

        /// <summary>
        /// Executes a non-query SQL statement.
        /// </summary>
        /// <param name="context">The context to use.</param>
        /// <param name="sql">The SQL statement to execute.</param>
        public static void ExecuteDefaultNonQueryText(this UpdateContext context, string sql)
        {
            var connection = context.GetDefaultConnection();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = sql;
                command.CommandType = CommandType.Text;
                command.Connection = connection;

                ExecuteAction<object>(
                    command,
                    localCommand =>
                        {
                            localCommand.ExecuteNonQuery();
                            return null;
                        });
            }
        }

        /// <summary>
        /// Executes a scalar SQL statement.
        /// </summary>
        /// <param name="context">The context to use.</param>
        /// <param name="sqlFormat">The format of the SQL statement to execute.</param>
        /// <param name="args">The arguments to use with the format string.</param>
        /// <returns>The scalar result.</returns>
        public static object ExecuteDefaultScalarText(this UpdateContext context, string sqlFormat, params object[] args)
        {
            return context.ExecuteDefaultScalarText(string.Format(sqlFormat, args));
        }

        /// <summary>
        /// Executes a scalar SQL statement.
        /// </summary>
        /// <param name="context">The context to use.</param>
        /// <param name="sql">The SQL statement to execute.</param>
        /// <returns>The scalar result.</returns>
        public static object ExecuteDefaultScalarText(this UpdateContext context, string sql)
        {
            var connection = context.GetDefaultConnection();

            object result;
            using (var command = connection.CreateCommand())
            {
                command.CommandText = sql;
                command.CommandType = CommandType.Text;
                command.Connection = connection;

                result = ExecuteAction(command, localCommand => localCommand.ExecuteScalar());
            }

            return result;
        }

        /// <summary>
        /// Gets an connection from the context, creating and storing one if it does not exist.
        /// </summary>
        /// <param name="context">The context to use.</param>
        /// <returns>A database connection.</returns>
        public static IDbConnection GetDefaultConnection(this UpdateContext context)
        {
            IDbConnection connection;

            object connectionObject;
            if (context.Variables.TryGetValue(ConnectionVariableName, out connectionObject))
            {
                connection = (IDbConnection)connectionObject;
            }
            else
            {
                connection = new SqlConnection(context.GetDefaultConnectionString());
                context.Variables[ConnectionVariableName] = connection;
            }

            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            return connection;
        }

        /// <summary>
        /// Gets the database name from the default connection string returned by the store.
        /// </summary>
        /// <param name="context">The update context to use.</param>
        /// <returns>The name of the default database.</returns>
        public static string GetDefaultDatabase(this UpdateContext context)
        {
            var builder = new SqlConnectionStringBuilder(context.GetDefaultConnectionString());
            return builder.InitialCatalog;
        }

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <param name="context">The update context to use.</param>
        /// <returns>The connection string.</returns>
        public static string GetDefaultConnectionString(this UpdateContext context)
        {
            return context.GetVariableString(UpdateContextVariableName.ConnectionString);
        }

        /// <summary>
        /// Executes the specified command throwing a detailed exception if it fails.
        /// </summary>
        /// <typeparam name="T">The type of scalar value to expect.</typeparam>
        /// <param name="command">The command to execute.</param>
        /// <param name="action">The action to perform with the command.</param>
        /// <returns>The scalar result.</returns>
        private static T ExecuteAction<T>(IDbCommand command, Func<IDbCommand, T> action)
        {
            T result;
            try
            {
                result = action(command);
            }
            catch (Exception ex)
            {
                string message = string.Format(
                    "Failed to execute non query: {0}",
                    GetCommandDescription(command));
                throw new Exception(message, ex);
            }

            return result;
        }

        /// <summary>
        /// Gets a description of the specified command.
        /// </summary>
        /// <param name="command">The command to describe.</param>
        /// <returns>The command description.</returns>
        private static string GetCommandDescription(IDbCommand command)
        {
            return command.CommandText;
        }
    }
}

// <copyright file="SqlServerStore.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Data.SqlServer
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.Text;
    using System.Text.RegularExpressions;

    using NeoMem2.Automation.Updates;
    using NeoMem2.Automation.Updates.Data;
    using NeoMem2.Core;
    using NeoMem2.Data.SqlServer.Updates;

    using NLog;

    /// <summary>
    /// A store that uses SQL Server for storage.
    /// </summary>
    public class SqlServerStore : AdoStoreBase
    {
        /// <summary>
        /// The logger to use in this class.
        /// </summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlServerStore" /> class.
        /// </summary>
        /// <param name="connectionString">The connection string to use to connect to the database.</param>
        /// <exception cref="ArgumentException">
        /// Thrown if a required argument has an invalid value.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if a required argument has a null reference.
        /// </exception>
        public SqlServerStore(string connectionString)
            : base(new SqlServerDataAccess(connectionString))
        {
        }

        protected override IObjectDataAccess<Note> CreateNoteDataAccess(IAdoDataAccess dataAccess)
        {
            return new AdoNoteDataAccess(dataAccess);
        }

        /// <summary>
        /// Gets a default connection string for this store.
        /// </summary>
        /// <returns>The connection string.</returns>
        public static string GetDefaultConnectionString()
        {
            return "server=.;database=NeoMem2;trusted_connection=true";
        }

        /// <summary>
        /// Backs up the store to the specified file.
        /// </summary>
        /// <param name="backupFile">The file to backup to.</param>
        public override void Backup(string backupFile)
        {
            string databaseName = Convert.ToString(this.ExecuteScalarCommandText("select Db_Name()"));
            var sql = new StringBuilder();
            sql.AppendFormat("BACKUP DATABASE [{0}]", databaseName);
            sql.AppendFormat(" TO  DISK = N'{0}'", backupFile);
            sql.Append(" WITH NOFORMAT, NOINIT");
            sql.AppendFormat(",  NAME = N'{0}-Full Database Backup'", databaseName);
            sql.Append(", SKIP, NOREWIND, NOUNLOAD, COMPRESSION, STATS = 10");

            this.ExecuteNonQueryCommandText(sql.ToString());
        }

        /// <summary>
        /// Creates a new store of the type managed by this class.
        /// </summary>
        /// <param name="recreate">Set to true to remove and recreate it if it already exists.</param>
        public override void CreateNewStore(bool recreate = false)
        {
            var updates = this.GetUpdates();
            foreach (var update in updates)
            {
                var manager = new ManualUpdateManager(update.Item2);
                manager.Update(update.Item1);
                update.Item2.Dispose();
            }
        }

        /// <summary>
        /// Gets any updates that are applicable to bring this store up to the latest version.
        /// </summary>
        /// <returns>The applicable updates.</returns>
        public override List<Tuple<ComponentInfo, UpdateContext>> GetUpdates()
        {
            var context = new UpdateContext();
            context.Variables[UpdateContextVariableName.ConnectionString] = this.DataAccess.ConnectionString;

            var components = new UpdateDiscoverer().Discover(
                Constants.NeoMem2DatabaseComponent,
                this.GetCurrentDatabaseVersion(context));

            var componentUpdates = new List<Tuple<ComponentInfo, UpdateContext>>();
            if (components.Count > 0)
            {
                componentUpdates.Add(new Tuple<ComponentInfo, UpdateContext>(components[0], context));
            }
            else
            {
                context.Dispose();
            }

            return componentUpdates;
        }
        
        /// <summary>
        /// Gets the database schema version.
        /// </summary>
        /// <param name="context">The context of the query.</param>
        /// <returns>The database schema version.</returns>
        private double GetCurrentDatabaseVersion(UpdateContext context)
        {
            string databaseToCreate = context.GetDefaultDatabase();
            int databaseCount = (int)context.ExecuteDatabaseScalarText(
                "master",
                "SELECT COUNT(*) FROM sys.databases WHERE name = '{0}'",
                databaseToCreate);

            double version;
            if (databaseCount > 0)
            {
                object versionObject = context.ExecuteDefaultScalarText(
                    "SELECT value FROM fn_listextendedproperty('{0}', default, default, default, default, default, default)",
                    "Version");
                if (versionObject == null || versionObject == DBNull.Value)
                {
                    version = 0.1;
                }
                else
                {
                    version = Convert.ToDouble(versionObject);
                }
            }
            else
            {
                version = 0;
            }

            return version;
        }
        
        /// <summary>
        /// Executes a query expecting no result returned.
        /// </summary>
        /// <param name="sql">The SQL statement to execute.</param>
        private void ExecuteNonQueryCommandText(string sql)
        {
            using (SqlCommand command = new SqlCommand(sql))
            {
                command.CommandType = CommandType.Text;
                this.ExecuteNonQuery(command);
            }
        }

        /// <summary>
        /// Executes a query expecting no result returned.
        /// </summary>
        /// <param name="command">The command to execute.</param>
        private void ExecuteNonQuery(IDbCommand command)
        {
            string sql = this.GetCommandDescription(command);
            var stopwatch = Stopwatch.StartNew();
            try
            {
                Logger.Trace("About to execute non-query command: {0}", sql);
                using (var connection = this.DataAccess.CreateConnection())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.ExecuteNonQuery();
                    connection.Close();
                }

                Logger.Debug("Executed non-query command: {0} [{1}]", sql, stopwatch.Elapsed);
            }
            catch (Exception ex)
            {
                stopwatch.Stop();

                string message = string.Format(
                    "Failed to execute command: {0} [{1}]",
                    sql,
                    stopwatch.Elapsed);
                throw new Exception(message, ex);
            }
        }

        /// <summary>
        /// Executes a query expecting a scalar result.
        /// </summary>
        /// <param name="sql">The SQL statement to execute.</param>
        /// <returns>The scalar result.</returns>
        private object ExecuteScalarCommandText(string sql)
        {
            object result;
            using (SqlCommand command = new SqlCommand(sql))
            {
                command.CommandType = CommandType.Text;
                result = this.ExecuteScalar(command);
            }

            return result;
        }

        /// <summary>
        /// Executes a query expecting a scalar result.
        /// </summary>
        /// <param name="command">The command to execute.</param>
        /// <returns>The scalar result.</returns>
        private object ExecuteScalar(IDbCommand command)
        {
            object result;
            using (var connection = this.DataAccess.CreateConnection())
            {
                connection.Open();
                command.Connection = connection;
                result = command.ExecuteScalar();
                connection.Close();
            }

            return result;
        }
        
        /// <summary>
        /// Gets a description of the specified command.
        /// </summary>
        /// <param name="command">The command to describe.</param>
        /// <returns>The string representation.</returns>
        private string GetCommandDescription(IDbCommand command)
        {
            return Regex.Replace(
                command.CommandText,
                @"@([a-zA-Z0-9]+)",
                match =>
                    {
                        string parameterName = match.Groups[1].Value;
                        return GetParameterValueForLogging(command, parameterName);
                    });
        }

        /// <summary>
        /// Gets a string representation of a command parameter.
        /// </summary>
        /// <param name="command">The command to get the parameter from.</param>
        /// <param name="parameterName">The name of the parameter.</param>
        /// <returns>The string representation.</returns>
        private string GetParameterValueForLogging(IDbCommand command, string parameterName)
        {
            return command.Parameters.Contains(parameterName)
                ? this.GetParameterValueForLogging((IDataParameter)command.Parameters[parameterName])
                : "[missing parameter value]";
        }

        /// <summary>
        /// Gets a string representation of a command parameter.
        /// </summary>
        /// <param name="parameter">The parameter to describe.</param>
        /// <returns>The string representation.</returns>
        private string GetParameterValueForLogging(IDataParameter parameter)
        {
            string replacement;
            object parameterValue = parameter.Value;
            if (parameterValue == null)
            {
                replacement = "NULL";
            }
            else if (parameterValue is bool)
            {
                replacement = ((bool)parameterValue) ? "1" : "0";
            }
            else if (parameterValue is DateTime || parameterValue is Guid || parameterValue is string)
            {
                replacement = "'" + this.GetParameterValueForLogging(parameterValue.ToString()) + "'";
            }
            else
            {
                replacement = this.GetParameterValueForLogging(parameterValue.ToString());
            }

            return replacement;
        }

        /// <summary>
        /// Gets a string representation of a command parameter value.
        /// </summary>
        /// <param name="parameterValue">The parameter value to describe.</param>
        /// <returns>The string representation.</returns>
        private string GetParameterValueForLogging(string parameterValue)
        {
            const int MaxLength = 50;

            int newLineIndex = parameterValue.IndexOf(Environment.NewLine, StringComparison.InvariantCulture);
            if (newLineIndex >= 0)
            {
                parameterValue = parameterValue.Substring(0, newLineIndex) + "...";
            }

            if (parameterValue.Length > MaxLength)
            {
                parameterValue = parameterValue.Substring(0, MaxLength) + "...";
            }

            return parameterValue;
        }
    }
}

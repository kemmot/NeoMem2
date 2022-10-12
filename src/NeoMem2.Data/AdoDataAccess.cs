// <copyright file="AdoDataAccess.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Data
{
    using System;
    using System.Data;
    using System.Diagnostics;
    using System.Text.RegularExpressions;
    using NeoMem2.Data.FluentSql;
    using NLog;

    /// <summary>
    /// An ADO helper class.
    /// </summary>
    public abstract class AdoDataAccess : IAdoDataAccess
    {
        /// <summary>
        /// The logger to use for this class.
        /// </summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Initializes a new instance of the <see cref="AdoDataAccess" /> class.
        /// </summary>
        /// <param name="connectionString">The connection string to use for data access.</param>
        public AdoDataAccess(string connectionString)
        {
            if (connectionString == null)
            {
                throw new ArgumentNullException("connectionString");
            }

            if (connectionString.Length == 0)
            {
                throw new ArgumentException("Argument cannot be zero length", "connectionString");
            }

            this.ConnectionString = connectionString;
        }

        /// <summary>
        /// Gets the connection string to use for data access.
        /// </summary>
        public string ConnectionString { get; }

        ///// <summary>
        ///// Executes a non-query SQL statement.
        ///// </summary>
        ///// <param name="sql">The SQL statement to execute.</param>
        //public void ExecuteNonQueryCommandText(string sql)
        //{
        //    using (var command = CreateCommand(sql))
        //    {
        //        command.CommandType = CommandType.Text;
        //        this.ExecuteNonQuery(command);
        //    }
        //}

        /// <summary>
        /// Executes a non-query command.
        /// </summary>
        /// <param name="command">The command to execute.</param>
        public void ExecuteNonQuery(IDbCommand command)
        {
            using (var context = CreateContext())
            {
                ExecuteNonQuery(command, context);
                context.Close(true);
            }
        }

        /// <summary>
        /// Executes a non-query command.
        /// </summary>
        /// <param name="command">The command to execute.</param>
        /// <param name="context">The database context.</param>
        public void ExecuteNonQuery(IDbCommand command, AdoContext context)
        {
            string sql = this.GetCommandDescription(command);
            var stopwatch = Stopwatch.StartNew();
            try
            {
                Logger.Trace("About to execute non-query command: {0}", sql);
                command.Connection = context.Connection;
                command.Transaction = context.Transaction;
                command.ExecuteNonQuery();
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

        ///// <summary>
        ///// Executes a SQL statement that returns a scalar value.
        ///// </summary>
        ///// <param name="sql">The SQL statement to execute.</param>
        ///// <returns>The scalar result.</returns>
        //public object ExecuteScalarCommandText(string sql)
        //{
        //    object result;
        //    using (var command = CreateCommand(sql))
        //    {
        //        command.CommandType = CommandType.Text;
        //        result = this.ExecuteScalar(command);
        //    }

        //    return result;
        //}

        /// <summary>
        /// Executes a command that returns a scalar value.
        /// </summary>
        /// <param name="command">The command to execute.</param>
        /// <returns>The scalar result.</returns>
        public object ExecuteScalar(IDbCommand command)
        {
            object result;
            using (var context = CreateContext())
            {
                result = ExecuteScalar(command);
                context.Close(true);
            }

            return result;
        }

        /// <summary>
        /// Executes a command that returns a scalar value.
        /// </summary>
        /// <param name="command">The command to execute.</param>
        /// <param name="context">The database context.</param>
        /// <returns>The scalar result.</returns>
        public object ExecuteScalar(IDbCommand command, AdoContext context)
        {
            string sql = this.GetCommandDescription(command, raw:true);
            var stopwatch = Stopwatch.StartNew();
            object result;
            Logger.Trace("About to execute scalar command: {0}", sql);
            try
            {
                command.Connection = context.Connection;
                command.Transaction = context.Transaction;
                result = command.ExecuteScalar();
                Logger.Debug("Executed scalar command: {0} [{1}]", sql, stopwatch.Elapsed);
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

            return result;
        }

        public AdoContext CreateContext(bool transaction = false)
        {
            var connection = CreateConnection();
            connection.Open();
            return new AdoContext
            {
                Connection = connection,
                Transaction = transaction ? connection.BeginTransaction() : null
            };
        }

        public IDbConnection CreateConnection(string connectionString)
        {
            IDbConnection connection = CreateConnection();
            connection.ConnectionString = connectionString;
            return connection;
        }

        public abstract IDbConnection CreateConnection();

        public IDbCommand CreateCommand(string commandText)
        {
            IDbCommand command = CreateCommand();
            command.CommandText = commandText;
            return command;
        }

        public IDbCommand CreateCommand(string commandText, IDbConnection connection)
        {
            IDbCommand command = CreateCommand(commandText);
            command.Connection = connection;
            return command;
        }

        public abstract IDbCommand CreateCommand();

        public IDataParameter CreateParameter(string name, object value)
        {
            var parameter = CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value;
            return parameter;
        }

        public abstract IDataParameter CreateParameter();

        /// <summary>
        /// Gets a string representation of a command.
        /// </summary>
        /// <param name="command">The command to return the string representation of.</param>
        /// <returns>The string representation.</returns>
        public string GetCommandDescription(IDbCommand command, bool raw = false)
        {
            string description;
            if (raw)
            {
                description = command.CommandText;
                foreach (IDbDataParameter parameter in command.Parameters)
                {
                    description += $" @{parameter.ParameterName}={GetParameterValueForLogging(parameter)}";
                }
            }
            else
            {
                description = Regex.Replace(
                    command.CommandText,
                    @"@([a-zA-Z0-9]+)",
                    match =>
                    {
                        string parameterName = match.Groups[1].Value;
                        return this.GetParameterValueForLogging(command, parameterName);
                    });
            }

            return description;
        }

        /// <summary>
        /// Gets a string representation of a command parameter.
        /// </summary>
        /// <param name="command">The command to return the string representation of.</param>
        /// <param name="parameterName">The name of the parameter to return the string representation of.</param>
        /// <returns>The string representation.</returns>
        private string GetParameterValueForLogging(IDbCommand command, string parameterName)
        {
            return command.Parameters.Contains(parameterName)
                ? this.GetParameterValueForLogging((IDbDataParameter)command.Parameters[parameterName])
                : "[missing parameter value]";
        }

        /// <summary>
        /// Gets a string representation of a command parameter.
        /// </summary>
        /// <param name="parameter">The parameter to return the string representation of.</param>
        /// <returns>The string representation.</returns>
        private string GetParameterValueForLogging(object parameter)
        {
            string replacement;
            var stronglyTypedParameter = parameter as IDbDataParameter;
            object parameterValue;
            if (stronglyTypedParameter != null)
            {
                parameterValue = stronglyTypedParameter.Value;
            }
            else
            {
                parameterValue = parameter;
            }

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
        /// Gets a string representation of a parameter value.
        /// </summary>
        /// <param name="parameterValue">The parameter value to return the string representation of.</param>
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
        
        public abstract IProviderSqlStatementBuilder GetProviderSqlStatementBuilder();
    }
}
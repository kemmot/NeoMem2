// <copyright file="SqlStatementBuilder.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Data.FluentSql
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;

    /// <summary>
    /// A base class for fluent SQL statement builders.
    /// </summary>
    public class SqlStatementBuilder
    {
        /// <summary>
        /// The date format to use.
        /// </summary>
        public const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// The parameters to add to the command.
        /// </summary>
        private readonly List<IDataParameter> parameters = new List<IDataParameter>();

        /// <summary>
        /// The backing field for the <see cref="Sql"/> property.
        /// </summary>
        private readonly StringBuilder sql = new StringBuilder();

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlStatementBuilder" /> class.
        /// </summary>
        protected SqlStatementBuilder()
            : this(new StringBuilder())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlStatementBuilder" /> class.
        /// </summary>
        /// <param name="sql">The StringBuilder to use internally when constructing the statement.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if a required argument has a null reference.
        /// </exception>
        protected SqlStatementBuilder(StringBuilder sql)
        {
            if (sql == null)
            {
                throw new ArgumentNullException("sql");
            }

            this.sql = sql;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the statement is complete.
        /// </summary>
        public bool IsComplete { get; protected set; }

        /// <summary>
        /// Gets the StringBuilder being used internally to construct the statement.
        /// </summary>
        protected StringBuilder Sql
        {
            get { return this.sql; }
        }

        /// <summary>
        /// Creates an instance of an insert statement builder.
        /// </summary>
        /// <param name="tableName">The table to insert into.</param>
        /// <returns>The statement builder.</returns>
        public static InsertSqlStatementBuilder Insert(string tableName)
        {
            return new InsertSqlStatementBuilder(tableName);
        }

        /// <summary>
        /// Creates an instance of a select statement builder.
        /// </summary>
        /// <returns>The statement builder.</returns>
        public static SelectSqlStatementBuilder Select()
        {
            return new SelectSqlStatementBuilder();
        }

        /// <summary>
        /// Gets the fully populated command.
        /// </summary>
        /// <returns>The command.</returns>
        public virtual IDbCommand GetCommand(IAdoDataAccess dataAccess)
        {
            var command = dataAccess.CreateCommand(this.ToString());
            foreach (var parameter in this.parameters.ToArray())
            {
                command.Parameters.Add(parameter);
            }

            return command;
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return this.sql.ToString();
        }

        /// <summary>
        /// Adds a parameter to the internal command.
        /// </summary>
        /// <param name="parameter">The parameter to add.</param>
        protected void AddParameter(IDataParameter parameter)
        {
            this.parameters.Add(parameter);
        }
    }
}

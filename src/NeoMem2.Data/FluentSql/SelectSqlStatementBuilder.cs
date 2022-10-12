// <copyright file="SelectSqlStatementBuilder.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Data.FluentSql
{
    using System;
    using System.Text;


    public interface ISelectSqlStatementBuilder
    {
    }

    /// <summary>
    /// A statement builder for constructing select statements.
    /// </summary>
    public class SelectSqlStatementBuilder : SqlStatementBuilder, ISelectSqlStatementBuilder
    {
        /// <summary>
        /// Whether to include the 'where' keyword in the resulting statement.
        /// </summary>
        private bool includesWhereKeyword;

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectSqlStatementBuilder" /> class.
        /// </summary>
        public SelectSqlStatementBuilder()
        {
            Sql.Append("SELECT");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectSqlStatementBuilder" /> class.
        /// </summary>
        /// <param name="sql">The string builder to use internally.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if a required argument has a null reference.
        /// </exception>
        protected SelectSqlStatementBuilder(StringBuilder sql)
            : base(sql)
        {
            this.Sql.Append("SELECT");
        }

        /// <summary>
        /// The supported ways of combining conditions.
        /// </summary>
        public enum WhereConditionCombination
        {
            /// <summary>
            /// Logical and.
            /// </summary>
            And,

            /// <summary>
            /// Logical or.
            /// </summary>
            Or
        }

        /// <summary>
        /// Adds the all column wildcard to the result set column list.
        /// </summary>
        /// <returns>A statement builder representing the current state of the statement.</returns>
        public SelectSqlStatementBuilder AllColumns()
        {
            this.Sql.Append(" *");
            return this;
        }

        /// <summary>
        /// Adds a column to the result set column list.
        /// </summary>
        /// <param name="columnName">The name of the column to return.</param>
        /// <param name="alias">Any alias to give to the column.</param>
        /// <returns>A statement builder representing the current state of the statement.</returns>
        public SelectSqlStatementBuilder Column(string columnName, string alias = "", bool isFunction = false)
        {
            string openBrace = isFunction ? string.Empty : "[";
            string closeBrace = isFunction ? string.Empty : "]";
            this.Sql.AppendFormat(" {0}{1}{2}", openBrace, columnName, closeBrace);

            if (!string.IsNullOrEmpty(alias))
            {
                this.Sql.AppendFormat(" AS [{0}]", alias);
            }

            return this;
        }

        /// <summary>
        /// Adds a function call to the result set column list.
        /// </summary>
        /// <param name="functionName">The name of the function to call.</param>
        /// <returns>A statement builder representing the current state of the statement.</returns>
        public SelectSqlStatementBuilder Function(string functionName)
        {
            this.Sql.AppendFormat(" {0}", functionName);
            return this;
        }

        /// <summary>
        /// Adds the table to query from.
        /// </summary>
        /// <param name="tableName">The name of the table to query.</param>
        /// <returns>A statement builder representing the current state of the statement.</returns>
        public SelectSqlStatementBuilder From(string tableName)
        {
            this.Sql.AppendFormat(" FROM [{0}]", tableName);
            return this;
        }

        /// <summary>
        /// Adds a where condition to the statement.
        /// </summary>
        /// <param name="condition">The condition to add.</param>
        /// <param name="conditionCombination">How to combine the condition with a previous condition.</param>
        /// <returns>A statement builder representing the current state of the statement.</returns>
        public SelectSqlStatementBuilder Where(string condition, WhereConditionCombination conditionCombination = WhereConditionCombination.And)
        {
            if (!this.includesWhereKeyword)
            {
                this.Sql.Append(" WHERE ");
                this.includesWhereKeyword = true;
            }
            else
            {
                string conditionCombinationText;
                switch (conditionCombination)
                {
                    case WhereConditionCombination.And:
                        conditionCombinationText = "AND";
                        break;
                    case WhereConditionCombination.Or:
                        conditionCombinationText = "OR";
                        break;
                    default:
                        string message = "Where condition combination not supported" + conditionCombination;
                        throw new NotSupportedException(message);
                }

                this.Sql.AppendFormat(" {0} ", conditionCombinationText);
            }

            this.Sql.Append(condition);
            return this;
        }
    }
}
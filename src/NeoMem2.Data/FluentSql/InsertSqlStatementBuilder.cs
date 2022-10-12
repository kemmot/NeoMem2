// <copyright file="InsertSqlStatementBuilder.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Data.FluentSql
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;

    /// <summary>
    /// A statement builder for constructing insert statements.
    /// </summary>
    public class InsertSqlStatementBuilder : SqlStatementBuilder
    {
        /// <summary>
        /// The columns that will be inserted into.
        /// </summary>
        private readonly List<Tuple<string, string>> columnValues = new List<Tuple<string, string>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="InsertSqlStatementBuilder" /> class.
        /// </summary>
        /// <param name="tableName">The name of the table to insert into.</param>
        /// <exception cref="ArgumentException">
        /// Thrown if a required argument has an invalid value.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if a required argument has a null reference.
        /// </exception>
        public InsertSqlStatementBuilder(string tableName)
        {
            if (tableName == null)
            {
                throw new ArgumentNullException("tableName");
            }

            if (tableName.Length == 0)
            {
                throw new ArgumentException("Argument cannot be zero length", "tableName");
            }

            this.Sql.AppendFormat("INSERT INTO [{0}]", tableName);
        }

        class ColumnDetails
        {
            public string columnName;
            public object value;
            public bool useParameter;
            public bool isFunction;
        }

        private readonly List<ColumnDetails> columns = new List<ColumnDetails>();

        /// <summary>
        /// Adds a column to the result set.
        /// </summary>
        /// <param name="columnName">The name of the column to add.</param>
        /// <param name="value">The value to insert for the column.</param>
        /// <param name="useParameter">Set to true if <paramref name="value"/> refers to a parameter.</param>
        /// <param name="isFunction">Set to true if <paramref name="value"/> refers to a function.</param>
        /// <returns>A statement builder representing the current state of the statement.</returns>
        public InsertSqlStatementBuilder AddColumn(string columnName, object value, bool useParameter = false, bool isFunction = false)
        {
            columns.Add(new ColumnDetails { columnName = columnName, value = value, useParameter = useParameter, isFunction = isFunction});
            return this;
        }

        public override IDbCommand GetCommand(IAdoDataAccess dataAccess)
        {
            foreach (var column in columns)
            {
                if (column.useParameter)
                {
                    string parameterName = "@" + column.columnName;
                    this.AddParameter(dataAccess.CreateParameter(parameterName, column.value));
                    this.columnValues.Add(new Tuple<string, string>(column.columnName, parameterName));
                }
                else
                {
                    string valueToUse;
                    if (column.isFunction)
                    {
                        valueToUse = column.value.ToString();
                    }
                    else
                    {
                        if (column.value == null)
                        {
                            valueToUse = "NULL";
                        }
                        else if (column.value is string)
                        {
                            valueToUse = "'" + (string)column.value + "'";
                        }
                        else if (column.value is DateTime?)
                        {
                            var nullableDateTimeValue = (DateTime?)column.value;
                            if (nullableDateTimeValue.HasValue)
                            {
                                valueToUse = string.Format("'{0:" + SqlStatementBuilder.DateTimeFormat + "}'", column.value);
                            }
                            else
                            {
                                valueToUse = "NULL";
                            }
                        }
                        else if (column.value is DateTime)
                        {
                            valueToUse = string.Format("'{0:" + SqlStatementBuilder.DateTimeFormat + "}'", column.value);
                        }
                        else
                        {
                            valueToUse = column.value.ToString();
                        }
                    }

                    this.columnValues.Add(new Tuple<string, string>(column.columnName, valueToUse));
                }
            }

            return base.GetCommand(dataAccess);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            var columns = new StringBuilder();
            columns.Append(" ");

            using (new ParenthesisBlock(columns))
            {
                for (int columnIndex = 0; columnIndex < this.columnValues.Count; columnIndex++)
                {
                    if (columnIndex > 0)
                    {
                        columns.Append(", ");
                    }

                    columns.AppendFormat("[{0}]", this.columnValues[columnIndex].Item1);
                }
            }

            columns.Append(" VALUES ");

            using (new ParenthesisBlock(columns))
            {
                for (int columnIndex = 0; columnIndex < this.columnValues.Count; columnIndex++)
                {
                    if (columnIndex > 0)
                    {
                        columns.Append(", ");
                    }

                    columns.Append(this.columnValues[columnIndex].Item2);
                }
            }

            return base.ToString() + columns;
        }
    }
}

// <copyright file="ParenthesisBlock.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Data.FluentSql
{
    using System;
    using System.Text;

    /// <summary>
    /// A statement builder block for creating block bound by parenthesis.
    /// </summary>
    public class ParenthesisBlock : StatementBuilderBlock
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParenthesisBlock" /> class.
        /// </summary>
        /// <param name="sql">The StringBuilder to use internally.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if a required argument has a null reference.
        /// </exception>
        public ParenthesisBlock(StringBuilder sql)
            : base(sql, "(", ")")
        {
        }
    }
}
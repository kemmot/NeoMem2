// <copyright file="StatementBuilderBlock.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Data.FluentSql
{
    using System;
    using System.Text;

    using NeoMem2.Utils;

    /// <summary>
    /// A base class for defining statement blocks that are bounded by opening and closing sections.
    /// </summary>
    public class StatementBuilderBlock : DisposableBase
    {
        /// <summary>
        /// The string to use to close the block.
        /// </summary>
        private readonly string closeBlock;

        /// <summary>
        /// The string to use to open the block.
        /// </summary>
        private readonly string openBlock;

        /// <summary>
        /// The StringBuilder to use internally.
        /// </summary>
        private readonly StringBuilder sql;

        /// <summary>
        /// Initializes a new instance of the <see cref="StatementBuilderBlock" /> class.
        /// </summary>
        /// <param name="sql">The StringBuilder to use internally.</param>
        /// <param name="openBlock">The string to use to open the block.</param>
        /// <param name="closeBlock">The string to use to close the block.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if a required argument has a null reference.
        /// </exception>
        protected StatementBuilderBlock(StringBuilder sql, string openBlock, string closeBlock)
        {
            if (sql == null)
            {
                throw new ArgumentNullException("sql");
            }

            this.sql = sql;
            this.openBlock = openBlock;
            this.closeBlock = closeBlock;

            this.OpenBlock();
        }

        /// <summary>
        /// Dispose managed resources.
        /// </summary>
        protected override void DisposeManagedResources()
        {
            this.CloseBlock();
            base.DisposeManagedResources();
        }

        /// <summary>
        /// Writes the open block string.
        /// </summary>
        private void OpenBlock()
        {
            this.sql.Append(this.openBlock);
        }

        /// <summary>
        /// Writes the close block string.
        /// </summary>
        private void CloseBlock()
        {
            this.sql.Append(this.closeBlock);
        }
    }
}
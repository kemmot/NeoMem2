// <copyright file="Step04DisableNullsInNamespaceColumn.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Data.SqlServer.Updates.Version_0.Version_0_7
{
    using NeoMem2.Automation.Updates;
    using NeoMem2.Automation.Updates.Data;

    /// <summary>
    /// Update to version 0.7 step 4.
    /// </summary>
    [UpdateStep(Constants.NeoMem2DatabaseComponent, 0.7, 4, "Disable null values in Note.Namespace column")]
    public class Step04DisableNullsInNamespaceColumn : DatabaseUpdateStep
    {
        /// <summary>
        /// The SQL statement to execute for change 1.
        /// </summary>
        private const string Sql1 = "ALTER TABLE [dbo].[Note] ALTER COLUMN [Namespace] VARCHAR(50) NOT NULL";

        /// <summary>
        /// Executes this update step.
        /// </summary>
        public override void Execute()
        {
            Context.ExecuteDefaultNonQueryText(Sql1);
        }
    }
}

// <copyright file="Step01AddNamespaceColumn.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Data.SqlServer.Updates.Version_0.Version_0_7
{
    using NeoMem2.Automation.Updates;
    using NeoMem2.Automation.Updates.Data;

    /// <summary>
    /// Update to version 0.7 step 1.
    /// </summary>
    [UpdateStep(Constants.NeoMem2DatabaseComponent, 0.7, 1, "Add Note.Namespace column")]
    public class Step01AddNamespaceColumn : DatabaseUpdateStep
    {
        /// <summary>
        /// The SQL statement to execute for change 1.
        /// </summary>
        private const string Sql1 = "ALTER TABLE dbo.Note ADD Namespace VARCHAR(50) NULL";

        /// <summary>
        /// Executes this update step.
        /// </summary>
        public override void Execute()
        {
            Context.ExecuteDefaultNonQueryText(Sql1);
        }
    }
}

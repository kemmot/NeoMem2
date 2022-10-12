// <copyright file="Step01AddNoteDeletedDateColumn.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Data.SqlServer.Updates.Version_1.Version_1_1
{
    using NeoMem2.Automation.Updates;
    using NeoMem2.Automation.Updates.Data;

    /// <summary>
    /// Update to version 1.1 step 1.
    /// </summary>
    [UpdateStep(Constants.NeoMem2DatabaseComponent, 1.1, 1, "Add Note.DeletedDate Column")]
    public class Step01AddNoteDeletedDateColumn : DatabaseUpdateStep
    {
        /// <summary>
        /// The SQL statement to execute for change 1.
        /// </summary>
        private const string Sql1 = "ALTER TABLE dbo.Note ADD DeletedDate DATETIME NULL";

        /// <summary>
        /// Executes this update step.
        /// </summary>
        public override void Execute()
        {
            Context.ExecuteDefaultNonQueryText(Sql1);
        }
    }
}

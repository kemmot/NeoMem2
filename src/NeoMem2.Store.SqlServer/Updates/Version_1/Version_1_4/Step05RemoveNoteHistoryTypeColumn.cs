// <copyright file="Step05RemoveNoteHistoryTypeColumn.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Data.SqlServer.Updates.Version_1.Version_1_4
{
    using NeoMem2.Automation.Updates;
    using NeoMem2.Automation.Updates.Data;

    /// <summary>
    /// Update to version 1.4 step 5.
    /// </summary>
    [UpdateStep(Constants.NeoMem2DatabaseComponent, 1.4, 5, "Remove NoteHistory.ChangeType column")]
    public class Step05RemoveNoteHistoryTypeColumn : DatabaseUpdateStep
    {
        /// <summary>
        /// The SQL statement to execute for change 1.
        /// </summary>
        private const string Sql = "ALTER TABLE dbo.NoteHistory DROP COLUMN ChangeType";

        /// <summary>
        /// Executes this update step.
        /// </summary>
        public override void Execute()
        {
            Context.ExecuteDefaultNonQueryText(Sql);
        }
    }
}

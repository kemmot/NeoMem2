// <copyright file="Step06DisableNullsInNoteHistoryTypes.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Data.SqlServer.Updates.Version_1.Version_1_4
{
    using NeoMem2.Automation.Updates;
    using NeoMem2.Automation.Updates.Data;

    /// <summary>
    /// Update to version 1.4 step 6.
    /// </summary>
    [UpdateStep(Constants.NeoMem2DatabaseComponent, 1.4, 6, "Disable nulls in NoteHistory.ChangeTypeId column")]
    public class Step06DisableNullsInNoteHistoryTypes : DatabaseUpdateStep
    {
        /// <summary>
        /// The SQL statement to execute for change 1.
        /// </summary>
        private const string Sql = "ALTER TABLE [dbo].[NoteHistory] ALTER COLUMN [ChangeTypeId] INT NOT NULL";

        /// <summary>
        /// Executes this update step.
        /// </summary>
        public override void Execute()
        {
            Context.ExecuteDefaultNonQueryText(Sql);
        }
    }
}

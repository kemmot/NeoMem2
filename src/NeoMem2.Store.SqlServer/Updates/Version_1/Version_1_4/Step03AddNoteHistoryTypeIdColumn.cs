// <copyright file="Step03AddNoteHistoryTypeIdColumn.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Data.SqlServer.Updates.Version_1.Version_1_4
{
    using NeoMem2.Automation.Updates;
    using NeoMem2.Automation.Updates.Data;

    /// <summary>
    /// Update to version 1.4 step 3.
    /// </summary>
    [UpdateStep(Constants.NeoMem2DatabaseComponent, 1.4, 3, "Add NoteHistory.ChangeTypeId column")]
    public class Step03AddNoteHistoryTypeIdColumn : DatabaseUpdateStep
    {
        /// <summary>
        /// The SQL statement to execute for change 1.
        /// </summary>
        private const string Sql1 = "ALTER TABLE [dbo].[NoteHistory] ADD ChangeTypeId INT NULL";

        /// <summary>
        /// The SQL statement to execute for change 2.
        /// </summary>
        private const string Sql2 = @"
            ALTER TABLE dbo.NoteHistory ADD CONSTRAINT
                FK_NoteHistory_ChangeTypeId FOREIGN KEY (ChangeTypeId) REFERENCES [dbo].[NoteHistoryType] (Id)
                ON UPDATE NO ACTION ON DELETE NO ACTION";

        /// <summary>
        /// Executes this update step.
        /// </summary>
        public override void Execute()
        {
            Context.ExecuteDefaultNonQueryText(Sql1);
            Context.ExecuteDefaultNonQueryText(Sql2);
        }
    }
}

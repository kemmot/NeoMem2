// <copyright file="Step02InsertNoteHistoryTypes.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Data.SqlServer.Updates.Version_1.Version_1_4
{
    using NeoMem2.Automation.Updates;
    using NeoMem2.Automation.Updates.Data;

    /// <summary>
    /// Update to version 1.4 step 2.
    /// </summary>
    [UpdateStep(Constants.NeoMem2DatabaseComponent, 1.4, 2, "Insert note history types")]
    public class Step02InsertNoteHistoryTypes : DatabaseUpdateStep
    {
        /// <summary>
        /// The SQL statement to execute for change 1.
        /// </summary>
        private const string Sql1 = @"INSERT INTO [dbo].[NoteHistoryType] (Id, Name) VALUES (1, 'Inserted')";

        /// <summary>
        /// The SQL statement to execute for change 2.
        /// </summary>
        private const string Sql2 = @"INSERT INTO [dbo].[NoteHistoryType] (Id, Name) VALUES (2, 'Updated')";

        /// <summary>
        /// The SQL statement to execute for change 3.
        /// </summary>
        private const string Sql3 = @"INSERT INTO [dbo].[NoteHistoryType] (Id, Name) VALUES (3, 'Deleted')";

        /// <summary>
        /// Executes this update step.
        /// </summary>
        public override void Execute()
        {
            Context.ExecuteDefaultNonQueryText(Sql1);
            Context.ExecuteDefaultNonQueryText(Sql2);
            Context.ExecuteDefaultNonQueryText(Sql3);
        }
    }
}

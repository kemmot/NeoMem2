// <copyright file="Step04MigrateNoteHistoryTypes.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Data.SqlServer.Updates.Version_1.Version_1_4
{
    using NeoMem2.Automation.Updates;
    using NeoMem2.Automation.Updates.Data;

    /// <summary>
    /// Update to version 1.4 step 4.
    /// </summary>
    [UpdateStep(Constants.NeoMem2DatabaseComponent, 1.4, 4, "Migrate note history types")]
    public class Step04MigrateNoteHistoryTypes : DatabaseUpdateStep
    {
        /// <summary>
        /// The SQL statement to execute for change 1.
        /// </summary>
        private const string Sql = @"
            UPDATE [dbo].[NoteHistory] SET ChangeTypeId = nht.Id
            FROM [dbo].[NoteHistoryType] nht
            JOIN [dbo].[NoteHistory] nh ON nh.ChangeType = nht.Name";

        /// <summary>
        /// Executes this update step.
        /// </summary>
        public override void Execute()
        {
            Context.ExecuteDefaultNonQueryText(Sql);
        }
    }
}

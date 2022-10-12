// <copyright file="Step05RemoveNoteTagColumn.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Data.SqlServer.Updates.Version_1.Version_1_2
{
    using NeoMem2.Automation.Updates;
    using NeoMem2.Automation.Updates.Data;

    /// <summary>
    /// Update to version 1.2 step 5.
    /// </summary>
    [UpdateStep(Constants.NeoMem2DatabaseComponent, 1.2, 5, "Drop Note.Tags column")]
    public class Step05RemoveNoteTagColumn : DatabaseUpdateStep
    {
        /// <summary>
        /// The SQL statement to execute for change 1.
        /// </summary>
        private const string Sql1 = @"ALTER TABLE [dbo].[Note] DROP CONSTRAINT [DF_Note_Tags]";

        /// <summary>
        /// The SQL statement to execute for change 2.
        /// </summary>
        private const string Sql2 = "ALTER TABLE [dbo].[Note] DROP COLUMN [Tags]";

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

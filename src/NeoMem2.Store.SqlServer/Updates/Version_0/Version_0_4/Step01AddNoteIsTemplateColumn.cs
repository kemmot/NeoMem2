// <copyright file="Step01AddNoteIsTemplateColumn.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Data.SqlServer.Updates.Version_0.Version_0_4
{
    using NeoMem2.Automation.Updates;
    using NeoMem2.Automation.Updates.Data;

    /// <summary>
    /// Update to version 0.4 step 1.
    /// </summary>
    [UpdateStep(Constants.NeoMem2DatabaseComponent, 0.4, 1, "Add Note.IsTemplate column")]
    public class Step01AddNoteIsTemplateColumn : DatabaseUpdateStep
    {
        /// <summary>
        /// The SQL statement to execute for change 1.
        /// </summary>
        private const string Sql1 = "ALTER TABLE dbo.Note ADD IsTemplate bit NOT NULL CONSTRAINT DF_Note_IsTemplate DEFAULT 0";

        /// <summary>
        /// Executes this update step.
        /// </summary>
        public override void Execute()
        {
            Context.ExecuteDefaultNonQueryText(Sql1);
        }
    }
}

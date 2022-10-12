// <copyright file="Step01AddClassColumn.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Data.SqlServer.Updates.Version_0.Version_0_8
{
    using NeoMem2.Automation.Updates;
    using NeoMem2.Automation.Updates.Data;

    /// <summary>
    /// Update to version 0.8 step 1.
    /// </summary>
    [UpdateStep(Constants.NeoMem2DatabaseComponent, 0.8, 1, "Add Note.Class column")]
    public class Step01AddClassColumn : DatabaseUpdateStep
    {
        /// <summary>
        /// The SQL statement to execute for change 1.
        /// </summary>
        private const string Sql1 = "ALTER TABLE dbo.Note ADD Class VARCHAR(50) NOT NULL DEFAULT ''";

        /// <summary>
        /// Executes this update step.
        /// </summary>
        public override void Execute()
        {
            Context.ExecuteDefaultNonQueryText(Sql1);
        }
    }
}

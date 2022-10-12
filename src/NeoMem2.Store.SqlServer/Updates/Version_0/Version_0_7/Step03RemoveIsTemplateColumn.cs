// <copyright file="Step03RemoveIsTemplateColumn.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Data.SqlServer.Updates.Version_0.Version_0_7
{
    using NeoMem2.Automation.Updates;
    using NeoMem2.Automation.Updates.Data;

    /// <summary>
    /// Update to version 0.7 step 3.
    /// </summary>
    [UpdateStep(Constants.NeoMem2DatabaseComponent, 0.7, 3, "Remove Note.IsTemplate column")]
    public class Step03RemoveIsTemplateColumn : DatabaseUpdateStep
    {
        /// <summary>
        /// The SQL statement to execute for change 1.
        /// </summary>
        private const string Sql1 = "ALTER TABLE dbo.Note DROP CONSTRAINT DF_Note_IsTemplate";

        /// <summary>
        /// The SQL statement to execute for change 2.
        /// </summary>
        private const string Sql2 = "ALTER TABLE dbo.Note DROP COLUMN IsTemplate";

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

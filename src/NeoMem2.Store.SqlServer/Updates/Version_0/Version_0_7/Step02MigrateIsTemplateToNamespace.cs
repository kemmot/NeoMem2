// <copyright file="Step02MigrateIsTemplateToNamespace.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Data.SqlServer.Updates.Version_0.Version_0_7
{
    using NeoMem2.Automation.Updates;
    using NeoMem2.Automation.Updates.Data;

    /// <summary>
    /// Update to version 0.7 step 2.
    /// </summary>
    [UpdateStep(Constants.NeoMem2DatabaseComponent, 0.7, 2, "Migrate Note.IsTemplate to Note.Namespace")]
    public class Step02MigrateIsTemplateToNamespace : DatabaseUpdateStep
    {
        /// <summary>
        /// The SQL statement to execute for change 1.
        /// </summary>
        private const string Sql1 = "UPDATE Note SET Namespace = 'NoteTemplate' WHERE IsTemplate = 1";

        /// <summary>
        /// The SQL statement to execute for change 2.
        /// </summary>
        private const string Sql2 = "UPDATE Note SET Namespace = 'Note' WHERE Namespace IS NULL";

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

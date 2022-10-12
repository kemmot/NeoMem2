// <copyright file="Step02MigrateIsTemplateToNamespace.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Data.SqlServer.Updates.Version_0.Version_0_8
{
    using NeoMem2.Automation.Updates;
    using NeoMem2.Automation.Updates.Data;

    /// <summary>
    /// Update to version 0.8 step 2.
    /// </summary>
    [UpdateStep(Constants.NeoMem2DatabaseComponent, 0.8, 2, "Migrate note templates to 'Class Template' class")]
    public class Step02MigrateIsTemplateToNamespace : DatabaseUpdateStep
    {
        /// <summary>
        /// The SQL statement to execute for change 1.
        /// </summary>
        private const string Sql1 = "UPDATE Note SET Namespace = 'Class Template' WHERE Namespace = 'NoteTemplate'";

        /// <summary>
        /// Executes this update step.
        /// </summary>
        public override void Execute()
        {
            Context.ExecuteDefaultNonQueryText(Sql1);
        }
    }
}

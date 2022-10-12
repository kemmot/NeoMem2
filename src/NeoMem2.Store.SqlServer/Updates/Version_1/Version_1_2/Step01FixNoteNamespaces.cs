// <copyright file="Step01FixNoteNamespaces.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Data.SqlServer.Updates.Version_1.Version_1_2
{
    using NeoMem2.Automation.Updates;
    using NeoMem2.Automation.Updates.Data;
    using NeoMem2.Core;

    /// <summary>
    /// Update to version 1.2 step 1.
    /// </summary>
    [UpdateStep(Constants.NeoMem2DatabaseComponent, 1.2, 1, "Fix Note Namespaces")]
    public class Step01FixNoteNamespaces : DatabaseUpdateStep
    {
        /// <summary>
        /// Executes this update step.
        /// </summary>
        public override void Execute()
        {
            string sql = string.Format("UPDATE dbo.Note SET Namespace = '{0}' WHERE Namespace = ''", NoteNamespace.Note);
            Context.ExecuteDefaultNonQueryText(sql);
        }
    }
}

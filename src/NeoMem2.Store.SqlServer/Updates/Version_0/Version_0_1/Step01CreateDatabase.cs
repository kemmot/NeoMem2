// <copyright file="Step01CreateDatabase.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Data.SqlServer.Updates.Version_0.Version_0_1
{
    using System.Transactions;

    using NeoMem2.Automation.Updates;
    using NeoMem2.Automation.Updates.Data;

    /// <summary>
    /// Update to version 0.1 step 1.
    /// </summary>
    [UpdateStep(Constants.NeoMem2DatabaseComponent, 0.1, 1, "Create database")]
    public class Step01CreateDatabase : DatabaseUpdateStep
    {
        /// <summary>
        /// Executes this update step.
        /// </summary>
        public override void Execute()
        {
            var databaseToCreate = Context.GetDefaultDatabase();

            using (var scope = new TransactionScope(TransactionScopeOption.Suppress))
            {
                Context.ExecuteDatabaseNonQueryText("master", "CREATE DATABASE [{0}]", databaseToCreate);
                scope.Complete();
            }
        }
    }
}

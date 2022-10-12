// <copyright file="Step99SetVersion.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Data.SqlServer.Updates.Version_1.Version_1_1
{
    using NeoMem2.Automation.Updates;
    using NeoMem2.Automation.Updates.Data;

    /// <summary>
    /// Update to version 1.1 step 99.
    /// </summary>
    [UpdateStep(Constants.NeoMem2DatabaseComponent, 1.1, 99, "Set version")]
    public class Step99SetVersion : DatabaseUpdateStep
    {
        /// <summary>
        /// Executes this update step.
        /// </summary>
        public override void Execute()
        {
            Context.ExecuteDefaultNonQueryText(
                "EXEC sys.sp_updateextendedproperty @name=N'Version', @value=N'1.1' ");
        }
    }
}

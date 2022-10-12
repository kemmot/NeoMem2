// <copyright file="Step99SetVersion.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Data.SqlServer.Updates.Version_0.Version_0_5
{
    using NeoMem2.Automation.Updates;
    using NeoMem2.Automation.Updates.Data;

    /// <summary>
    /// Update to version 0.5 step 99.
    /// </summary>
    [UpdateStep(Constants.NeoMem2DatabaseComponent, 0.5, 99, "Set version")]
    public class Step99SetVersion : DatabaseUpdateStep
    {
        /// <summary>
        /// Executes this update step.
        /// </summary>
        public override void Execute()
        {
            Context.ExecuteDefaultNonQueryText(
                "EXEC sys.sp_updateextendedproperty @name=N'Version', @value=N'0.5' ");
        }
    }
}

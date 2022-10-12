// <copyright file="Step02InsertTextFormatData.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Data.SqlServer.Updates.Version_1.Version_1_6
{
    using NeoMem2.Automation.Updates;

    /// <summary>
    /// Update to version 0.9 step 2.
    /// </summary>
    [UpdateStep(Constants.NeoMem2DatabaseComponent, 1.6, 1, "Insert TextFormat data")]
    public class Step01InsertTextFormatData : UpdateTextFormatsStepBase
    {
        /// <summary>
        /// Executes this update step.
        /// </summary>
        public override void Execute()
        {
            ProcessTextFormats();
        }
    }
}

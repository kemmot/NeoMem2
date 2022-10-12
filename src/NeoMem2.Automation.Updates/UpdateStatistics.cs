// <copyright file="UpdateStatistics.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Automation.Updates
{
    /// <summary>
    /// Statistics regarding an update.
    /// </summary>
    public class UpdateStatistics
    {
        /// <summary>
        /// Gets or sets the number of version updated.
        /// </summary>
        public int VersionsUpdated { get; set; }

        /// <summary>
        /// Gets or sets the number of steps executed.
        /// </summary>
        public int StepsUpdated { get; set; }
    }
}

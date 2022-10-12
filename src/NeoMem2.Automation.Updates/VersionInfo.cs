// <copyright file="VersionInfo.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Automation.Updates
{
    using System.Collections.Generic;

    /// <summary>
    /// Determines the steps involved to move to a software version.
    /// </summary>
    public class VersionInfo
    {
        /// <summary>
        /// The backing field for the <see cref="Steps"/> property.
        /// </summary>
        private readonly List<StepInfo> steps = new List<StepInfo>();

        /// <summary>
        /// Initializes a new instance of the <see cref="VersionInfo" /> class.
        /// </summary>
        public VersionInfo()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VersionInfo" /> class.
        /// </summary>
        /// <param name="version">The number of this version.</param>
        /// <param name="steps">The steps to execute.</param>
        public VersionInfo(double version, params StepInfo[] steps)
        {
            this.Version = version;
            this.Steps.AddRange(steps);
        }

        /// <summary>
        /// Gets the steps to execute.
        /// </summary>
        public List<StepInfo> Steps
        {
            get { return this.steps; }
        }

        /// <summary>
        /// Gets or sets the number of this version.
        /// </summary>
        public double Version { get; set; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return string.Format("Version: {0}", this.Version);
        }
    }
}

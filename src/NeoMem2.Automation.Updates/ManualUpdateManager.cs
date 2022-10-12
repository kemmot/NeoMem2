// <copyright file="ManualUpdateManager.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Automation.Updates
{
    /// <summary>
    /// An implementation of <see cref="UpdateManager"/> that requires the current version to be set manually.
    /// </summary>
    public class ManualUpdateManager : UpdateManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ManualUpdateManager" /> class.
        /// </summary>
        /// <param name="context">The update context.</param>
        public ManualUpdateManager(UpdateContext context)
            : base(context)
        {
        }

        /// <summary>
        /// Gets or sets the current component version.
        /// </summary>
        public double CurrentVersion { get; set; }
        
        /// <summary>
        /// Gets the current component version.
        /// </summary>
        /// <returns>The current component version.</returns>
        protected override double GetCurrentVersion()
        {
            return this.CurrentVersion;
        }
    }
}

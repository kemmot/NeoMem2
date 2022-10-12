// <copyright file="FuncUpdateManager.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Automation.Updates
{
    using System;

    /// <summary>
    /// An update manager that acquires current component version numbers from a supplied function.
    /// </summary>
    public class FuncUpdateManager : UpdateManager
    {
        /// <summary>
        /// The function used to retrieve the current component version number.
        /// </summary>
        private readonly Func<double> currentVersionRetriever;

        /// <summary>
        /// Initializes a new instance of the <see cref="FuncUpdateManager" /> class.
        /// </summary>
        /// <param name="context">The update context.</param>
        /// <param name="currentVersionRetriever">The function used to retrieve the current component version number.</param>
        public FuncUpdateManager(UpdateContext context, Func<double> currentVersionRetriever)
            : base(context)
        {
            this.currentVersionRetriever = currentVersionRetriever;
        }

        /// <summary>
        /// Gets the current component version.
        /// </summary>
        /// <returns>The current component version.</returns>
        protected override double GetCurrentVersion()
        {
            return this.currentVersionRetriever();
        }
    }
}

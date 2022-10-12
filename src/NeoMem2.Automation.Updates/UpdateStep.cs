// <copyright file="UpdateStep.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Automation.Updates
{
    /// <summary>
    /// A base class implementation of <see cref="IUpdateStep"/> providing functionality common to all implementations.
    /// </summary>
    public abstract class UpdateStep : IUpdateStep
    {
        /// <summary>
        /// Gets or sets the context of the update.
        /// </summary>
        public UpdateContext Context { get; set; }

        /// <summary>
        /// Executes this update step.
        /// </summary>
        public abstract void Execute();
    }
}

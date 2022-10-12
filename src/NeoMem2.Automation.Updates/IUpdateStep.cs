// <copyright file="IUpdateStep.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Automation.Updates
{
    /// <summary>
    /// The interface that must be implemented to provide code based update steps.
    /// </summary>
    public interface IUpdateStep
    {
        /// <summary>
        /// Gets or sets the context of the update.
        /// </summary>
        UpdateContext Context { get; set; }

        /// <summary>
        /// Executes this update step.
        /// </summary>
        void Execute();
    }
}
